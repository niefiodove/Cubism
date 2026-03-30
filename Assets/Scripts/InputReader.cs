using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const int LeftMouseButton = 0;

    public event Action LeftMouseButtonPressed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            LeftMouseButtonPressed?.Invoke();
        }
    }
}
