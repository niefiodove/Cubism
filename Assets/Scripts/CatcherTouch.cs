using System;
using UnityEngine;

public class CatcherTouch : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private LayerMask _raycastLayers;
    [SerializeField] private float _raycastSize;

    public event Action<SplitHandler> PassObject;

    private void OnEnable()
    {
        _inputReader.UserInput += PassCube;
    }

    private void OnDisable()
    {
        _inputReader.UserInput -= PassCube;
    }

    private void PassCube()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _raycastSize, _raycastLayers))
        {
            if (hit.transform.gameObject.TryGetComponent<SplitHandler>(out SplitHandler splitHandler))
                PassObject?.Invoke(splitHandler);
        }
    }
}
