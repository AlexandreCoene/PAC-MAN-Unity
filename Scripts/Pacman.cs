using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    [SerializeField] // This attribute exposes the variable in the inspector
    private AnimatedSprite deathSequence; // The animated sprite for the death sequence
    private SpriteRenderer spriteRenderer; // The sprite renderer component
    private Movement movement; // The movement component
    private new Collider2D collider; // The collider component

    private void Awake() // This method is called when the object is initialized
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer component
        movement = GetComponent<Movement>(); // Get the movement component
        collider = GetComponent<Collider2D>(); // Get the collider component
    }

    private void Update()
    {
        // Set the new direction based on the current input
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)) {
            movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            movement.SetDirection(Vector2.right);
        }

        // Rotate pacman to face the movement direction
        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x); // Get the angle of the direction
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward); // Rotate the object to face the direction
    }

    public void ResetState() // This method is called when the game is reset
    {
        enabled = true; // Enable the pacman script
        spriteRenderer.enabled = true; // Enable the sprite renderer
        collider.enabled = true; 
        deathSequence.enabled = false; 
        movement.ResetState(); 
        gameObject.SetActive(true); 
    }

    public void DeathSequence() // This method is called when pacman dies
    {
        enabled = false;
        spriteRenderer.enabled = false;
        collider.enabled = false;
        movement.enabled = false;
        deathSequence.enabled = true;
        deathSequence.Restart();
    }

}
