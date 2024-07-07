using System.Collections;
using UnityEngine;
using Photon.Pun;

public class JumpScare : MonoBehaviourPunCallbacks
{
    public GameObject objectToEnable;
    public GameObject objectToDisable;
    public string handTag = "HandTag";
    public float enableDuration = 2.0f;
    public float disableDuration = 1.0f;

    void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine && other.CompareTag(handTag))
        {
            StartCoroutine(EnableDisableCoroutine());
        }
    }

    IEnumerator EnableDisableCoroutine()
    {
        // Enable objectToEnable for enableDuration seconds
        objectToEnable.SetActive(true);
        yield return new WaitForSeconds(enableDuration);
        objectToEnable.SetActive(false);

        // Wait a moment before disabling objectToDisable
        yield return new WaitForSeconds(0.1f);

        // Disable objectToDisable for disableDuration seconds
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }

        yield return new WaitForSeconds(disableDuration);

        // Re-enable objectToDisable after disableDuration seconds
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(true);
        }
    }
}
