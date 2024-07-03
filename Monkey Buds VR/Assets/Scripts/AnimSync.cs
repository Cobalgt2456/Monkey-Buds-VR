using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSync : MonoBehaviour
{
    public Vector3 oldpos;
    public Transform ai;
    public Animator anim;
    public float speed;

    private void Start()
    {
        oldpos = ai.position;
    }

    private void Update()
    {
        float currentSpeed = Vector3.Distance(ai.position, oldpos) / Time.deltaTime;
        float normalizedSpeed = Mathf.Clamp01(currentSpeed / speed);
        anim.speed = normalizedSpeed;
        oldpos = ai.position;
    }
}
