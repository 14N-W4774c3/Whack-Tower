using UnityEngine;

public class SphereCleanup : MonoBehaviour
{
    public float deleteHeight = -10f; // Height threshold below which the sphere will be destroyed

    void Update()
    {
        // Check if the sphere's Y position is below the threshold
        if (transform.position.y < deleteHeight)
        {
            Destroy(gameObject); // Destroy the sphere
        }
    }
}