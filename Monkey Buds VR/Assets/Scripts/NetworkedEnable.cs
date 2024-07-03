using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkedEnable : MonoBehaviourPun
{
    public List<GameObject> objectsToEnable;
    public string triggerTag = "Player"; // Editable in the Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            photonView.RPC("EnableObjects", RpcTarget.All);
        }
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
