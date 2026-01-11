using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Knife", menuName = "Scriptable Objects/Weapons/Knife")]
public class Knife : BaseWeapons
{
    [SerializeField] private KnifeProjectiles prefabProjectileKnife;

    [SerializeField] private Vector2 spawnProjectileDistance;
    public override void StartingTheWeapon(Player player)
    {
        base.StartingTheWeapon(player);
    }
    public override void ActivateWeapon()
    {
        var go = Instantiate(prefabProjectileKnife);
        go.GetComponent<KnifeProjectiles>().GetStats(this);
        go.transform.position = new Vector3
        (_player.transform.position.x + spawnProjectileDistance.x,
        _player.transform.position.y,
        _player.transform.position.z + spawnProjectileDistance.y);

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
