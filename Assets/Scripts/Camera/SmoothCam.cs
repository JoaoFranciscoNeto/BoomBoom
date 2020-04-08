using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam : MonoBehaviour
{
    public Transform Target;
    public Vector3 DefaultDistance = new Vector3(0f, 2f, -10f);
    public float DistanceDamp = 10f;

    private Vector3 _velocity = Vector3.one;

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var toPos = Target.position + (Target.rotation * DefaultDistance);
        var curPos = Vector3.SmoothDamp(transform.position, toPos, ref _velocity, DistanceDamp);
        transform.position = curPos;

        transform.LookAt(Target, Target.up);
    }
}
