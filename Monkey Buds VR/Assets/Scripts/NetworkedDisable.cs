using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkedDisable : MonoBehaviourPun
{
    public List<GameObject> objectsToDisable;
    public string triggerTag = "Player"; // Editable in the Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            photonView.RPC("DisableObjects", RpcTarget.All);
        }
    }

    [PunRPC]
    void DisableObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }
}
