using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    // Use to flag if the solar system is active
    public static bool s_isActive { get; set; }

    // List of buttons in the solar system
    public static GameObject[] m_Btn;

    // Reference to our skybox object
    public GameObject m_Skybox;

    // Start is called before the first frame update
    void Start()
    {
        // Only used to initalize the list
        m_Btn = GameObject.FindGameObjectsWithTag("Planet Buttons Holder");

        // Loop every single button holders
        for (int i = 0; i < 9; i++)
        {
            // Reallocate the buttton index as it may get jumbled using FindGameObjectsWithTag
            m_Btn[i] = GameObject.Find((i + 1).ToString() + "holder");

            // Hide all of them
            m_Btn[i].SetActive(false);
        }
    }

    void OnDestroy()
    {
        // When the object is destroyed, let other script knows it is not active anymore
        // We can use this incase in the future we want to allow to destroy the solar system
        // We can create a new one if this boolean is false
        s_isActive = false;
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Only if the collider is MainCamera tag
        if (other.CompareTag("MainCamera"))
        {
            // Turn on/off the skybox
            m_Skybox.SetActive(!m_Skybox.activeSelf);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Only if the collider is MainCamera tag
        if (other.CompareTag("MainCamera"))
        {
            // Turn on/off the skybox
            m_Skybox.SetActive(!m_Skybox.activeSelf);
        }
    }
}