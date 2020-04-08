using Assets.Scripts.StarFighter;
using UnityEngine;

[RequireComponent(typeof(FighterInput))]
public class BaseFighter : MonoBehaviour
{
    [Tooltip("X: Lateral thrust\nY: Vertical thrust\nZ: Longitudinal Thrust")]
    public Vector3 LinearForce = new Vector3(100.0f, 100.0f, 100.0f);

    [Tooltip("X: Pitch\nY: Yaw\nZ: Roll")]
    public Vector3 AngularForce = new Vector3(100.0f, 100.0f, 100.0f);


    private Thruster[] _thrusters;
    private Rigidbody _rigidBody;
    private FighterInput _input;
    private Transform _body;

    private const float ForceMultiplier = 100f;

    void Awake()
    {
        _thrusters = GetComponentsInChildren<Thruster>();
        _rigidBody = GetComponent<Rigidbody>();
        _input = GetComponent<FighterInput>();

        _body = transform.Find("Body").transform;
        if (_body == null)
        {
            Debug.LogError($"Body not found in {transform.name}");
        }

        _rigidBody.centerOfMass = Vector3.back;

    }

    private void FixedUpdate()
    {
        Thrust(_input.Throttle);
        Rotate(_input.Pitch,_input.Yaw,_input.Roll);
    }

    public void Rotate(float pitch, float yaw, float roll)
    {
        _rigidBody.AddRelativeTorque(
            Vector3.Scale(
                new Vector3(pitch, -yaw, roll),
                AngularForce) *
            ForceMultiplier,ForceMode.Force);
    }

    public void Thrust(float throttle)
    {
        foreach (var thruster in _thrusters)
        {
            thruster.Activate(throttle);
        }

        var throttleVector = Vector3.forward * throttle * LinearForce.z;

        _rigidBody.AddRelativeForce(throttleVector * ForceMultiplier,ForceMode.Force);
    }
}
