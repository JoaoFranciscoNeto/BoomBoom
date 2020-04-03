using Assets.Scripts.StarFighter;
using UnityEngine;

[RequireComponent(typeof(FighterInput))]
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

    private Rigidbody _rigidBody;

    private FighterInput _input;

    private Transform _body;

    // Start is called before the first frame update
    void Start()
    {
        _thrusters = GetComponentsInChildren<Thruster>();
        _rigidBody = GetComponent<Rigidbody>();
        _input = GetComponent<FighterInput>();

        _body = transform.Find("Body").transform;
        if (_body == null)
        {
            Debug.LogError($"Body not found in {transform.name}");
        }

    }

    private void FixedUpdate()
    {
        Rotate(_input.Pitch,_input.Yaw,_input.Roll);
        Thrust(_input.Throttle,0);
    }

    public void Rotate(float pitch, float yaw, float roll)
    {
        _rigidBody.AddRelativeTorque(new Vector3(pitch * _pitchPower, -yaw * _yawPower, -transform.right.y * _rollPower) * 100f,ForceMode.Force);

        var currentAngle = Vector3.SignedAngle(transform.up, _body.up, transform.forward);
        var desiredAngle = Mathf.Lerp(-45, 45, (yaw / 2) + .5f);

        var rotationAngle = desiredAngle - currentAngle;

        //_body.Rotate(0,0,Mathf.Lerp(0,rotationAngle,Time.deltaTime*20f));
    }

    public void Thrust(float throttle, float strafe)
    {
        foreach (var thruster in _thrusters)
        {
            thruster.Activate(throttle);
        }

        var throttleVector = Vector3.forward * throttle * _thrustPower;
        var strafeVector = Vector3.right * strafe * _strafePower;

        var combinedVector = (throttleVector + strafeVector);

        _rigidBody.AddRelativeForce(combinedVector * 100f,ForceMode.Force);
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with " + collision.other.name);
    }
}
