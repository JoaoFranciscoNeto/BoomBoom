using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.StarFighter;
using UnityEngine;

public class AIFigter : FighterInput
{

    [SerializeField]
    private Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Throttle = 1f;

        var targetPoint = _target.position;

        var localTargetPoint = transform.InverseTransformVector(targetPoint - transform.position).normalized;
        var angleOff = Vector3.Angle(transform.forward, targetPoint - transform.position);

        var rollToTarget = -Mathf.Clamp(localTargetPoint.x, -1f, 1f);
        var rollToLevel = -transform.right.y;
        var levelInfluence = Mathf.InverseLerp(0f, 10f, angleOff);

        Pitch = Mathf.Clamp(-localTargetPoint.y, -1f, 1f);
        Yaw = Mathf.Clamp(-localTargetPoint.x, -1f, 1f);
        Roll = Mathf.Lerp(rollToLevel, rollToTarget, levelInfluence);
    }
}
