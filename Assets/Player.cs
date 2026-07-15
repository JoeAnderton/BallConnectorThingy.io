using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static Player;

public class Player : MonoBehaviour
{
    public GameObject currentBall;
    public GameObject ballAfterThat;
    public GameObject ballAfterThatDisplay;
    public GameObject smallBall;
    public GameObject mediumBall;
    public GameObject largeBall;
    public GameObject veryLargeBall;
    public GameObject purpleBall;
    public GameObject rainbowBall;
    public GameObject blackBall;
    public GameObject container;
    public Transform leftPoint;
    public Transform rightPoint;
    public TextMeshProUGUI score;
    public TextMeshProUGUI nextBallText;
    public Transform blackOut;
    public TextMeshProUGUI gameOver;
    public TextMeshProUGUI gameOverScore;
    public TextMeshProUGUI restartPrompt;
    public int currentBallNumber = 1;
    public float playerScore = 0;
    public bool hasBlackBall = false;
    public int blackBallChance = 20;
    bool fired = false;
    public bool hasRainbow = false;
    public bool canFire = true;
    public bool isGameOver = false;
    public AudioSource NoWay;
    public AudioSource ballMix;
    public AudioSource backgroundMusic;
    public AudioClip nice;
    public float scoreMultiplier = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int ballType = Random.Range(1, 3);
        if (playerScore > 40 && !hasRainbow)
        {
            ballType = Random.Range(1, 4);
        }
        else if (hasRainbow)
        {
            ballType = Random.Range(1, 4);
            if (Random.Range(1, 21) == 1)
            {
                ballAfterThat = rainbowBall;
                ballType = 0;
            }
        }
        if (ballType == 1)
        {
            ballAfterThat = smallBall;
        }
        if (ballType == 2)
        {
            ballAfterThat = mediumBall;
        }
        if (ballType == 3)
        {
            ballAfterThat = largeBall;
        }
        nextBall();
        backgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            score.text = playerScore.ToString();
            if ((!FindAnyObjectByType<UpgradeButtons>().choosingUpgrades && FindAnyObjectByType<UpgradeButtons>().upgradeMode) || !FindAnyObjectByType<UpgradeButtons>().upgradeMode)
            {
                Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, Mathf.Abs(Camera.main.transform.position.z)));
                if (currentBall == smallBall)
                {
                    mouseWorldPos.x = Mathf.Clamp(mouseWorldPos.x, leftPoint.position.x + leftPoint.localScale.x, rightPoint.position.x - rightPoint.localScale.x);
                }
                else
                {
                    mouseWorldPos.x = Mathf.Clamp(mouseWorldPos.x, leftPoint.position.x + leftPoint.localScale.x + (currentBall.transform.localScale.x / 2 - transform.localScale.x / 2 + 0.01f), rightPoint.position.x - rightPoint.localScale.x - currentBall.transform.localScale.x / 2 + transform.localScale.x / 2 - 0.01f);
                }
                transform.position = new Vector3(mouseWorldPos.x, transform.position.y, transform.position.z);
            }
        }
        else if (isGameOver)
        {
            backgroundMusic.Stop();
            gameOverScore.text = "You scored "+playerScore.ToString();
            score.gameObject.SetActive(false);
            FindAnyObjectByType<UpgradeButtons>().currentBallDisplay.gameObject.SetActive(false);
            blackOut.gameObject.SetActive(true);
            gameOver.gameObject.SetActive(true);
            gameOverScore.gameObject.SetActive(true);
            restartPrompt.gameObject.SetActive(true);
        }
    }

    public void SpawnBall(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 1 && !fired && canFire && !isGameOver)
        {
            GameObject thisBall = Instantiate(currentBall, GetComponent<Transform>().position, Quaternion.identity);
            thisBall.GetComponent<Balls>().ballNumber = currentBallNumber;
            currentBallNumber++;
            nextBall();
            thisBall.GetComponent<Balls>().fromPlayer = true;
            fired = true;
            canFire = false;
        }
        else if (isGameOver && context.ReadValue<float>() == 1)
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            fired = false;
        }
    }
    void nextBall()
    {
        currentBall = ballAfterThat;
        int ballType = Random.Range(1, 3);
        if (playerScore > 40 && !hasRainbow)
        {
            ballType = Random.Range(1, 4);
            if (Random.Range(1, blackBallChance + 1) == 1 && hasBlackBall)
            {
                ballAfterThat = blackBall;
                ballType = 0;
            }
        }
        else if (hasRainbow)
        {
            ballType = Random.Range(1, 4);
            if (Random.Range(1, 21) == 1)
            {
                ballAfterThat = rainbowBall;
                ballType = 0;
            }
        }
        if (ballType == 1)
        {
            ballAfterThat = smallBall;
        }
        if (ballType == 2)
        {
            ballAfterThat = mediumBall;
        }
        if (ballType == 3)
        {
            ballAfterThat = largeBall;
        }
    }
    public void PlayNoWay()
    {
        NoWay.Play();
    }

    public void PlayBallMix()
    {
        ballMix.PlayOneShot(nice);
    }
}
