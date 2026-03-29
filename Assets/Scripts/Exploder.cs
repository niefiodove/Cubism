using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private CubeMultiplier _cubeMultiplier;
    [SerializeField] private CatcherTouch _catcherTouch;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private Vector3 _explosionPosition;
    private GameObject _oldCube;

    public event Action<GameObject> DestroyCube;

    private void OnEnable()
    {
        _cubeMultiplier.ScatterExplosion += ScatterExplosion;
        _catcherTouch.PassObject += GetExplosionPosition;
    }

    private void OnDisable()
    {
        _cubeMultiplier.ScatterExplosion -= ScatterExplosion;
        _catcherTouch.PassObject -= GetExplosionPosition;        
    }

    private void ScatterExplosion(List<GameObject> creatingCopy)
    {
        foreach (var copy in creatingCopy)
            copy.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, _explosionPosition, _explosionRadius);

        DestroyCube?.Invoke(_oldCube);
    }

    private void GetExplosionPosition(GameObject gameObject)
    {
        _oldCube = gameObject;
        _explosionPosition = gameObject.transform.position;
    }
}
