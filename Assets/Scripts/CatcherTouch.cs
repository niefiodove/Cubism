using System;
using UnityEngine;

public class CatcherTouch : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

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

        int mask = ~LayerMask.GetMask("Ignore Raycast");

        if (Physics.Raycast(ray, out hit, 20f, mask))
        {
            if (hit.transform.gameObject.TryGetComponent<SplitHandler>(out SplitHandler splitHandler))
                PassObject?.Invoke(splitHandler);
        }
    }
}
