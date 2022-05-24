using TMPro;
using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    // Refernece to the AR Camera
    public Camera m_ARCamera;

    // Reference to the ray cast hit game object
    GameObject m_hitObject = null;

    // Keep track of which planet or button has been touched.
    public static int planetHit = 0;
    public static int btnHit = 0;

    // Reference to the Info Box and its Text
    public GameObject m_infoBox;
    public TextMeshPro m_txtInfo;

    // Solar System layermask
    public LayerMask m_solarSystemLayer;
    
    // List of srings for the Info Box
    public string[] m_TextSrings;

    // Update is called once per frame
    void Update()
    {
        // Not necessary to continue if the game is paused
        // Not necessary to continue running if theres no input on the screen
        if (PauseMenu.isPaused || Input.touchCount == 0)
            return;

        // Store the 2D Vector position of touched area
        Touch touch = Input.GetTouch(0);
        Vector2 touchPt = touch.position;

        // Input began
        if (touch.phase == TouchPhase.Began)
        {
            // Raycast from camera
            Ray ray = m_ARCamera.ScreenPointToRay(touchPt);

            // Cast a raycast and ignore the solar system layer
            if (Physics.Raycast(ray, out RaycastHit hit, m_solarSystemLayer))
            {
                // Store the game object hit
                m_hitObject = hit.collider.gameObject;

                // If planet tag collider was hit
                if (hit.collider.CompareTag("Planet"))
                {
                    // If the user is reading the info box, we dont want it to process the input on tapping planets
                    // Prevents the planet from moving away if tap on Planet tag collider
                    // Only allow closing the info box by tapping on the close icon
                    if (m_infoBox.activeSelf)
                        return;

                    // Hide previous button before changing the btn variable
                    HideButton();

                    // Parse the name of the planet into an integer and store it
                    planetHit = int.Parse(m_hitObject.name);

                    // Set the buttons to active based on the planet touched
                    SolarSystem.m_Btn[planetHit - 1].SetActive(true);
                }
                // If Planet Button collider was hit
                else if (hit.collider.CompareTag("Planet Button"))
                {
                    // If the user is reading the info box, we dont want it to process the input on tapping planets
                    // Prevents the planet from moving away if tap on Planet tag collider
                    // Only allow closing the info box by tapping on the close icon
                    if (m_infoBox.activeSelf)
                        return;

                    // Parse the name of the planet into an integer and store it
                    btnHit = int.Parse(m_hitObject.name);

                    // Set the info box position to the button position
                    m_infoBox.transform.position = m_hitObject.transform.position + Vector3.up * 0.1f;

                    /* Look at the AR Camera
                       We use this basic vector math to have the "back" of the UI to look at us
                       Adding its own position will result in the direction vector to start at its own position instead
                       Hence, flipping the UI around
                       We will also call this in another script in Update to keep looking at the AR Camera.
                       We call it once here so that when it activates, it does not look like it changed for a split second.
                       2 * v1 - v2 */
                    m_infoBox.transform.LookAt(2 * m_infoBox.transform.position - m_ARCamera.transform.position);

                    // Change the information box text based on the button touched
                    m_txtInfo.text = m_TextSrings[btnHit - 1];
                    m_infoBox.SetActive(true);
                }
                else if (hit.collider.CompareTag("Info Close"))
                {
                    // Hide the info box
                    m_infoBox.SetActive(false);
                }
            }
            else // Did not hit any collider
            {
                // Only reset the hit IDs if the info box is inactive
                // Prevents the planet from moving away if tap on empty collider
                if (!m_infoBox.activeSelf)
                {
                    // Hide previous button
                    HideButton();

                    //Reset the planet hit ID & button hit ID
                    planetHit = 0;
                    btnHit = 0;
                }
            }
        }
    }

    void HideButton()
    {
        // Loop every button
        for (int i = 0; i < SolarSystem.m_Btn.Length; i++)
        {
            // Hide all of the button
            SolarSystem.m_Btn[i].SetActive(false);
        }
    }
}