using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.VR;

public class OfflineRig : MonoBehaviour
{

    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject Body;
    public GameObject Head;
    public PhotonVRManager Manager;

    void Update() 
    {
        if (PhotonNetwork.InRoom == false) 
        {
        LeftHand.SetActive(true);
            Body.SetActive(true);
            RightHand.SetActive(true);
            Head.SetActive(true);
            LeftHand.transform.position = Manager.LeftHand.position;
            LeftHand.transform.rotation = Manager.LeftHand.rotation;
            RightHand.transform.position = Manager.RightHand.position;
            RightHand.transform.rotation = Manager.RightHand.rotation;
            Head.transform.position = Manager.Head.position;
            Head.transform.rotation = Manager.Head.rotation;
        
        }
    if(PhotonNetwork.InRoom) 
        {
        LeftHand.SetActive(false);
            RightHand.SetActive(false);
            Head.SetActive(false);
            Body.SetActive(false);

        }
    
    }
}