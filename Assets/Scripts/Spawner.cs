using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CatcherTouch _catcherTouch;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Rigidbody _plane;
    [SerializeField] private int _numberCubes;
    [Range(1f, 5f)][SerializeField] private float _heightSpawn;

    private List<SplitHandler> _creatingCopy = new();

    public event Action<List<SplitHandler>, Vector3> ScatterExplosion;
    public event Action<Vector3> PassSizeCube;
    public event Action<Renderer> Repaint;

    private void OnEnable()
    {
        _catcherTouch.PassObject += OnObjectCatched;

    }

    private void OnDisable()
    {
        _catcherTouch.PassObject -= OnObjectCatched;
    }

    void Start()
    {
        for (int i = 0; i < _numberCubes; i++)
        {
            GameObject newCube = Instantiate(_cubePrefab, GetSpawnPosition(), Quaternion.identity);
            Color randomColor = UnityEngine.Random.ColorHSV();
            Renderer renderer = newCube.GetComponent<Renderer>();
            renderer.material.color = randomColor;
        }

        PassSizeCube?.Invoke(_cubePrefab.transform.localScale);
    }

    private Vector3 GetSpawnPosition()
    {
        Collider collider = _plane.GetComponent<Collider>();

        Bounds bounds = collider.bounds;

        float randomX = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(randomX, _heightSpawn, randomZ);
    }

    private void OnObjectCatched(SplitHandler splitHandler)
    {
        if (splitHandler.IsSplittable)
        {
            CreateCopies(splitHandler);
        }

        Destroy(splitHandler.transform.gameObject);
    }

    private void CreateCopies(SplitHandler splitHandler)
    {
        _creatingCopy.Clear();

        for (int i = 0; i < splitHandler.NumberCopies; i++)
        {
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * 0.1f;
            SplitHandler newCube = Instantiate(splitHandler, splitHandler.transform.localPosition + randomOffset, Quaternion.identity);
            newCube.transform.localScale /= 2;

            Renderer renderer = newCube.GetComponent<Renderer>();

            if (renderer != null)
                Repaint?.Invoke(renderer);

            _creatingCopy.Add(newCube);
        }

        ExplosionCall(_creatingCopy, splitHandler.transform.localPosition);
    }

    private void ExplosionCall(List<SplitHandler> splitHandlers, Vector3 explosionPosition)
    {
        ScatterExplosion?.Invoke(splitHandlers, explosionPosition);
    }
}
