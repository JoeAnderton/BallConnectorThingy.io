using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class UpgradeButtons : MonoBehaviour
{
    public Player player;
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public GameObject currentBallDisplay;
    public GameObject container;
    public TextMeshProUGUI score;
    public TextMeshProUGUI titleScreen;
    public TextMeshProUGUI currentBallText;
    public PhysicsMaterial2D friction;
    public Upgrades floorMoveFaster;
    public Upgrades blackBallIncrease;
    public bool upgradeMode = false;
    public bool isGameStarted = false;
    public bool choosingUpgrades = false;
    public int upgradeScore = 50;
    int upgradeScoreIncreaser = 500;
    public Transform upgradeSelection;
    public List<Upgrades> allUpgrades;
    public bool squares = false;
    public bool nice = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetRandomUpgrades();
        friction.bounciness = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted)
        {
            titleScreen.gameObject.SetActive(false);
            score.gameObject.SetActive(true);
            currentBallDisplay.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
            container.gameObject.SetActive(true);
            isGameStarted = false;
        }
        if (player.playerScore >= upgradeScore && upgradeMode)
        {
            player.canFire = false;
            GetRandomUpgrades();
            upgradeSelection.gameObject.SetActive(true);
            upgradeScore += upgradeScoreIncreaser;
            choosingUpgrades = true;
        }
    }
    public List<Upgrades> GetRandomUpgrades()
    {
        List<Upgrades> choices = new List<Upgrades>();

        while (choices.Count < 3)
        {
            Upgrades randomUpgrade = allUpgrades[Random.Range(0, allUpgrades.Count)];

            if (!choices.Contains(randomUpgrade))
            {
                choices.Add(randomUpgrade);
            }
        }
        Button1.GetComponent<Buttons>().upgrade = choices[0].upgradeType;
        Button1.GetComponent<Buttons>().description = choices[0].description;
        Button1.GetComponent<Buttons>().upgradePlace = choices[0];
        Button2.GetComponent<Buttons>().upgrade = choices[1].upgradeType;
        Button2.GetComponent<Buttons>().description = choices[1].description;
        Button2.GetComponent<Buttons>().upgradePlace = choices[1];
        Button3.GetComponent<Buttons>().upgrade = choices[2].upgradeType;
        Button3.GetComponent<Buttons>().description = choices[2].description;
        Button3.GetComponent<Buttons>().upgradePlace = choices[2];
        return choices;
    }
    public void doUpgrade(UpgradeType upgrade, Upgrades upgradePlace)
    {
        switch (upgrade)
        {
            case UpgradeType.ScoreMulti:
                player.scoreMultiplier += 1;
                break;
            case UpgradeType.Floor:
                FindAnyObjectByType<Floor>().floorGrow();
                break;
            case UpgradeType.Rainbow:
                player.hasRainbow = true;
                allUpgrades.Remove(upgradePlace);
                break;
            case UpgradeType.Purple:
                player.currentBall = player.purpleBall;
                break;
            case UpgradeType.NextBall:
                player.ballAfterThatDisplay.gameObject.SetActive(true);
                player.nextBallText.gameObject.SetActive(true);
                allUpgrades.Remove(upgradePlace);
                break;
            case UpgradeType.Blind:
                currentBallDisplay.gameObject.SetActive(false);
                currentBallText.gameObject.SetActive(false);
                allUpgrades.Remove(upgradePlace);
                break;
            case UpgradeType.Bouncy:
                friction.bounciness += 0.3f;
                if (friction.bounciness >= 1)
                {
                    allUpgrades.Remove(upgradePlace);
                }
                break;
            case UpgradeType.FloorMove:
                if (FindAnyObjectByType<Floor>().canMove)
                {
                    //FindAnyObjectByType<Container>().speed += 0.001f;
                    FindAnyObjectByType<Floor>().speed += 0.01f;
                }
                //FindAnyObjectByType<Container>().canMove = true;
                FindAnyObjectByType<Floor>().canMove = true;
                allUpgrades.Remove(upgradePlace);
                allUpgrades.Add(floorMoveFaster);
                break;
            case UpgradeType.FasterDowngrade:
                upgradeScoreIncreaser -= upgradeScoreIncreaser / 2;
                allUpgrades.Remove(upgradePlace);
                break;
            case UpgradeType.Squares:
                squares = true;
                allUpgrades.Remove(upgradePlace);
                break;
            case UpgradeType.Nice:
                nice = true;
                allUpgrades.Remove(upgradePlace);
                break;
            case UpgradeType.Black:
                player.hasBlackBall = true;
                allUpgrades.Remove(upgradePlace);
                allUpgrades.Add(blackBallIncrease);
                break;
            case UpgradeType.BlackIncrease:
                player.blackBallChance += 1;
                if (player.blackBallChance == 5)
                {
                    allUpgrades.Remove(upgradePlace);
                }
                break;
        }
        upgradeSelection.gameObject.SetActive(false);
        player.canFire = true;
        choosingUpgrades = false;
    }
}
