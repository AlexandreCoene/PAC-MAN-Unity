using UnityEngine;

[DefaultExecutionOrder(-10)]
[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour
{
    public Movement movement { get; private set; }
    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }
    public GhostBehavior initialBehavior;
    public Transform target;
    public int points = 200;

    private void Awake() // Reference different scripts
    {
        movement = GetComponent<Movement>(); // This is a reference to the Movement script
        home = GetComponent<GhostHome>(); //...
        scatter = GetComponent<GhostScatter>();
        chase = GetComponent<GhostChase>();
        frightened = GetComponent<GhostFrightened>();
    }

    private void Start() // Reset the state of the ghost
    {
        ResetState(); // This method is called to reset the state of the ghost
    }

    public void ResetState() // This method is called when the game is reset
    {
        gameObject.SetActive(true); // Set the game object to active
        movement.ResetState(); // Reset the state of the movement

        frightened.Disable(); // Disable the frightened behavior
        chase.Disable(); // Disable the chase behavior
        scatter.Enable(); // Enable the scatter behavior

        if (home != initialBehavior) { // If the home behavior is not the initial behavior
            home.Disable(); // Disable the home behavior
        }

        if (initialBehavior != null) { // If the initial behavior is not null
            initialBehavior.Enable(); // Enable the initial behavior
        }
    }

    public void SetPosition(Vector3 position) // This method is called to set the position of the ghost
    {
        // Keep the z-position the same since it determines draw depth
        position.z = transform.position.z;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision) // This method is called when the ghost collides with something
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman")) // If the ghost collides with pacman
        {
            if (frightened.enabled) { // If the ghost is frightened
                GameManager.Instance.GhostEaten(this); // Call the GhostEaten method from the GameManager
            } else {
                GameManager.Instance.PacmanEaten(); // Call the PacmanEaten method from the GameManager
            }
        }
    }

}
