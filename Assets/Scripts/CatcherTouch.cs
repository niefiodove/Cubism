using System;
using UnityEngine;

public class CatcherTouch : MonoBehaviour
{
    public event Action<GameObject> PassObject;

    private const int _leftMouseButton = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
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
}
