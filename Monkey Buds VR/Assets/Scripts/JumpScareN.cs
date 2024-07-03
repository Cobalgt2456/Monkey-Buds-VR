using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class JumpScareN : MonoBehaviour
{
    public GameObject objectEnable;
    public float enableTime = 1.0f; // in seconds
    public string targetTag = "Player"; // tag to check against


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            objectEnable.SetActive(true);
            StartCoroutine(DisableAfterDelay(enableTime));
        }
    }


    IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        objectEnable.SetActive(false);
    }
}
