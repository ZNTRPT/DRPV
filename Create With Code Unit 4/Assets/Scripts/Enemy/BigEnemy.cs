using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy// we are inheriting from the normal enemy to give this one extra functionality
{
    [SerializeField] private GameObject smallEnemyPrefab;

    /// <summary>
    /// This function is simple as its just used to spawn a small enemy when this one dies.
    /// However something important to know is that since this script is inheriting from the Enemy one it is replacing the Enemy.OnDestroy function to do this.
    /// If you want to make it do both you need to make OnDestroy a Protected function in both classes then put "base.OnDestroy();" where you want it to do the base function
    /// </summary>
    private void OnDestroy()
    {
        if (Time.timeScale !=0 )//make sure the game isnt paused like it is at the end
        {
            Vector3 spawnPoint = new Vector3(0, 10, 0);
            Instantiate(smallEnemyPrefab, spawnPoint, smallEnemyPrefab.transform.rotation);
        }
    }
}
