using System.Collections.Generic;
using UnityEngine;

public class BasePool<T> where T : Component
{
    protected T Prefab { get; private set; }
    protected Transform Parent { get; private set; }
    private readonly Queue<T> _pool = new Queue<T>();


    public BasePool(T prefab, int prewarmCount = 0)
    {
        Prefab = prefab;

        for (int i = 0; i < prewarmCount; i++)
        {
            AddToPool(CreateInstance());
        }
    }


    protected virtual T CreateInstance()
    {
        return Object.Instantiate(Prefab);
    }


    protected virtual void OnGet(T instance)
    {
        if (instance is IPoolable poolable)
            poolable.OnSpawn();
    }


    protected virtual void OnReturn(T instance)
    {
        if (instance is IPoolable poolable)
            poolable.OnDespawn();
    }


    public void AddToPool(T instance)
    {
        instance.gameObject.SetActive(false);
        instance.transform.SetParent(Parent);
        _pool.Enqueue(instance);
    }

    public T GetPoolable(Vector3 position, Quaternion rotation)
    {
        T instance = _pool.Count > 0 ? _pool.Dequeue() : CreateInstance();

        instance.transform.SetPositionAndRotation(position, rotation);
        instance.gameObject.SetActive(true);
        OnGet(instance);

        return instance;
    }
}
