using System;
using UnityEngine;

public class CoroutineRunners : MonoBehaviour
{
    public static CoroutineRunners instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
