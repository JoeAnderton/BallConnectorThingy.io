using UnityEngine;

public enum UpgradeType
{
    ScoreMulti,
    Floor,
    Rainbow,
    Purple,
    NextBall,
    Blind,
    Bouncy,
    FloorMove,
    FasterDowngrade,
    Squares,
    Nice,
    Black,
    BlackIncrease
}
[CreateAssetMenu(fileName = "Upgrades", menuName = "Scriptable Objects/Upgrades")]
public class Upgrades : ScriptableObject
{
    public string upgradeName;
    public string description;

    public UpgradeType upgradeType;
}
