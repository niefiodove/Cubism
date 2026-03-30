using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private Vector3 _explosionPosition;

    private void OnEnable()
    {
        _spawner.ScatterExplosion += ScatterExplosion;
    }

    private void OnDisable()
    {
        _spawner.ScatterExplosion -= ScatterExplosion;
    }

    private void ScatterExplosion(List<SplitHandler> creatingCopy, Vector3 explosionPosition)
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