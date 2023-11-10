using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private int score = 0;
    [SerializeField] private int waveNumber = 0;
    [SerializeField, Range(5,10)] private float spawnRange = 9; //Set to 9 to match the size of the platform.

    [Header("Enemy Objects")]
    [SerializeField] private int enemyCount = 0;
    [SerializeField] List<GameObject> enemyPrefabs;

    [Header("Powerup Objects")]
    [SerializeField] private GameObject powerupPrefab;
    
    [Header("UI Component")]
    [SerializeField] private Text ScoreText;
    
    /// <summary>
    /// Every frame we need to check if we have run out of enemies to kill.
    /// </summary>
    private void Update()
    {
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPoint(), powerupPrefab.transform.rotation);
        }
    }
    
    /// <summary>
    /// Here we are randomly picking 2 ints to make our coordinates to spawn an object. This is used to spawn both enemies and powerups.
    /// </summary>
    /// <returns></returns>
    public Vector3 GenerateSpawnPoint()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    #region Spawn Enemies
    
    /// <summary>
    /// Spawns a number of enemies equal to the wave number we're on. 
    /// </summary>
    /// <param name="_enemiesToSpawn"></param>
    private void SpawnEnemyWave(int _enemiesToSpawn)
    {
        for (int i = 0; i < _enemiesToSpawn; i++)
        {
            Instantiate(ChooseRandomEnemy(), GenerateSpawnPoint(), Quaternion.identity);
            enemyCount++; //this is a nice short hand for "enemyCount = enemyCount + 1;"
        }
    }
    
    /// <summary>
    /// Since we have multiple enemies that we can spawn we're just spawning a random one.
    /// This is one area that could probably be improved but may seem simpler than it really is.
    /// </summary>
    /// <returns></returns>
    private GameObject ChooseRandomEnemy()
    {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Count-1)];
    }

    /// <summary>
    /// When an enemy dies we need to update the score and our enemy count so we know how many are left.
    /// </summary>
    public void EnemyDied()
    {
        enemyCount--;
        score++;
        UpdateUIScore();
    }
    
    #endregion

    #region Pickups

    #endregion

    #region UI
    public void UpdateUIScore()
    {
        ScoreText.text = "Score: " + score;
    }
    #endregion
}
