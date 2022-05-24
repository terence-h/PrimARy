using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Spawner : MonoBehaviour
{
    // AR Raycast Manager component
    public ARRaycastManager m_ARRaycastManager;

    // AR Plane Manager component
    public ARPlaneManager m_ARPlaneManager;

    // List of game objects that will be used to spawn
    public List<GameObject> m_spawnableObjectPrefab;

    // AR Camera game object
    public GameObject m_ARCamera;

    // Position & rotation in 3D world space 
    Pose m_placementPose;

    // Reference to the spawned object
    GameObject m_spawnedObject = null;

    // Start is called before the first frame update
    void Start()
    {
        // At the start, there shouldn't be any spawnable game objects
        // Ensure that the game object variable is empty.
        m_spawnedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        // Not necessary to continue if the game is paused
        // Not necessary to continue running if theres no input on the screen
        if (PauseMenu.isPaused || Input.touchCount == 0)
            return;

        // Store the 2D Vector position of touched area
        Vector2 touchPt = Input.GetTouch(0).position;

        // Create a list of raycast hit
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        // Raycast from the touch position and store the raycast hits in the 'hits' variable
        m_ARRaycastManager.Raycast(touchPt, hits);

        // If there are no hits, no purpose of continuing to run the functions
        if (hits.Count == 0)
            return;

        // Position & rotation in 3D world space 
        m_placementPose = hits[0].pose;

        if (Tutorial.s_tutorialDone || Tutorial.s_TutorialProgress == 3)
        {
            // Start of touch input
            // Only spawn the object if the solar system object doest not exist
            if (Input.GetTouch(0).phase == TouchPhase.Began && !SolarSystem.s_isActive)
            {
                // Flag the solar system to be active
                SolarSystem.s_isActive = true;

                // Spawn the 1st game object in the list at the stored pose.
                Spawn(0, m_placementPose.position);
            }
            // Input is still held and moving & the solar system ojbect exist
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && m_spawnedObject)
            {
                // Update the position of the spawned game object base on the raycast hit position
                m_spawnedObject.transform.position = m_placementPose.position;
            }
            // Input let go
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                // In the case the object wasnt created, we dont want to hide our plane trackables
                if (m_spawnedObject == null)
                    return;

                // We longer need to reference the spawned object anymore.
                m_spawnedObject = null;

                // Since we do not need the plane detection trackables anymore
                // We can simply hide the every one of them
                // Note that I did not disable the AR Plane Manager completely
                // New planes will still be detected, but they wont be shown on screen
                // This will give better immersive experience
                foreach (var plane in m_ARPlaneManager.trackables)
                    plane.gameObject.SetActive(false);

                // Stop detecting any plane since we already created our solar system
                m_ARPlaneManager.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.None;
            }
        }
    }

    // Arguments taken (1 - index of the game objects list, 2 - Position to spawn)
    void Spawn(int id, Vector3 position)
    {
        // Instantiate the game object at the 2nd argument given
        m_spawnedObject = Instantiate(m_spawnableObjectPrefab[id],
            position,
            Quaternion.identity);

        m_spawnedObject.SetActive(true);
    }
}