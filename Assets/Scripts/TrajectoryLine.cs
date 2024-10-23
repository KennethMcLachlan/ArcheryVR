using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.XR.Interaction.Toolkit.XRInteractionUpdateOrder;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using JetBrains.Annotations;

public class TrajectoryLine : MonoBehaviour
{
    //Reference to the new Simulated Scene
    private Scene _simulatedScene;
    //Reference to the phyics scene of the simulated scene
    private PhysicsScene _physicsScene;

    [SerializeField] private GameObject _arrowPrefab;

    //Create reference to the environment for the simulated space
    //[SerializeField] private Transform _environment;

    [SerializeField] private PullInteraction _pullInteraction;

    //Trajectory Line Variables
    //[SerializeField] private GameObject _bowString; // Holds the Trajectory line start position
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _maxPhysicsIterations;

    void Start()
    {
        _lineRenderer.GetComponent<LineRenderer>();
        if (_lineRenderer == null)
        {
            Debug.LogError("Line Renderer is null");
        }
    }

    public void SimulateTrajectory(Vector3 startPosition, Vector3 initialVelocity)
    {
        Vector3 currentPosition = startPosition;
        Vector3 currentVelocity = initialVelocity;

        // Set the number of points in the Line Renderer
        _lineRenderer.positionCount = _maxPhysicsIterations;

        for (int i = 0; i < _maxPhysicsIterations; i++)
        {
            // Store the current position
            _lineRenderer.SetPosition(i, currentPosition);

            // Apply gravity to the velocity
            currentVelocity += Physics.gravity * Time.fixedDeltaTime;

            // Update the position based on the velocity
            currentPosition += currentVelocity * Time.fixedDeltaTime;
        }
    }

    public void ClearTrajectory()
    {
        _lineRenderer.positionCount = 0;
    }

}
