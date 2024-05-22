using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Passage : MonoBehaviour
{
    public Transform connection;

    private void OnTriggerEnter2D(Collider2D other) // This method is called when the pellet collides with something
    {
        Vector3 position = connection.position; // Set the position of the connection
        position.z = other.transform.position.z; // Keep the z-position the same since it determines draw depth
        other.transform.position = position; // Set the position of the other object to the position of the connection
    }

}
