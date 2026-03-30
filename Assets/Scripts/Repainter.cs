using UnityEngine;

public class Repainter : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _spawner.Repaint += Repaint;
    }

    private void OnDisable()
    {
        _spawner.Repaint -= Repaint;
    }

    private void Repaint(Renderer renderer)
    {
        if (renderer != null)
        {
            Color randomColor = Random.ColorHSV();
            renderer.material.color = randomColor;
        }
    }
}
