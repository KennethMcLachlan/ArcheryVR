using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    private float _forceValue;

    private Vector3 _lastPosition = Vector3.zero;

    public Transform tip;
    private Rigidbody _rigidbody;
    private TrailRenderer _trailRenderer;
    public PullInteraction pullInteraction;

    private bool _inAir = false;
    private void Awake()
    {
        pullInteraction = GameObject.Find("BowString").GetComponent<PullInteraction>();
        PullInteraction.PullActionReleased += Release;

        _rigidbody = GetComponent<Rigidbody>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();

        StopFunctions();
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= Release;
    }

    public void Release(float value)
    {
        PullInteraction.PullActionReleased -= Release;

        if (gameObject != null)
        {
            gameObject.transform.parent = null;
        }

        _inAir = true;
        SetPhysics(true);

        Vector3 force = transform.forward * value * speed;
        _forceValue = value;
        _rigidbody.AddForce(force, ForceMode.Impulse);

        StartCoroutine(RotateWithVelocity());

        _lastPosition = tip.position;
        _trailRenderer.emitting = true;
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while (_inAir) 
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidbody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (_inAir == true)
        {
            CheckCollision();
            _lastPosition = tip.position;
        }
    }

    private void CheckCollision()
    {
        if (Physics.Linecast(_lastPosition, tip.position, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.gameObject.layer != 9) //Ensures the arrow ignores the Player's body (Layer 9)
            {
                //Arrows can be applied to anything with a Rigidbody except for the player
                if (hitInfo.transform.TryGetComponent(out Rigidbody body))
                {
                    _rigidbody.interpolation = RigidbodyInterpolation.None;
                    transform.parent = hitInfo.transform;
                }

                //Arrows can hit Targets
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
                            body.AddForce(_rigidbody.velocity, ForceMode.Impulse);
                        }
                    }
                }

                //Arrows can hit the Bomb Powerup
                if (hitInfo.transform.gameObject.layer == 11)
                {
                    BombPowerup bombPowerup = hitInfo.transform.GetComponent<BombPowerup>();
                    if (bombPowerup != null)
                    {
                        bombPowerup.BombPowerupHit();
                    }
                }

                //Arrows can hit the Score Powerup
                if (hitInfo.transform.gameObject.layer == 12)
                {
                    ScorePowerup scorePowerup = hitInfo.transform.GetComponent<ScorePowerup>();
                    if (scorePowerup != null)
                    {
                        scorePowerup.ScorePowerupHit();
                    }
                }

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
                StartCoroutine(DestroyArrowRoutine());
            }
        }
    }

    public void StopFunctions()
    {
        _inAir = false;
        SetPhysics(false);
        _trailRenderer.emitting = false;
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidbody.useGravity = usePhysics;
        _rigidbody.isKinematic = !usePhysics;
    }

    private IEnumerator DestroyArrowRoutine()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
