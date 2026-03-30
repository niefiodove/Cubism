using UnityEngine;

public class Cube : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        SetColor();
    }

    private void SetColor()
    {
        Color randomColor = UnityEngine.Random.ColorHSV();
        _renderer.material.color = randomColor;
    }
}
