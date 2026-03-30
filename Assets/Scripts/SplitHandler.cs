using UnityEngine;

public class SplitHandler : MonoBehaviour
{
    private int _minimumChance = 0;
    private int _maximumChance = 100;
    private int _minimumCopies = 2;
    private int _maximumCopies = 6;
    private int _chanceMultiplier = 100;

    public bool IsSplittable { get; private set; }
    public int NumberCopies { get; private set; }

    private void Start()
    {
        CalculateSplittable();
        DetermineNumberCopies();
    }

    private void CalculateSplittable()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer == null)
        {
            IsSplittable = false;
            return;
        }

        float chanceSeparation = renderer.bounds.size.y * _chanceMultiplier;
        float randomChance = Random.Range(_minimumChance, _maximumChance + 1);
        IsSplittable = chanceSeparation >= randomChance;
    }

    private void DetermineNumberCopies()
    {
        NumberCopies = UnityEngine.Random.Range(_minimumCopies, _maximumCopies + 1);
    }
}