using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private CubeMultiplier _cubeMultiplier;

    private void OnEnable()
    {
        _cubeMultiplier.DestroyCube += DestroyMainCube;
    }

    private void OnDisable()
    {
        _cubeMultiplier.DestroyCube -= DestroyMainCube;
    }

    private void DestroyMainCube(GameObject oldCube)
    {
        Destroy(oldCube);
    }
}
