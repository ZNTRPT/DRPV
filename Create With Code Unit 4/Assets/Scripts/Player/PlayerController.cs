using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float speed = 50.0f;
    private Rigidbody playerRigidbody;
    private GameObject powerupIndicator;
    private GameObject focalPoint;
    public int lives = 3;

    [Header("Powerup variables")]
    [SerializeField] private bool hasPowerup = false;
    [SerializeField] private float powerupStrength = 15.0f;
    [SerializeField] private float PowerupTime = 7.0f;

    [Header("Handling death")]
    [SerializeField] private Text LivesText;
    [SerializeField] private GameObject FailedPanel;
    #region BASIC CONTROLS


    /// <summary>
    /// The start function will be run when the object first starts in the scene. 
    /// It is often used to initialize a script on an object by assigning any variables that need to be assigned.
    /// </summary>
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");

        //Powerup Indicator will start enabled so that we can find it. After getting it we can disable the object so we dont see it.
        //There a lot of different ways to do this. 
        powerupIndicator = GameObject.Find("Powerup Indicator");
        powerupIndicator.SetActive(false);

        UpdateUILives();
    }

    /// <summary>
    /// The Update funtion will occur every frame, its often used to handle things that need to be checked often, 
    /// however try to avoid using it for everything as you may find that it will slow down your game.
    /// </summary>
    private void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRigidbody.AddForce(focalPoint.transform.forward * speed * forwardInput * Time.deltaTime);

        powerupIndicator.transform.position = this.transform.position + new Vector3(0, -0.5f, 0);//we minus from the y to get it to stay on the ground

        if (this.transform.position.y < -10)
        {
            ReSpawn();
        }
    }

    /// <summary>
    /// This function is used to control what will happen when the player respawns.
    /// Common things to find are resetting values of the player so they dont spawn in moving, or if they have a gun to refill it.
    /// </summary>
    private void ReSpawn()
    {
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.angularVelocity = Vector3.zero;
        this.transform.position = new Vector3(0, 10, 0);
        lives--;
        UpdateUILives();
        if (lives == 0)
        {
            Fail();
        }
    }


    #endregion

    #region MANAGE COLLISIONS
    /// <summary>
    /// Triggers are things you can hit but wont effect you physically, like a ghost.
    /// They are often used to trigger certain events, here we are using them to check when we have picked up a powerup.
    /// </summary>
    /// <param name="other"> The other collider involved in this trigger event</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUP"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    /// <summary>
    /// This IEnumerator is used to wait for a set amount of time then disable the powerup on the player.
    /// </summary>
    /// <returns></returns>
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(PowerupTime);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    /// <summary>
    /// This OnCollisionEnter is used to check with any object with a collider that is NOT a trigger to see if it is an enemy. 
    /// If it is and the powerup is active then it will push the enemy it collided with away.
    /// </summary>
    /// <param name="collision">The other collider involved in this collision event</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - this.transform.position);

            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    #endregion

    #region UI
    /// <summary>
    /// Just update the number of lives shown to the player.
    /// </summary>
    private void UpdateUILives()
    {
        LivesText.text = "Lives: " + lives;
    }

    /// <summary>
    /// When the player dies and has no lives left what should happen?
    /// </summary>
    private void Fail()
    {
        FailedPanel.SetActive(true);
        // Setting the timescale to 0 will pause the games physics engine, effectivly pausing the game.
        // Setting this back to 1 will unpause the engine.
        Time.timeScale = 0f; 
    }
    #endregion
}
