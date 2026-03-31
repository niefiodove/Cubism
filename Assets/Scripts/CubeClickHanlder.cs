using UnityEngine;

public class CubeClickHanlder : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;

    private int _minimumChance = 0;
    private int _maximumChance = 100;
    private int _chanceMultiplier = 100;

    private void OnEnable()
    {
        _raycaster.ObjectCatched += OnCubeCatched;

    }

    private void OnDisable()
    {
        _raycaster.ObjectCatched -= OnCubeCatched;
    }

    private void OnCubeCatched(Cube cube)
    {
        if (cube.TryGetComponent<Renderer>(out Renderer renderer) && cube.TryGetComponent<Collider>(out Collider collider))
        {
            if (CalculateSplittable(renderer))
                _exploder.ScatterLocalExplosion(_spawner.CreateCopies(cube, DetermineNumberCopies()), renderer.gameObject.transform.position);
            else
                _exploder.ScatterGlobalExplosion(renderer.gameObject.transform.position, collider);
        }

        Destroy(cube.gameObject);
    }

    private bool CalculateSplittable(Renderer renderer)
    {
        float chanceSeparation = renderer.bounds.size.y * _chanceMultiplier;
        float randomChance = Random.Range(_minimumChance, _maximumChance + 1);
        return chanceSeparation >= randomChance;
    }

    private int DetermineNumberCopies()
    {
        int _minimumCopies = 2;
        int _maximumCopies = 6;
        return UnityEngine.Random.Range(_minimumCopies, _maximumCopies + 1);
    }

}
