using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeMultiplier : MonoBehaviour
{
    [SerializeField] private CatcherTouch _catcherTouch;

    private GameObject _gameObject;
    private int _minimumCopies = 2;
    private int _maximumCopies = 6;
    private List<GameObject> _creatingCopy = new();

    public event Action<GameObject> Repaint;
    public event Action<List<GameObject>> ScatterExplosion;

    private void OnEnable()
    {
        _catcherTouch.PassObject += OnObjectReceived;
    }

    private void OnDisable()
    {
        _catcherTouch.PassObject -= OnObjectReceived;
    }

    private void OnObjectReceived(GameObject gameObject)
    {
        _gameObject = gameObject;

        if (gameObject.transform.gameObject.TryGetComponent<SplitHandler>(out SplitHandler splitHandler) && splitHandler.IsSplittable)
        {
            CreateCopies();
            ScatterExplosion?.Invoke(_creatingCopy);
            _creatingCopy.Clear();
        }

        Destroy(gameObject);
    }

    private int DetermineNumberCopies()
    {
        return UnityEngine.Random.Range(_minimumCopies, _maximumCopies + 1);
    }

    private void CreateCopies()
    {
        for (int i = 0; i < DetermineNumberCopies(); i++)
        {
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * 0.1f;
            GameObject newCube = Instantiate(_gameObject, _gameObject.transform.localPosition + randomOffset, Quaternion.identity);
            newCube.transform.localScale /= 2;
            Repaint?.Invoke(newCube);
            _creatingCopy.Add(newCube);
        }
    }
}