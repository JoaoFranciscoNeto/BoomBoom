using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform Target;
    public Vector3 DefaultDistance = new Vector3(0f, 2f, -10f);
    public float DistanceDamp = 10f;
    public float RotationalDamp = 10f;

    void LateUpdate()
    {
        var toPos = Target.position + (Target.rotation * DefaultDistance);
        var curPos = Vector3.Lerp(transform.position, toPos, DistanceDamp * Time.deltaTime); 
        transform.position = curPos;

        var toRot = Quaternion.LookRotation(Target.position - transform.position, Target.up);
        var curRot = Quaternion.Slerp(transform.rotation, toRot, RotationalDamp * Time.deltaTime);
        transform.rotation = curRot;
    }
}
