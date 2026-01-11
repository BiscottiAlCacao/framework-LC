using System.Collections;
using UnityEngine;

public class BaseWeapons : ScriptableObject
{
    protected Player _player;

    [SerializeField] protected int _startingPower;
    [SerializeField] protected float _startingSpeedProjectile;
    [SerializeField] protected float _startingDuration;
    [SerializeField] protected float _startingArea;
    [SerializeField] protected float _startingCountdown;
    [SerializeField] protected float _startingProjectileLifeTime;

    public int realPower { get; private set; }
    public float realSpeedProjectile { get; private set; }
    public float realDuration { get; private set; }
    public float realArea { get; private set; }
    public float realCountdown { get; private set; }
    public float realProjectileLifeTime { get; private set; }

    public virtual void StartingTheWeapon(Player player)
    {
        _player = player;

        UpdateWeaponStats();

        CoroutineRunners.instance.StartCoroutine(CountdownWeapon());
    }

    public virtual void ActivateWeapon()
    {
        CoroutineRunners.instance.StartCoroutine(CountdownWeapon());
    }

    public virtual void UpdateWeaponStats()
    {
        realProjectileLifeTime = _startingProjectileLifeTime;
        realPower = (_startingPower * _player.weaponPowerPercent / 100);
        realSpeedProjectile = (_startingSpeedProjectile * _player.speedWeaponPercent / 100);
        realDuration = (_startingDuration * _player.durationWeaponPercent / 100);
        realArea = (_startingArea * _player.areaeffectWeaponPercent / 100);
        realCountdown = (_startingCountdown * _player.countdownWeaponPercent / 100);
    }

    public virtual IEnumerator CountdownWeapon()
    {
        yield return new WaitForSeconds(realCountdown);
        ActivateWeapon();
    }
}
