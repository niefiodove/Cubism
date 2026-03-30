using UnityEngine;

public class Cube : MonoBehaviour
{
    private SplitHandler _splitHandler;
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    public SplitHandler SplitHandler => _splitHandler;
    public Renderer Renderer => _renderer;
    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _splitHandler = gameObject.AddComponent<SplitHandler>();
        _rigidbody = gameObject.AddComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    public void SetColor()
    {
        Color randomColor = UnityEngine.Random.ColorHSV();
        _renderer.material.color = randomColor;
    }
}
