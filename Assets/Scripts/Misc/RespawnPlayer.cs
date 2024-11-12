using System.Collections;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    // Variable to store the player's spawn position
    private Vector3 spawnPosition;
    private GameObject player;

    private void Start()
    {
        // Find the GameObject with the tag 'Player'
        player = GameObject.FindGameObjectWithTag("Player");

        // Get the player's current position and store it as the spawn position
        spawnPosition = player.transform.position;
    }

    // Call when another collider with the correct tag has collided with the object in this script
    private void OnTriggerEnter(Collider collider)
    {
        // Check if the collided object has the "Player" tag
        if (collider.gameObject.CompareTag("Player"))
        {
            // Teleport the player to the spawn location
            player.transform.position = spawnPosition;

            // Reset the player's rotation to (0, 0, 0)
            player.transform.rotation = Quaternion.Euler(0, 0, 0);

            // Freeze rotation on all axes
           // player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            // Start a coroutine to unfreeze rotation after a short delay
           // StartCoroutine(UnfreezeRotation());
        } else if (collider.gameObject.CompareTag("Ball"))
        {
            Destroy(collider.gameObject); // Destroy all balls that hit the bottom of the world
            //Debug.Log("Void caught ball");
        }
    }

    private IEnumerator UnfreezeRotation()
    {
        // Wait for a short moment to allow the player to stabilize
        yield return new WaitForSeconds(0.1f);

        // Allow the player to rotate again
        if (player.GetComponent<Rigidbody>() != null)
        {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
