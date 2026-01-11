using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Staff", menuName = "Scriptable Objects/Weapons/Staff")]
public class Staff : BaseWeapons
{
    [SerializeField] private StaffProjectiles prefabProjectileStaff;

    [SerializeField] private Vector2 spawnProjectileDistance;
    public override void StartingTheWeapon(Player player)
    {
        base.StartingTheWeapon(player);
    }
    public override void ActivateWeapon()
    {
        Vector3 positionSpawn = new Vector3
        (_player.transform.position.x + spawnProjectileDistance.x,
        _player.transform.position.y,
        _player.transform.position.z + spawnProjectileDistance.y);

        var go = Instantiate(prefabProjectileStaff, positionSpawn, Quaternion.identity);
        go.GetComponent<StaffProjectiles>().GetStats(this);
       

        base.ActivateWeapon();
    }

    public override void UpdateWeaponStats()
    {
        base.UpdateWeaponStats();
    }
    public override IEnumerator CountdownWeapon()
    {
        return base.CountdownWeapon();
    }
}