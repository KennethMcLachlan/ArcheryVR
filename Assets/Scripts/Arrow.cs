using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
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

    //Ray / Linecast
    //private RaycastHit _hitInfo;

    private void Awake()
    {
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
                //Arrows can be applied to anything with a Rigidbody except for the player
                if (hitInfo.transform.TryGetComponent(out Rigidbody body)) // if there's a Rigidbody
                {
                    _rigidbody.interpolation = RigidbodyInterpolation.None; // Turn off interpolation. No Jitter is noticeable and reduces memory usage
                    transform.parent = hitInfo.transform; //Set new parent of the arrow to what is hit so the arrow sticks to it

                    //_targetBehavior.UpdateForceValue(_forceValue); Deemed Uneccesary
                    
                }

                if (hitInfo.transform.gameObject.layer == 10)
                {
                    //Deemed next two line uneccessary
                    //_rigidbody.interpolation = RigidbodyInterpolation.None;
                    //transform.parent = hitInfo.transform; //Arrow sticks to the object it hits

                    //Communicate to the tragetBehavior script
                    

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

                StopFunctions();
            }
        }
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

    //public void CurrentForceValue(float value)
    //{
    //    _forceValue = value;
    //}
}
