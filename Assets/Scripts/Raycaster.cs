using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private LayerMask _raycastLayers;
    [SerializeField] private float _raycastSize;

    public event Action<SplitHandler> ObjectCatched;

    private void OnEnable()
    {
        _inputReader.Leftmousebuttonispressed += CatchedObject;
    }

    private void OnDisable()
    {
        _inputReader.Leftmousebuttonispressed -= CatchedObject;
    }

    private void CatchedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _raycastSize, _raycastLayers))
        {
            if (hit.transform.gameObject.TryGetComponent<SplitHandler>(out SplitHandler splitHandler))
                ObjectCatched?.Invoke(splitHandler);
        }
    }
}
