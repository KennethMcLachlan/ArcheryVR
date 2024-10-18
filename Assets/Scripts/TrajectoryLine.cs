using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.XR.Interaction.Toolkit.XRInteractionUpdateOrder;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class TrajectoryLine : MonoBehaviour
{
    //Reference to the new Simulated Scene
    private Scene _simulatedScene;
    //Reference to the phyics scene of the simulated scene
    private PhysicsScene _physicsScene;

    [SerializeField] private GameObject _arrowPrefab;

    //Create reference to the Bow (Maybe)
    [SerializeField] private Transform _environment;

    [SerializeField] private PullInteraction _pullInteraction;

    void Start()
    {
        //Set the Bow reference
        _environment = gameObject.transform; // Like the lab parent in the video (GDHQ)

        //Call the Create Simulated Physics Scene Method
        CreateSimulatedPhysicsScene();

        _pullInteraction = GameObject.Find("BowString").GetComponent<PullInteraction>();
    }

    void Update()
    {
        
    }

    //Creates the Trajectory Line Scene
    void CreateSimulatedPhysicsScene()
    {
        _simulatedScene = SceneManager.CreateScene("TrajectoryLine", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulatedScene.GetPhysicsScene();

        //ForEach object tagged as arrow - Do I need to do the whole environment and not the arrow?
        foreach (Transform objects in _environment)
        {
            if (objects.CompareTag("Environment"))
            {
                //Instantiates an arrow based on the Tag
                var simulatedObjects = Instantiate(objects.gameObject, objects.position, objects.rotation);
                if (objects.GetComponentInChildren<Renderer>() != null)
                {
                    objects.GetComponentInChildren<Renderer>().enabled = false;

                    //Remove Script
                    //objects.GetComponent<Arrow>().enabled = false;
                }
                SceneManager.MoveGameObjectToScene(simulatedObjects, _simulatedScene);
            }
        }
        //Create a reference to the simulated obstacles
        //GetMeshRenderer of each obstacle and disable it
        //Move GameObjects to the new simulated physic scene

    }

    public void SimulatedTrajectory(Arrow arrow, Vector3 pos, Vector3 pullPosition)
    {
        //Reference for a simulated arrow - Should this be referenced here?
        var simulatedArrow = Instantiate(arrow, pos, Quaternion.identity);

        //simulated object renderer is disabled
        simulatedArrow.GetComponentInChildren<Renderer>().enabled = false;

        //Move the simulated object to the simulated physics scene
        SceneManager.MoveGameObjectToScene(simulatedArrow.gameObject, _simulatedScene);

        //apply velocity to the simulated object
        _pullInteraction.CalculatePull(pullPosition);
        simulatedArrow.pullInteraction.CalculatePull(pullPosition);
    }

}
