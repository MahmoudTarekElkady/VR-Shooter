using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyData[] enemyDataArray;

    public void SpawnEnemy(EnemyData enemyData)
    {
        Vector3 randomPosition = GetRandomSpawnPosition();
        Instantiate(enemyData.enemyPrefab, randomPosition, Quaternion.identity);
    }

    public EnemyData GetRandomEnemyData()
    {
        return enemyDataArray[Random.Range(0, enemyDataArray.Length)];
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float radius = 10f; // Adjust as needed
        Vector2 randomCircle = Random.insideUnitCircle * radius;
        return new Vector3(transform.position.x + randomCircle.x, transform.position.y, transform.position.z + randomCircle.y);
    }
}
