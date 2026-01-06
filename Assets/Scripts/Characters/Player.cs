using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public  PlayerCharactersStats characterStats;

    public List<BaseWeapons> weapons;

    public Rigidbody rb { get; private set; }
    private int _maxHp;
    private float _currentHP;
    private float _regenHp;
    private int _armor;
    private float _magnetRange = 1;

    public float movementSpeed { get; private set; } = 1;
    public int weaponPowerPercent { get; private set; }
    public int speedWeaponPercent { get; private set; }
    public int durationWeaponPercent { get; private set; }
    public int areaeffectWeaponPercent { get; private set; }
    public int countdownWeaponPercent { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GetStartigStats();
    }

    private void Start()
    {
        StartWeapons();
        RegenHealth();
    }

    private void StartWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].StartingTheWeapon(this);
        }
    }

    private void GetStartigStats()
    {
        _maxHp = characterStats.maxHp;
        _currentHP = _maxHp;
        _regenHp = characterStats.regenHp;

        _armor = characterStats.armor;

        movementSpeed += (1 * characterStats.movementSpeedPercent / 100);
        _magnetRange += (1 * characterStats.magnetRangePercent / 100);

        weaponPowerPercent = characterStats.weaponPowerPercent;
        speedWeaponPercent = characterStats.speedWeaponPercent;
        durationWeaponPercent = characterStats.durationWeaponPercent;
        areaeffectWeaponPercent = characterStats.areaeffectWeaponPercent;
        countdownWeaponPercent = characterStats.countdownWeaponPercent;
    }

    public void TakeDamage(float damage)
    {
        float trueDamage = damage - _armor;
        if (trueDamage > 0)
        {
            _currentHP -= damage;
        }

        if (_currentHP <= 0)
        {
            GameManager.instance.ChangeState(new LoseState(GameManager.instance));
        }
    }

    private void RegenHealth()
    {
        if (_currentHP < _maxHp)
        {
            _currentHP += _regenHp;
        }

        if (_currentHP > _maxHp)
        {
            _currentHP = _maxHp;
        }

        StartCoroutine(RegenHealthTimer());
    }


    IEnumerator RegenHealthTimer()
    {
        yield return new WaitForSeconds(1);
        RegenHealth();
    }


}
