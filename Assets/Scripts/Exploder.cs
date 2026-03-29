using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private CubeMultiplier _cubeMultiplier;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private Vector3 _explosionPosition;

    private void OnEnable()
    {
        _cubeMultiplier.ScatterExplosion += ScatterExplosion;
    }

    private void OnDisable()
    {
        _cubeMultiplier.ScatterExplosion -= ScatterExplosion;
    }

    private void ScatterExplosion(List<GameObject> creatingCopy, Vector3 explosionPosition)
    {
        _explosionPosition = explosionPosition;

        foreach (var copy in creatingCopy)
        {
            Rigidbody rb = copy.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.AddExplosionForce(_explosionForce, _explosionPosition, _explosionRadius);
            }
        }
    }
}