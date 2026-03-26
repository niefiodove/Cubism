using System;
using UnityEngine;

public class CatcherTouch : MonoBehaviour
{
    public event Action<GameObject> PassObject;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int mask = ~LayerMask.GetMask("Ignore");

            if (Physics.Raycast(ray, out hit, 10f, mask))
            {
                Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.gameObject.tag == "Cube")
                    PassObject?.Invoke(hit.transform.gameObject);
            }
        }
    }
}
