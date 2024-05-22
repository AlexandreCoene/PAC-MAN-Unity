using UnityEngine;

public class PowerPellet : Pellet
{
    public float duration = 8f; // The duration of the power pellet effect

    protected override void Eat() // This method is called when the power pellet is eaten
    {
        GameManager.Instance.PowerPelletEaten(this); // Notify the game manager that the power pellet was eaten
    }

}
