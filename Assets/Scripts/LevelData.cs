using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Game/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public string levelName;
    public int totalWaves;
    public float timeBetweenWaves;
    public EnemyData[] enemyPool; // Enemies available for this level
}
