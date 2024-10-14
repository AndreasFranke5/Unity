using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public float speed = 5f;
    public GameObject powerupIndicator;
    private Powerup activePowerup;
    public float collisionForce = 15.0f; // Default collision force


    void Start()
{
    playerRb = GetComponent<Rigidbody>();
    playerRb.mass = 1.0f; // Set the default mass to 1
    focalPoint = GameObject.Find("Focal Point");
}

    void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(forwardInput * speed * focalPoint.transform.forward);
        float horizontalInput = Input.GetAxis("Horizontal2");
        playerRb.AddForce(horizontalInput * speed * focalPoint.transform.right);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Powerup powerup = other.GetComponent<Powerup>();
            if (powerup != null)
            {
                ActivatePowerup(powerup);
                Destroy(other.gameObject); // Destroy the powerup object after pickup
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Enemy"))
    {
        Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
        
        // Apply a force to the enemy to push it away
        enemyRigidbody.AddForce(awayFromPlayer.normalized * collisionForce, ForceMode.Impulse);
        
        Debug.Log("Collided with " + collision.gameObject.name + " using mass: " + GetComponent<Rigidbody>().mass);
    }
}


    private void ActivatePowerup(Powerup powerup)
    {
        if (activePowerup != null)
        {
            activePowerup.DeactivateEffect(this);
        }

        activePowerup = powerup;
        powerupIndicator.SetActive(true);
        StartCoroutine(PowerupRoutine(powerup));
    }

    IEnumerator PowerupRoutine(Powerup powerup)
    {
        powerup.ApplyEffect(this);
        yield return new WaitForSeconds(powerup.duration);
        powerup.DeactivateEffect(this);
        activePowerup = null;
        powerupIndicator.SetActive(false);
    }
}
