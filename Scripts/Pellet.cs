using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pellet : MonoBehaviour
{
    public int points = 10; // The number of points the pellet is worth

    protected virtual void Eat() // This method is called when the pellet is eaten
    {
        GameManager.Instance.PelletEaten(this); // Notify the game manager that the pellet was eaten
    }

    private void OnTriggerEnter2D(Collider2D other) // This method is called when the pellet collides with something    
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman")) { // If the pellet collides with pacman
            Eat();
        }
    }

}
