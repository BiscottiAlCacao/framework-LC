using UnityEngine;

public class BaseProjectiles : MonoBehaviour
{
    private int _power;
    private float _speedProjectile;
    private float _duration;
    private float _area;
    private float _countdown;
    private float _projectileLifeTime;

    public float speed { get; private set; }


    public virtual void GetStats(BaseWeapons t)
    {
        _power = t.realPower;
        _speedProjectile = t.realSpeedProjectile;
        _duration = t.realDuration;
        _area = t.realArea;
        _countdown = t.realCountdown;
        _projectileLifeTime = t.realProjectileLifeTime;

        speed = _speedProjectile;

        Destroy(gameObject, _projectileLifeTime);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(_power);
        }

    }
}
