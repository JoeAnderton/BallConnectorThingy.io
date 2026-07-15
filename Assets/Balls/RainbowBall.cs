using UnityEngine;

public class RainbowBall : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float hue = Mathf.Repeat(Time.time * 0.2f, 1f);

        spriteRenderer.color = Color.HSVToRGB(hue, 1f, 1f);
    }
}
