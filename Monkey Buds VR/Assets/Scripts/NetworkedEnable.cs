using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkedEnable : MonoBehaviourPun
{
    public List<GameObject> objectsToEnable;
    public string triggerTag = "Player"; // Editable in the Inspector
    public bool useDelay = false; // Checkbox to enable delay
    public float delayDuration = 1.0f; // Delay duration in seconds

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            if (useDelay)
            {
                StartCoroutine(EnableObjectsWithDelay());
            }
            else
            {
                photonView.RPC("EnableObjects", RpcTarget.All);
            }
        }
    }

    IEnumerator EnableObjectsWithDelay()
    {
        yield return new WaitForSeconds(delayDuration);
        photonView.RPC("EnableObjects", RpcTarget.All);
    }

    [PunRPC]
    public void EnableObjects()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
    }
}
