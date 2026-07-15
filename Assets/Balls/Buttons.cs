using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class Buttons : MonoBehaviour
{
    public TextMeshProUGUI upgradeDes;
    public string description;
    public UpgradeType upgrade;
    public Upgrades upgradePlace;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        upgradeDes.text = description;
    }
    public void ApplyUpgrade()
    {
        FindAnyObjectByType<UpgradeButtons>().doUpgrade(upgrade, upgradePlace);
    }

    public void initiateUpgradeMode()
    {
        FindAnyObjectByType<UpgradeButtons>().upgradeMode = true;
        FindAnyObjectByType<UpgradeButtons>().isGameStarted = true;
    }

    public void initiateClassicMode()
    {
        FindAnyObjectByType<UpgradeButtons>().upgradeMode = false;
        FindAnyObjectByType<UpgradeButtons>().isGameStarted = true;
    }
}
