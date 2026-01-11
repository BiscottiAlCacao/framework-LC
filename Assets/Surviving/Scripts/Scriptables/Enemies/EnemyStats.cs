using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public int maxHP;
    public int power;
    public int movementSpeed;
    public float knockback;
    public int experience;
}
