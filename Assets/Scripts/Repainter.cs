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

    private void Repaint(GameObject gameObject)
    {
        Color randomColor = UnityEngine.Random.ColorHSV();
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material.color = randomColor;
    }
}
