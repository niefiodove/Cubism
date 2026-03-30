using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

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

        foreach (var copy in creatingCopy)
        {   
            if (copy.TryGetComponent<Rigidbody>(out Rigidbody component))
            {
                component.AddExplosionForce(_explosionForce, explosionPosition, _explosionRadius);
            }
        }
    }
}