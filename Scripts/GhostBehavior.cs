using UnityEngine;

[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost ghost { get; private set; }
    public float duration;

    private void Awake() // This method is called when the script is loaded
    {
        ghost = GetComponent<Ghost>(); // Get the Ghost component
    }

    public void Enable() // This method is called to enable the behavior
    {
        Enable(duration); // Call the Enable method with the duration
    } 

    public virtual void Enable(float duration) // This method is called to enable the behavior with a duration
    {
        enabled = true; // Enable the behavior

        CancelInvoke(); // Cancel any existing invokes
        Invoke(nameof(Disable), duration); // Invoke the Disable method after the duration
    }

    public virtual void Disable() // This method is called to disable the behavior
    {
        enabled = false; // Disable the behavior

        CancelInvoke(); 
    }

}
