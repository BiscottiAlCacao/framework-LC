using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour , IPoolable
{
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] Player _player;
    public Rigidbody rb { get; private set; }

    private float _maxHP;
    private float _currentHP;
    private int _power;
    private float _movementSpeed;
    private float _knockback;
    private int _experience;

    private bool _haveAttacked;

    public Action<Enemy> OnDeath;


    private void GetStartigStats()
    {
        _maxHP = _enemyStats.maxHP;
        _currentHP = _maxHP;
        _power = _enemyStats.power;
        _movementSpeed = _enemyStats.movementSpeed;
        _knockback = _enemyStats.knockback;
        _experience = _enemyStats.experience;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = (_player.transform.position - rb.position).normalized * _movementSpeed;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && _haveAttacked == false)
        {
            _haveAttacked = true;
            StartCoroutine(StartCountdownAttack());
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHP -= damage;

        if ( _currentHP <= 0 )
        {
            OnDeath.Invoke(this);
        }
    }

    IEnumerator StartCountdownAttack()
    {
        _player.TakeDamage(_power);
        yield return new WaitForSeconds(0.5f);
        _haveAttacked = false;     
    }


    public void OnSpawn()
    {
        rb = GetComponent<Rigidbody>();
        _player = FindAnyObjectByType<Player>();
        GetStartigStats();
    }

    public void OnDespawn()
    {
        StopAllCoroutines();
    }
}