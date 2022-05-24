using UnityEngine;

public class OrbitController : MonoBehaviour
{
    // Orbit line of the planet
    public GameObject m_orbitLine;

    // Determines the orbit speed of the sun/planets
    public float orbitSpeed = 1f;

    // Determines the rotation speed of the sun/planets
    public float rotationSpeed = 1f;

    // Planet ID
    public int planetId = 0;

    // Update is called once per frame
    void Update()
    {
        // If the planet hit is the same as this game object hit or the pause menu is active
        // Dont rotate and stop the orbit of the planet
        if (planetId == TouchInputManager.planetHit || PauseMenu.isPaused)
            return;

        // Rotate the object around the axis parent
        // Rate: rotationSpeed degress/second
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Only rotate if its not the sun
        if (planetId != 1)
        {
            // Rotate the object around the forward axis
            // Rate: rotationSpeed degress/second
            m_orbitLine.transform.Rotate(Vector3.forward, orbitSpeed * Time.deltaTime);
        }
    }
}