using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class Thruster : MonoBehaviour
{
    private TrailRenderer _trailRenderer;

    [SerializeField]
    private float _trailWidth;

    // Start is called before the first frame update
    void Start()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    public void Activate(float thrusterThrottle)
    {
        _trailRenderer.startWidth = thrusterThrottle * _trailWidth;
    }
}
