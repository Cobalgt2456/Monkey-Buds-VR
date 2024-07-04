
using UnityEngine;

public class EnableOnStart : MonoBehaviour
{
    // This variable controls whether the GameObject should be active in Play mode
    public GameObject targetObject;

    // This method is called when the script is enabled
    void OnEnable()
    {
        // Enable the targetObject when the script is enabled (entering Play mode)
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
    }

    // This method is called when the script is disabled
    void OnDisable()
    {
        // Disable the targetObject when the script is disabled (exiting Play mode)
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }
}
