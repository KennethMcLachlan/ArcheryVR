using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArrow : MonoBehaviour
{
    public float speed = 10f;
    public Transform tip;

    private Rigidbody _rigidbody;
    private bool _inAir = false;
    private Vector3 _lastPosition = Vector3.zero;

    //VFX
    private ParticleSystem _particleSystem;
    private TrailRenderer _trailRenderer;

    private float _forceValue;
    public TargetBehavior _targetBehavior;

    //Bomb Powerup
    public float explosiveForce = 10.0f;
    public float explosiveRadius = 5.0f;
    public float upwardsModifier = 5.0f;
    public bool _bombIsActive;

    //Bomb Behavior
    [SerializeField] private GameObject _explosionPFX;
    private Transform _player;
    [SerializeField] private AudioSource _explosionSFX;

    public PullInteraction pullInteraction;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        if (_player == null)
        {
            Debug.LogError("Player is null in the Bomb Arrow Script");
        }

        pullInteraction = GameObject.Find("BowString").GetComponent<PullInteraction>();
        if (pullInteraction == null)
        {
            Debug.LogError("Pull Interaction is null on the Bomb Arrow Script");
        }

        _explosionSFX = GetComponent<AudioSource>();
        if (_explosionSFX == null)
        {
            Debug.LogError("_explosionSFX is Null");
        }

        _rigidbody = GetComponent<Rigidbody>();
        PullInteraction.PullActionReleased += Release; // Subscribes to the Release Pull Interaction

        //VFX
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();

        PullInteraction.PullActionReleased += Release;
        StopFunctions();
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= Release; // Unsubscribes when the object is destroyed
    }

    private void Release(float value)
    {
        PullInteraction.PullActionReleased -= Release; // Unsubscribe to prevent the arrow from unintended movement
        if (gameObject != null)
        {
            gameObject.transform.parent = null;
        }
        _inAir = true;
        SetPhysics(true);

        //May be a spot to determine the Force applied to targets *****
        Vector3 force = transform.forward * value * speed; // Determines arrow force based on the value from PullActionReleased

        Debug.Log("Arrow Release Force Value: " + force + value);
        _forceValue = value;

        _rigidbody.AddForce(force, ForceMode.Impulse); // Adds force to the rigidbody (Projects the arrow)


        StartCoroutine(RotateWithVelocity());

        _lastPosition = tip.position;

        //VFX
        _particleSystem.Play();
        _trailRenderer.emitting = true;
    }

    private IEnumerator RotateWithVelocity() // Allows the arrow to rotate in unison with projection
    {
        yield return new WaitForFixedUpdate();
        while (_inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidbody.velocity, transform.up); // Allows the arrow to rotate with the transform.up
            transform.rotation = newRotation;
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        //If the arrow is in the air then check for collision
        if (_inAir == true)
        {
            CheckCollision();
            _lastPosition = tip.position; // Update the last position to egaul the current tip position
        }
    }

    private void CheckCollision() //Linecast seems to work better than a Box Trigger
    {
        if (Physics.Linecast(_lastPosition, tip.position, out RaycastHit hitInfo)) //Creates a linecast from the last position to the current tip position
        {
            if (hitInfo.transform.gameObject.layer != 9) //Ensures the arrow ignores the Player's body (Layer 9)
            {
                //Explosion Faces the Player
                Vector3 explosionPoint = hitInfo.point;
                Vector3 directionToPlayer = (_player.position - explosionPoint).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                Quaternion endRotation = lookRotation * Quaternion.Euler(-90, 0, 0);
                Instantiate(_explosionPFX, explosionPoint, endRotation);
                _explosionSFX.Play();

                //Arrows can be applied to anything with a Rigidbody except for the player
                if (hitInfo.transform.TryGetComponent(out Rigidbody body)) // if there's a Rigidbody
                {
                    _rigidbody.interpolation = RigidbodyInterpolation.None; // Turn off interpolation. No Jitter is noticeable and reduces memory usage
                    transform.parent = hitInfo.transform; //Set new parent of the arrow to what is hit so the arrow sticks to it
                }

                //Regular Arrow hit
                if (hitInfo.transform.gameObject.layer == 10)
                {

                    if (_forceValue >= 1) //Ensures the target is only affected when enough force is applied
                    {
                        TargetBehavior targetBehavior = hitInfo.transform.GetComponent<TargetBehavior>();
                        if (targetBehavior != null)
                        {
                            targetBehavior.TargetHit();

                            hitInfo.rigidbody.useGravity = true;
                            hitInfo.rigidbody.isKinematic = false;
                            body.AddForce(_rigidbody.velocity, ForceMode.Impulse); //Add Force to the Rigidbody to what was hit
                        }
                    }
                }

                //Bomb Arrow Powerup hit
                if (hitInfo.transform.gameObject.layer == 11) // Initiates the Bomb Powerup when hit
                {
                    BombPowerup bombPowerup = hitInfo.transform.GetComponent<BombPowerup>();
                    if (bombPowerup != null)
                    {
                        bombPowerup.BombPowerupHit();
                        _bombIsActive = true;

                        if (pullInteraction != null)
                        {
                            pullInteraction.ReceiveBombInfoTrue(); //Communicates with Pull Interaction Script

                            Debug.Log("Called ReceiveBombInfoTrue() from Arrow script");
                        }
                        else
                        {
                            Debug.LogError("PullInteraction is null in Arrow Script");
                        }
                        StartCoroutine(BombDurationRoutine());

                    }
                }

                //Bomb Arrow Explosion
                int layerMask = 1 << 10;
                Collider[] colliders = Physics.OverlapSphere(tip.position, explosiveRadius, layerMask);
                foreach (Collider hit in colliders)
                {
                    Rigidbody rigidbody = hit.GetComponent<Rigidbody>();
                    if (rigidbody != null)
                    {
                        TargetBehavior targetProp = rigidbody.GetComponent<TargetBehavior>();
                        rigidbody.useGravity = true;
                        rigidbody.isKinematic = false;
                        rigidbody.AddExplosionForce(explosiveForce, tip.transform.position, explosiveRadius, upwardsModifier, ForceMode.Impulse);
                        
                        if (targetProp != null)
                        {
                            Debug.Log("target was hit by explosion");
                            targetProp.TargetHit();
                        }
                        else
                        {
                            Debug.Log("Explosion did not affect other targets");
                        }
                    }
                }

                //Score Powerup Hit
                if (hitInfo.transform.gameObject.layer == 12)
                {
                    ScorePowerup scorePowerup = hitInfo.transform.GetComponent<ScorePowerup>();
                    if (scorePowerup != null)
                    {
                        scorePowerup.ScorePowerupHit();
                    }
                }

                //TimePowerup Hit
                //Arrows can hit the Time Powerup
                if (hitInfo.transform.gameObject.layer == 13)
                {
                    TimePowerup timePowerup = hitInfo.transform.GetComponent<TimePowerup>();
                    if (timePowerup != null)
                    {
                        timePowerup.TimePowerupHit();
                    }
                }

                StopFunctions();
                StartCoroutine(DestroyBombArrowRoutine());
            }
        }
    }

    private IEnumerator BombDurationRoutine()
    {
        yield return new WaitForSeconds(5f);
        _bombIsActive = false;
        pullInteraction.ReceiveBombInfoFalse(); //Tells Pull Interaction Script to change arrow Instantiation types
    }
    private void StopFunctions()
    {
        _inAir = false;
        SetPhysics(false);

        //VFX
        _particleSystem.Stop();
        _trailRenderer.emitting = false;
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidbody.useGravity = usePhysics;
        _rigidbody.isKinematic = !usePhysics;
    }

    private IEnumerator DestroyBombArrowRoutine()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

}
