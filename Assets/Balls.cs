using Unity.VisualScripting;
using UnityEngine;

public class Balls : MonoBehaviour
{
    public GameObject nextBall;
    public int ballNumber;
    public bool canCombine = true;
    public bool fromPlayer = false;
    public bool messageSent = false;
    public int pointValue;
    public SpriteRenderer spriteRenderer;
    public CircleCollider2D circleCollider;
    public BoxCollider2D boxCollider;
    public Rigidbody2D rigidBody;
    public Sprite square;
    public UpgradeButtons downgradeManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        if (!fromPlayer)
        {
            FindAnyObjectByType<Player>().playerScore += pointValue * FindAnyObjectByType<Player>().scoreMultiplier;
        }
            Physics2D.IgnoreLayerCollision(3,6,true);
        downgradeManager = FindAnyObjectByType<UpgradeButtons>();
    }

    // Update is called once per frame
    void Update()
    {
        if (downgradeManager.squares)
        {
            turnToSquares();
        }
    }

    private void LateUpdate()
    {
        //canCombine = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (downgradeManager.nice)
        {
            FindAnyObjectByType<Player>().PlayBallMix();
        }
        Physics2D.IgnoreLayerCollision(3, 6, false);
        if (FindAnyObjectByType<Player>().currentBallNumber - 1 == ballNumber && fromPlayer && collision.gameObject.layer != 7 && !messageSent)
        {
            FindAnyObjectByType<Player>().canFire = true;
            messageSent = true;
        }
        if ((collision.gameObject.tag == tag || collision.gameObject.tag == "RainbowBall" || (tag == "RainbowBall" && collision.gameObject.tag != "floor")) && canCombine && !FindAnyObjectByType<Player>().isGameOver)
        {
            if (ballNumber > collision.gameObject.GetComponent<Balls>().ballNumber)
            {
                collision.gameObject.GetComponent<Balls>().canCombine = false;
            }
            if (tag == "RainbowBall" && collision.gameObject.tag != "RainbowBall")
            {
                nextBall = collision.gameObject.GetComponent<Balls>().nextBall;
            }
            if (ballNumber > collision.gameObject.GetComponent<Balls>().ballNumber && nextBall != gameObject)
            {
                canCombine = false;
                Vector3 spawnPosition = ((GetComponent<Transform>().position + collision.gameObject.GetComponent<Transform>().position) / 2 + Vector3.up * 0.1f);
                GameObject thisBall = Instantiate(nextBall, spawnPosition, Quaternion.identity);
                FindAnyObjectByType<Player>().PlayBallMix();
                thisBall.GetComponent<Balls>().ballNumber = ballNumber;
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else if (ballNumber > collision.gameObject.GetComponent<Balls>().ballNumber && (collision.gameObject.tag == tag || collision.gameObject.tag == "RainbowBall" || tag == "RainbowBall") && nextBall == gameObject)
            {
                canCombine = false;
                FindAnyObjectByType<Player>().playerScore += pointValue * FindAnyObjectByType<Player>().scoreMultiplier;
                FindAnyObjectByType<Player>().PlayNoWay();
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Physics2D.IgnoreLayerCollision(3, 6, false);
        if (FindAnyObjectByType<Player>().currentBallNumber - 1 == ballNumber && fromPlayer && collision.gameObject.layer != 7 && !messageSent)
        {
            FindAnyObjectByType<Player>().canFire = true;
            messageSent = true;
        }
        if ((collision.gameObject.tag == tag || collision.gameObject.tag == "RainbowBall" || (tag == "RainbowBall" && collision.gameObject.tag != "floor")) && canCombine && !FindAnyObjectByType<Player>().isGameOver)
        {
            collision.gameObject.GetComponent<Balls>().canCombine = false;
            if (tag == "RainbowBall" && collision.gameObject.tag != "RainbowBall")
            {
                nextBall = collision.gameObject.GetComponent<Balls>().nextBall;
            }
            if (ballNumber > collision.gameObject.GetComponent<Balls>().ballNumber && nextBall != gameObject)
            {
                canCombine = false;
                Vector3 spawnPosition = ((GetComponent<Transform>().position + collision.gameObject.GetComponent<Transform>().position) / 2 + Vector3.up * 0.1f);
                GameObject thisBall = Instantiate(nextBall, spawnPosition, Quaternion.identity);
                FindAnyObjectByType<Player>().PlayBallMix();
                thisBall.GetComponent<Balls>().ballNumber = ballNumber;
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else if (ballNumber > collision.gameObject.GetComponent<Balls>().ballNumber && (collision.gameObject.tag == tag || collision.gameObject.tag == "RainbowBall" || tag == "RainbowBall") && nextBall == gameObject)
            {
                canCombine = false;
                FindAnyObjectByType<Player>().playerScore += pointValue * FindAnyObjectByType<Player>().scoreMultiplier;
                FindAnyObjectByType<Player>().PlayNoWay();
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player" && (!fromPlayer||messageSent)) || collision.gameObject.layer == 7)
        {
            FindAnyObjectByType<Player>().isGameOver = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player" && (!fromPlayer || messageSent)) || collision.gameObject.layer == 7)
        {
            FindAnyObjectByType<Player>().isGameOver = true;
        }
    }
    public void turnToSquares()
    {
        spriteRenderer.sprite = square;
        //rigidBody.freezeRotation = true;
        boxCollider.enabled = true;
        circleCollider.enabled = false;
    }
}
