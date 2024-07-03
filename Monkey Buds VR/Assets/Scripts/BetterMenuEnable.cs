using System.Collections.Generic;
using UnityEngine;
using easyInputs;

public class BetterMenuEnable : MonoBehaviour
{
 [Header("my black ni made this scirpt so dont say its yours.")]
    public EasyHand LeftHand;
    public GameObject ModMenu;
    private bool IsEnabled;
    private void Update()
    {
        if (EasyInputs.GetPrimaryButtonDown(EasyHand.LeftHand))
        {
            ModMenu.SetActive(true);
            IsEnabled = true;
        }
        else
        {
            ModMenu.SetActive(false);
            IsEnabled = false;
        }
    }
} 
