using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaffProjectiles : BaseProjectiles
{
    public LayerMask enemyLayer;
    private List<GameObject> _enemyList;
    private GameObject _enemyToChase;

    private Vector3 _moveDirection;

    private void Awake()
    {
        _enemyList = new List<GameObject>();

        Collider[] hits = Physics.OverlapSphere(transform.position, 100, enemyLayer);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                _enemyList.Add(hit.gameObject);
                Debug.Log(_enemyList.Count);
            }
        }

        if (_enemyList.Count == 0)
        {
            Destroy(gameObject);
        }
    }

    public override void GetStats(BaseWeapons t)
    {
        base.GetStats(t);
        FindNearEnemy();
    }

    private void FindNearEnemy()
    {
        float distToEnemy = Mathf.Infinity;
        for (int i = 0; i < _enemyList.Count; i++)
        {
            float dist = Vector3.Distance(transform.position, _enemyList[i].transform.position);

            if (dist < distToEnemy)
            {
                distToEnemy = dist;
                _enemyToChase = _enemyList[i];
            }
        }
    }

    private void Update()
    {
        if (_enemyToChase != null)
        {
            _moveDirection = (_enemyToChase.transform.position - transform.position).normalized;
        }

        transform.position += _moveDirection * speed * Time.deltaTime;
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

}
