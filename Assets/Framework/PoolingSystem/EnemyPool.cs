using UnityEngine;

public class EnemyPool : BasePool<Enemy>
{
    private int _maxActive;
    public EnemyPool(Enemy prefab, int prewarmCount, int maxActive) : base(prefab, prewarmCount) 
    {
        _maxActive = maxActive;
    }
 
    public int ActiveCount { get; private set; }

    protected override void OnGet(Enemy instance)
    {
        base.OnGet(instance);
        ActiveCount++;
    }

    protected override void OnReturn(Enemy instance)
    {
        base.OnReturn(instance);
        ActiveCount--;
    }
}
