using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private Exploder _exploder;

    private void OnEnable()
    {
        _exploder.DestroyCube += DestroyMainCube;
    }

    private void OnDisable()
    {
        _exploder.DestroyCube -= DestroyMainCube;
    }

    private void DestroyMainCube(GameObject oldCube)
    {
        Destroy(oldCube);
    }
}
