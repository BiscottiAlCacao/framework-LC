using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacters", menuName = "Scriptable Objects/PlayerCharacters")]
public class PlayerCharactersStats : ScriptableObject
{
    public int maxHp;
    public float regenHp;
    public int armor;
    public int movementSpeedPercent;
    public int weaponPowerPercent;
    public int speedWeaponPercent;
    public int durationWeaponPercent;
    public int areaeffectWeaponPercent;
    public int countdownWeaponPercent;
    public int magnetRangePercent;
}
