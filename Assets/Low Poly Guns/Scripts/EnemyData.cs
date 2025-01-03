using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemies/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float health;
    public float speed;
    public int scoreValue;
    public float attackRange;
    public float attackDamage;
    public GameObject projectilePrefab;  // For ranged enemies, if needed
    public float fireRate;  // For ranged enemies, if needed
}
