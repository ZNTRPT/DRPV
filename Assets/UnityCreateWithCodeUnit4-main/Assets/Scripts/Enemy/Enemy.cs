using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    private Rigidbody enemyRigidbody;
    private GameObject player;

    /// <summary>
    /// We are getting the Ridgidbody of the object and finding a reference to the player object.
    /// </summary>
    private void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    /// <summary>
    /// Every frame we want to add a little force to the enemy to push it towards the player.
    /// </summary>
    private void Update()
    {
        Vector3 lookDirection = (player.transform.position - this.transform.position).normalized;
        enemyRigidbody.AddForce(lookDirection * speed * Time.deltaTime);

        if (this.transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        //When the project ends it will destroy the SpawnManager before all the enemies. This try will prevent an error from appearing when ending the game.
        try
        {
            FindObjectOfType<SpawnManager>().EnemyDied();
        }
        catch { } //Nothing needs to be caught here as the only time this will occur is when closing the game.
    }
}
