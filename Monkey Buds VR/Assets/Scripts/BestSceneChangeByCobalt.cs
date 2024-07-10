using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Collections.Generic;

public class BestSceneChangeByCobalt : MonoBehaviour
{
    public string targetSceneName;
    public List<GameObject> objectsToDestroy = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandTag")) 
        {
            foreach (GameObject obj in objectsToDestroy)
            {
                Destroy(obj);
            }

            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
