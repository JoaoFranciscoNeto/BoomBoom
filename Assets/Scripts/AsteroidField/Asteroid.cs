using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Vector3 _constantRotation;
    public float RotationOffset = 50f;

    public float BaseSize = 20f;
    public float MinScaleRange = .8f;
    public float MaxScaleRange = 1.2f;

    void Start()
    {
        var localScale = Vector3.one;
        var baseScale = Random.Range(MinScaleRange, MaxScaleRange) * BaseSize;

        localScale.x = Random.Range(.9f,1.1f) * baseScale;
        localScale.y = Random.Range(.9f, 1.1f) * baseScale;
        localScale.z = Random.Range(.9f, 1.1f) * baseScale;
        transform.localScale = localScale;

        _constantRotation = Vector3.one;
        _constantRotation.x = Random.Range(-RotationOffset, RotationOffset);
        _constantRotation.y = Random.Range(-RotationOffset, RotationOffset);
        _constantRotation.z = Random.Range(-RotationOffset, RotationOffset);
    }

    void Update()
    {
        transform.Rotate(_constantRotation * Time.deltaTime);
    }
}
