using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    private float _localExplosionRadius = 3f;
    private float _localExplosionForce = 500f;
    private float _startSize = 1f;
    private float _explosionForceMultiplier = 200f;

    public void ScatterLocalExplosion(List<Cube> cubes, Vector3 explosionPosition)
    {
        foreach (var cube in cubes)
        {   
            if (cube.TryGetComponent<Rigidbody>(out Rigidbody component))
            {
                component.AddExplosionForce(_localExplosionForce, explosionPosition, _localExplosionRadius);
            }
        }
    }

    public void ScatterGlobalExplosion(Vector3 explosionPosition, Collider collider)
    {
        foreach (Rigidbody explotableObject in GetExplodableObject(explosionPosition, collider))
            explotableObject.AddExplosionForce(GetExplosionForce(collider), explosionPosition, GetExplosionRadius(collider));

        Instantiate(_effect, explosionPosition, Quaternion.identity);
    }

    private List<Rigidbody> GetExplodableObject(Vector3 explosionPosition, Collider collider)
    {
        Collider[] hits = Physics.OverlapSphere(explosionPosition, GetExplosionRadius(collider));

        List<Rigidbody> cubes = new();

        foreach(Collider hit in hits)
            if(hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
            
    }

    private float GetExplosionRadius(Collider collider)
    {
        float size = collider.transform.localScale.x;
        return _startSize / size;
    }

    private float GetExplosionForce(Collider collider)
    {
        float size = collider.transform.localScale.x;
        return _startSize / size * _explosionForceMultiplier;
    }

}