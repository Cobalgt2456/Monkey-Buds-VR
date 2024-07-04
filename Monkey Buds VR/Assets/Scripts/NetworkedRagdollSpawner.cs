using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using easyInputs;

public class NetworkedRagdollSpawner : MonoBehaviour
{
[Header("siren made this edited by cobalt to make it networked and disable delay.")]

    public GameObject spawnObject;
    public Transform spawnSpot;
    public string Handtag;
    public float disableDelay = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == Handtag)
        {
            GameObject obj = PhotonNetwork.Instantiate(spawnObject.name, spawnSpot.position, Quaternion.identity);
            StartCoroutine(DisableObjectAfterDelay(obj));
        }
    }

    IEnumerator DisableObjectAfterDelay(GameObject obj)
    {
        yield return new WaitForSeconds(disableDelay);
        if (obj != null)
        {
            PhotonView photonView = obj.GetComponent<PhotonView>();
            if (photonView != null && photonView.IsMine)
            {
                PhotonNetwork.Destroy(obj);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }
}
