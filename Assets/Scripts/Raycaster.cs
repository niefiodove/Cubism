using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private LayerMask _raycastLayers;
    [SerializeField] private float _raycastSize;

    public event Action<Cube> ObjectCatched;

    private void OnEnable()
    {
        _inputReader.LeftMouseButtonPressed += CatchObject;
    }

    private void OnDisable()
    {
        _inputReader.LeftMouseButtonPressed -= CatchObject;
    }

    private void CatchObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _raycastSize, _raycastLayers))
        {
            if (hit.transform.gameObject.TryGetComponent<Cube>(out Cube cube))
                ObjectCatched?.Invoke(cube);
        }
    }
}
