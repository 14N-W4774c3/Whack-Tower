using UnityEngine;

public class SpherePush : MonoBehaviour
{
    public float pushForce = 10f; // Force of the pushback

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player is the one colliding with the sphere
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                // Calculate the direction from the sphere to the player
                Vector3 pushDirection = collision.transform.position - transform.position;
                pushDirection.Normalize();

                // Apply the force in the direction away from the sphere
                playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
        }
    }
}