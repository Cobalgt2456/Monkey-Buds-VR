using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterEnableByCobalt : MonoBehaviour
{
    public List<GameObject> objectsToEnable;
    public List<GameObject> objectsToDisable;
    public float cooldown = 2.0f;
    private bool isCooldownActive;

    [Header("Optional Delay Settings")]
    public bool useDelay = false;
    public float delay = 2.0f;

    [Header("Optional Timed Disable Settings")]
    public bool useTimedDisable = false;
    public float timeToDisable = 5.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (!isCooldownActive)
        {
            if (useDelay)
            {
                StartCoroutine(EnableDisableWithDelay());
            }
            else
            {
                EnableDisableObjects();
                if (useTimedDisable)
                {
                    StartCoroutine(TimedDisableObjects());
                }
                StartCoroutine(CooldownRoutine());
            }
        }
    }

    private IEnumerator EnableDisableWithDelay()
    {
        yield return new WaitForSeconds(delay);
        EnableDisableObjects();
        if (useTimedDisable)
        {
            StartCoroutine(TimedDisableObjects());
        }
        StartCoroutine(CooldownRoutine());
    }

    private void EnableDisableObjects()
    {
        foreach (var obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
        foreach (var obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }

    private IEnumerator TimedDisableObjects()
    {
        yield return new WaitForSeconds(timeToDisable);
        foreach (var obj in objectsToEnable)
        {
            obj.SetActive(false);
        }
    }

    private IEnumerator CooldownRoutine()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(cooldown);
        isCooldownActive = false;
    }
}
