
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Required for UI components
using Photon.Pun;
using System.Collections.Generic;

public class UiSceneChange : MonoBehaviour
{
    public string targetSceneName;  // The name of the scene to load
    public List<GameObject> objectsToDestroy = new List<GameObject>();  // List of objects to destroy before changing scene
    public Button yourUIButton;  // Reference to your UI Button

    void Start()
    {
        // Ensure the button reference is set in the inspector
        if (yourUIButton != null)
        {
            // Add the ChangeScene method to the button's onClick event
            yourUIButton.onClick.AddListener(ChangeScene);
        }
        else
        {
            Debug.LogError("Button reference is not assigned in the inspector.");
        }
    }

    // Method to be called when the button is clicked
    void ChangeScene()
    {
        // Destroy each object in the objectsToDestroy list
        foreach (GameObject obj in objectsToDestroy)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        // Disconnect from Photon Network
        PhotonNetwork.Disconnect();

        // Load the target scene
        SceneManager.LoadScene(targetSceneName);
    }
}
