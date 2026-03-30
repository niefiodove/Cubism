using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void ScatterExplosion(List<Cube> cubes, Vector3 explosionPosition)
    {

        foreach (var cube in cubes)
        {   
            if (cube.TryGetComponent<Rigidbody>(out Rigidbody component))
            {
                component.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
            }
        }
    }
}