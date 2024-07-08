using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PowerOutageManagerNetworked : MonoBehaviourPun
{
    [Header("Objects you want to enable when power goes out")]
    public GameObject[] PowerThingy;
    [Space]
    [Header("300 sec = 5 min 600 = 10 min 900 sec = 15 min 1800 = 30 min")]
    public float timerDuration = 900f;
    [Space]
    [Header("Don't edit or it will break; it's only there to show how much time has gone")]
    [SerializeField] private float timer = 0f;
    private bool isTimerStarted = true;
    private bool areObjectsEnabled = true;

    void Start()
    {
        isTimerStarted = true;
    }

    void Update()
    {
        if (isTimerStarted)
        {
            timer += Time.deltaTime;
            if (timer >= timerDuration && !areObjectsEnabled)
            {
                photonView.RPC("EnableGameObjects", RpcTarget.All);
                areObjectsEnabled = true;
            }
        }
    }

    [PunRPC]
    void EnableGameObjects()
    {
        foreach (GameObject obj in PowerThingy)
        {
            obj.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandTag"))
        {
            photonView.RPC("DisableGameObjects", RpcTarget.All);
            timer = 0f;
            areObjectsEnabled = false;
            Debug.Log("Objects have been disabled and timer reset.");
        }
    }

    [PunRPC]
    void DisableGameObjects()
    {
        foreach (GameObject obj in PowerThingy)
        {
            obj.SetActive(false); // Setting objects inactive
        }
    }
}
