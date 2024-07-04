using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;

public class UiSceneChange : MonoBehaviour
{
    public string targetSceneName;  // The name of the scene to load
    public List<GameObject> objectsToDestroy = new List<GameObject>();  // List of objects to destroy before changing scene
    public Button yourUIButton;  // Reference to your UI Button
    public float delayBeforeChange = 0f;  // Optional delay before changing scene (in seconds)
    public bool enableDelay = false;  // Checkbox to enable or disable delay
    public GameObject objectToEnable;  // Optional GameObject to enable on button click
    public bool enableObject = false;  // Checkbox to enable or disable object activation

    void Start()
    {
        // Ensure the button reference is set in the inspector
        if (yourUIButton != null)
        {
            // Add the ChangeSceneWithOptions method to the button's onClick event
            yourUIButton.onClick.AddListener(() => StartCoroutine(ChangeSceneWithOptions()));
        }
        else
        {
            Debug.LogError("Button reference is not assigned in the inspector.");
        }
    }

    // Coroutine to handle the optional delay and GameObject activation
    IEnumerator ChangeSceneWithOptions()
    {
        // If delay is enabled, wait for the specified delay time
        if (enableDelay && delayBeforeChange > 0f)
        {
            yield return new WaitForSeconds(delayBeforeChange);
        }

        // If object enabling is toggled on, activate the specified GameObject
        if (enableObject && objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }

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
