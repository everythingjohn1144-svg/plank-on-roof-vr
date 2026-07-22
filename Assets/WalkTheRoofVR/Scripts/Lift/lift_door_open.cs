using UnityEngine;
using UnityEngine.InputSystem;

public class lift_door_open : MonoBehaviour
{
    
  [Header("Door Configuration")]
    [Tooltip("Check this if this object is the Left Door. Uncheck for Right Door.")]
    public bool isLeftDoor = true;

    [Header("Door Movement Settings")]
    public float openDistance = 1.2f; // How far the door slides open locally
    public float speed = 2.0f;        // Speed of sliding

    private Vector3 closedLocalPos;
    private Vector3 openLocalPos;
    private Vector3 targetLocalPos;
    private bool isOpen = false;

    void Start()
    {
        // 1. Save initial LOCAL position (relative to parent Lift)
        closedLocalPos = transform.localPosition;

        // 2. Calculate local open position (Left = subtract local X, Right = add local X)
        float direction = isLeftDoor ? -1f : 1f;
        openLocalPos = new Vector3(
            closedLocalPos.x + (openDistance * direction), 
            closedLocalPos.y, 
            closedLocalPos.z
        );

        // Start closed
        targetLocalPos = closedLocalPos;
    }

    void Update()
    {
        // Press 'O' to open/close
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            ToggleDoor();
        }

        // Smoothly move door in LOCAL space
        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition, 
            targetLocalPos, 
            speed * Time.deltaTime
        );
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        targetLocalPos = isOpen ? openLocalPos : closedLocalPos;
    }
}
