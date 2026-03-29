using UnityEngine;

public class SplitHandler : MonoBehaviour
{
    private int _minimumChance = 0;
    private int _maximumChance = 100;

    public bool IsSplittable { get; private set; }

    private void Start()
    {
        CalculateSplittable();
    }

    private void CalculateSplittable()
    {
        float chanceSeparation = GetComponent<Renderer>().bounds.size.y * 100;
        float randomChance = UnityEngine.Random.Range(_minimumChance, _maximumChance + 1);
        IsSplittable = chanceSeparation >= randomChance;
    }
}