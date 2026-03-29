using UnityEngine;

public class Repainter : MonoBehaviour
{
    [SerializeField] private CubeMultiplier _cubeMultiplier;

    private void OnEnable()
    {
        _cubeMultiplier.Repaint += Repaint;
    }

    private void OnDisable()
    {
        _cubeMultiplier.Repaint -= Repaint;
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
