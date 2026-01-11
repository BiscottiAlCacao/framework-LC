using UnityEngine;

public class KnifeProjectiles : BaseProjectiles
{
    public override void GetStats(BaseWeapons t)
    {
        base.GetStats(t);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
