using UnityEngine;

public class InfoBox : MonoBehaviour
{
    // Reference to our AR Camera
    public Camera m_ARCamera;
    
    // Update is called once per frame
    void Update()
    {
        // Look at the AR Camera
        // We use this basic vector math to have the "back" of the UI to look at us
        // Adding its own position will result in the direction vector to start at its own position instead
        // Hence, flipping the UI around
        // 2 * v1 - v2
        transform.LookAt(2 * transform.position - m_ARCamera.transform.position);
    }
}