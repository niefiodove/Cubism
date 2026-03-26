using System.Collections.Generic;
using UnityEngine;

public class CubeMultiplier : MonoBehaviour
{
    [SerializeField] private CatcherTouch _catcherTouch;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private GameObject _gameObject;
    private Vector3 _normalSize = Vector3.one;
    private float _startShansDuplicate = 100f;
    private int _minimumCopies = 2;
    private int _maximumCopies = 6;
    private List<Rigidbody> _explodableObjects;

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

        if (DoNeedReproduce())
        {
            CreateCopies();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private bool DoNeedReproduce()
    {
        float chanceReproduction = _normalSize.x * _gameObject.transform.localScale.x * 100;
        return Random.Range(0, _startShansDuplicate + 1) <= chanceReproduction;
    }

    private int DetermineNumberCopies()
    {
        return Random.Range(_minimumCopies, _maximumCopies + 1); 
    }

    private void CreateCopies()
    {
        Color randomColor = Random.ColorHSV();

        for (int i = 0; i < DetermineNumberCopies(); i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * 0.1f;
            GameObject newCube = Instantiate(_gameObject, _gameObject.transform.localPosition + randomOffset, Quaternion.identity);
            newCube.transform.localScale /= 2;
            Renderer renderer = newCube.GetComponent<Renderer>();
            renderer.material.color = randomColor;
            newCube.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, _gameObject.transform.localPosition, _explosionRadius);
        }
    }
}
