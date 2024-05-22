using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour // This class is used to animate a sprite
{
    public Sprite[] sprites = new Sprite[0]; // An array of sprites to animate 
    public float animationTime = 0.25f; // The time between each frame of the animation
    public bool loop = true; // Whether or not the animation should loop

    private SpriteRenderer spriteRenderer; // The sprite renderer component
    private int animationFrame; // The current frame of the animation

    private void Awake() // This method is called when the object is initialized
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer component
    }

    private void OnEnable() // This method is called when the object is enabled
    {
        spriteRenderer.enabled = true; // Enable the sprite renderer
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), animationTime, animationTime);
    }

    private void Advance() // This method is called every animationTime seconds
    {
        if (!spriteRenderer.enabled) { // If the sprite renderer is disabled, we don't need to advance the animation
            return;
        }

        animationFrame++; // Advance the animation frame

        if (animationFrame >= sprites.Length && loop) { // If the animation frame is greater than or equal to the number of sprites and we're looping
            animationFrame = 0;
        }

        if (animationFrame >= 0 && animationFrame < sprites.Length) { // If the animation frame is within the bounds of the sprites array
            spriteRenderer.sprite = sprites[animationFrame]; // Set the sprite renderer's sprite to the current frame
        }
    }

    public void Restart() // This method is called when the animation needs to be restarted
    {
        animationFrame = -1; 

        Advance();
    }

}
