using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam : MonoBehaviour
{
    public Transform Target;
    public Vector3 DefaultDistance = new Vector3(0f, 2f, -10f);
    public float Damp = 10f;

    [SerializeField]
    private AnimationCurve upInfluence;

    private Vector3 _velocity = Vector3.one;

    void Awake()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Target.position;

        //var upVector = (Mathf.Abs(Target.forward.y) > .9f) ? Target.up : Vector3.up;

        var evaluatedInfluence = upInfluence.Evaluate(Math.Abs(Target.forward.y));
        var upVector = Vector3.Lerp( Vector3.up, Target.up, evaluatedInfluence);

        var targetRigRotation = Quaternion.LookRotation(Target.forward, upVector);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRigRotation, Damp * Time.deltaTime);
    }
}
