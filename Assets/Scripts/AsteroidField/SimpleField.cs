using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SimpleField : MonoBehaviour
{
    public GameObject AsteroidPrefab;
    public GameObject[] AsteroidGraphics;

    [SerializeField]
    private int _nAsteroids = 100;

    [SerializeField]
    private Vector3 _boundary = new Vector3(100,100,100);

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _nAsteroids; i++)
        {
            var asteroid = GameObject.Instantiate(AsteroidPrefab);

            var asteroidGraphics = GameObject.Instantiate(AsteroidGraphics[Random.Range(0, AsteroidGraphics.Length)]);
            asteroidGraphics.transform.parent = asteroid.transform;

            asteroid.transform.position = Vector3.Scale(Random.insideUnitSphere, _boundary / 2f);
            asteroid.transform.parent = transform;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, _boundary);
    }
}
