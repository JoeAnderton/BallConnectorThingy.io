using UnityEngine;
using UnityEngine.UIElements;

public class NextBall : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField]
    bool doingNextBall = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!doingNextBall)
        {
            sr.color = FindAnyObjectByType<Player>().currentBall.GetComponent<SpriteRenderer>().color;
            if (FindAnyObjectByType<Player>().currentBall.gameObject.tag == "RainbowBall")
            {
                float hue = Mathf.Repeat(Time.time * 0.2f, 1f);

                sr.color = Color.HSVToRGB(hue, 1f, 1f);
            }
            transform.localScale = FindAnyObjectByType<Player>().currentBall.gameObject.transform.localScale;
        }
        else
        {
            sr.color = FindAnyObjectByType<Player>().ballAfterThat.GetComponent<SpriteRenderer>().color;
            if (FindAnyObjectByType<Player>().ballAfterThat.gameObject.tag == "RainbowBall")
            {
                float hue = Mathf.Repeat(Time.time * 0.2f, 1f);

                sr.color = Color.HSVToRGB(hue, 1f, 1f);
            }
            transform.localScale = FindAnyObjectByType<Player>().ballAfterThat.gameObject.transform.localScale;
        }
        if (FindAnyObjectByType<UpgradeButtons>().squares)
        {
            sr.sprite = FindAnyObjectByType<Balls>().square;
        }
    }
}
