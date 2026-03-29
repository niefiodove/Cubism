using System;
using Unity.VisualScripting;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    public event Action<GameObject> PassObject;

    private const int _leftMouseButton = 0;

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
                    PassObject?.Invoke(hit.transform.gameObject);
            }
    }
}
