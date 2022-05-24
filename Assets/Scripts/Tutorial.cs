using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Tutorial : MonoBehaviour
{
    // Reference to the Tutorial menu UI
    public GameObject m_TutorialMenu;
    public TextMeshProUGUI m_TutorialText;

    // AR Plane Manager component
    public ARPlaneManager m_ARPlaneManager;

    // How many plane needs to be detected before the tutorial can continue
    public int minPlaneDetected = 5;

    // Save file name for tutorial progress
    public static string s_tutorialSaveFile = "TutorialSave";

    // List of tutorial text
    [TextArea]
    public string[] tutorialText;

    // Is the tutorial done?
    public static bool s_tutorialDone;

    // Progression of the tutorial
    public static int s_TutorialProgress = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Return true if tutorial is done (1) else not done (0)
        s_tutorialDone = PlayerPrefs.GetInt(s_tutorialSaveFile, 1) == 1;

        // If tutorial not done
        if (!s_tutorialDone)
        {
            // Reset the tutorial progress just in case
            s_TutorialProgress = 0;

            // Change the tutorial text & show the tutorial menu
            m_TutorialText.text = tutorialText[0];
            m_TutorialMenu.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Since the tutorial is already done, we do not need to run any functions below for the tutorial
        // or no touch input at all
        if (s_tutorialDone)
            return;

        // If player is on the plane detection tutorial & the plane tracked is more than the minimum set
        if (s_TutorialProgress == 1 && m_ARPlaneManager.trackables.count >= minPlaneDetected)
        {
            // Plane detection tutorial completed, proceed to object placement tutorial
            s_TutorialProgress = 2;

            // Change text to object placement tutorial and show the UI
            m_TutorialText.text = tutorialText[2];
            m_TutorialMenu.SetActive(true);
        }

        // Not necessary to continue running if theres no input on the screen
        if (Input.touchCount == 0)
            return;

        // Store the touch
        Touch touch = Input.GetTouch(0);

        // Input began
        if (touch.phase == TouchPhase.Began)
        {
            // The order of the switch is important because it will might run multiple times due to setting
            // the tutorial progress in the function itself.
            switch (s_TutorialProgress)
            {
                case 1:
                    {
                        // Tapped on the start of plane detection tutorial, close and proceed to try out plane detection
                        m_TutorialMenu.SetActive(false);
                        break;
                    }
                case 0:
                    {
                        // Tapped on welcome screen, proceed to plane detection tutuorial
                        m_TutorialText.text = tutorialText[1];
                        s_TutorialProgress = 1;
                        m_TutorialMenu.SetActive(true);
                        break;
                    }
                case 2:
                    {
                        // We set a short delay since setting the progress immediately will result in
                        // spawning the object while closing the tutorial menu UI
                        StartCoroutine(SetDelayedProgress());

                        // Tapped on end of plane detection tutorial, close the plane detection tutorial
                        m_TutorialMenu.SetActive(false);
                        break;
                    }
                case 4:
                    {
                        // End of tutorial
                        s_TutorialProgress = 5;

                        // Marked as tutorial done and save it
                        s_tutorialDone = true;
                        PlayerPrefs.SetInt(s_tutorialSaveFile, 1);

                        // Turn off the menu
                        m_TutorialMenu.SetActive(false);
                        break;
                    }
            }
        }
        // Input let go
        else if (touch.phase == TouchPhase.Ended)
        {
            // Return true of solar system tagged object exist else return false
            bool solarSystemExist = GameObject.FindGameObjectWithTag("Solar System");

            // Placed the solar system object
            if (s_TutorialProgress == 3 && solarSystemExist)
            {
                // Completed the object placement tutorial, proceed to final tutorial words
                s_TutorialProgress = 4;

                // Change text to end of tutorial and show the UI
                m_TutorialText.text = tutorialText[3];
                m_TutorialMenu.SetActive(true);
            }
        }
    }

    IEnumerator SetDelayedProgress()
    {
        // Wait for 0.75s before changing the tutorial progress
        yield return new WaitForSeconds(0.75f);
        s_TutorialProgress = 3;
    }
}