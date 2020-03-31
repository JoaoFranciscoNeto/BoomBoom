using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SimpleField : MonoBehaviour
{
    public GameObject AsteroidPrefab;

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
            asteroid.transform.position = Vector3.Scale(Random.insideUnitSphere, _boundary / 2f);
            asteroid.transform.rotation = Random.rotation;
            asteroid.transform.localScale = Vector3.one * Random.Range(1f,3f);
            asteroid.transform.parent = transform;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, _boundary);
    }
}
