using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action UserInput;

    private const int _leftMouseButton = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
        {
            UserInput?.Invoke();
        }
    }
}
