using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Rigidbody _plane;
    [SerializeField] private int _numberCubes;
    [SerializeField] private float _spawnOffsetRadius = 0.1f;
    [SerializeField] private float _copyScaleDivisor = 2f;
    [Range(1f, 5f)][SerializeField] private float _heightSpawn;

    private void Start()
    {
        for (int i = 0; i < _numberCubes; i++)
        {
            Cube newCube = Instantiate(_cubePrefab, GetSpawnPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Collider collider = _plane.GetComponent<Collider>();

        Bounds bounds = collider.bounds;

        float randomX = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(randomX, _heightSpawn, randomZ);
    }

    public List<Cube> CreateCopies(Cube cube, int numberCopies)
    {
        List<Cube> copies = new();
        for (int i = 0; i < numberCopies; i++)
        {
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * _spawnOffsetRadius;
            Cube newCube = Instantiate(cube, cube.transform.localPosition + randomOffset, Quaternion.identity);
            newCube.transform.localScale /= _copyScaleDivisor;
            copies.Add(newCube);
        }

        return copies;
    }
}
