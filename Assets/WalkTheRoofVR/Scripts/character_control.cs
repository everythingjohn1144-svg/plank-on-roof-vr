using UnityEngine;

public class character_control : MonoBehaviour
{

   [Header("Gravity")]
    public float gravity = -9.81f;
    public float groundedGravity = -2f;
 
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;
 
    [Header("VR Head Sync")]
    public Transform cameraTransform; // Drag the Main Camera (inside Camera Offset) here
    public float minValidCameraHeight = 0.3f; // Ignore camera readings below this (bad/uninitialized tracking)
 
    private CharacterController characterController;
    private Vector3 velocity;
    private bool onGround;
    private bool gravityEnabled = true; // toggled off while riding the lift
 
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
 
    void Update()
    {
        // --- Sync CharacterController to actual head height (VR-specific) ---
        // Only trust the camera's local Y if it looks like a real, initialized
        // tracking value. Prevents the capsule collapsing to the clamp minimum
        // during the first frame(s) before tracking/simulator reports a real height.
        if (cameraTransform != null && cameraTransform.localPosition.y > minValidCameraHeight)
        {
            float camHeight = Mathf.Clamp(cameraTransform.localPosition.y, 1.0f, 2.2f);
            characterController.height = camHeight;
            characterController.center = new Vector3(
                cameraTransform.localPosition.x,
                camHeight / 2f,
                cameraTransform.localPosition.z
            );
        }
 
        // --- While riding the lift, skip gravity/Move entirely so parenting alone drives position ---
        if (!gravityEnabled)
        {
            velocity.y = 0f;
            return;
        }
 
        onGround = Physics.CheckSphere(
            groundCheck.position,
            groundCheckDistance,
            groundLayer
        );
 
        if (onGround && velocity.y < 0)
        {
            velocity.y = groundedGravity;
        }
 
        velocity.y += gravity * Time.deltaTime;
 
        characterController.Move(velocity * Time.deltaTime);
    }
 
    // Called by lift_movement to suspend/resume gravity while riding
    public void SetGravityEnabled(bool enabled)
    {
        gravityEnabled = enabled;
        if (!enabled) velocity.y = 0f;
    }
 
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
 
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckDistance);
    }
}

