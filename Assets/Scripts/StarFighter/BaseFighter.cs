using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFighter : MonoBehaviour
{
    [SerializeField]
    private float _thrustPower = 40f;
    [SerializeField]
    private float _strafePower = 10f;

    [SerializeField]
    private float _pitchPower = 20f;
    [SerializeField]
    private float _yawPower = 10f;
    [SerializeField]
    private float _rollPower = 10f;

    private Thruster[] _thrusters;

    // Start is called before the first frame update
    void Start()
    {
        _thrusters = GetComponentsInChildren<Thruster>();
    }

    public void Rotate(float pitch, float yaw, float roll)
    {
        transform.Rotate(pitch * _pitchPower * Time.deltaTime, -yaw * _yawPower * Time.deltaTime, -transform.right.y * _rollPower * Time.deltaTime);
    }

    public void Thrust(float throttle, float strafe)
    {
        foreach (var thruster in _thrusters)
        {
            thruster.Activate(throttle);
        }

        var throttleVector = transform.forward * Time.deltaTime * throttle * _thrustPower;
        var strafeVector = transform.right * Time.deltaTime * strafe * _strafePower;

        var combinedVector = (throttleVector + strafeVector);
        transform.position += combinedVector;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up);
    }
}
