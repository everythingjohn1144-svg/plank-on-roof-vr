using UnityEngine;
using UnityEngine.InputSystem;

public class lift_movement : MonoBehaviour
{
 //[Header("Floor Targets")]
  //  public Transform floor0;
  //  public Transform floor1;
  //  public Transform floor2;
  //  public Transform floor3;
  /*  public Transform floor4;
    public Transform floor5;

    [Header("Lift Settings")]
    public float speed = 3.0f;
    public float rotationSpeed = 90.0f; // Degrees per second

    private Vector3 targetPosition;
    private float defaultYAngle; // Stores your original -77.9° rotation
    private float targetYAngle;
    private bool isHeadingToFloorZero = false;

    void Start()
    {
        targetPosition = transform.position;
        
        // Save the lift's initial Y angle from Inspector (e.g., -77.9°)
        defaultYAngle = transform.eulerAngles.y;
        targetYAngle = defaultYAngle;
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // Key inputs
        if (Keyboard.current.digit0Key.wasPressedThisFrame) SetTargetY(floor0, isFloorZero: true);
        if (Keyboard.current.digit1Key.wasPressedThisFrame) SetTargetY(floor1, isFloorZero: false);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) SetTargetY(floor2, isFloorZero: false);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) SetTargetY(floor3, isFloorZero: false);
        if (Keyboard.current.digit4Key.wasPressedThisFrame) SetTargetY(floor4, isFloorZero: false);
        if (Keyboard.current.digit5Key.wasPressedThisFrame) SetTargetY(floor5, isFloorZero: false);

        // Check if position movement is complete
        bool hasArrivedAtPosition = Vector3.Distance(transform.position, targetPosition) < 0.01f;

        // 1. ARRIVED AT FLOOR 0: Rotate 180° relative to default orientation
        if (isHeadingToFloorZero && hasArrivedAtPosition)
        {
            targetYAngle = defaultYAngle + 180f;
        }

        // Check if rotation to current target angle is complete
        float currentYAngle = transform.eulerAngles.y;
        bool isRotationFinished = Mathf.Abs(Mathf.DeltaAngle(currentYAngle, targetYAngle)) < 0.5f;

        // 2. MOVEMENT: Move first if heading to Floor 0. Otherwise wait for rotation back to defaultYAngle.
        if (isHeadingToFloorZero || isRotationFinished)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                targetPosition, 
                speed * Time.deltaTime
            );
        }

        // 3. ROTATION: Smoothly rotate toward target angle on Y axis
        float nextYAngle = Mathf.MoveTowardsAngle(
            currentYAngle, 
            targetYAngle, 
            rotationSpeed * Time.deltaTime
        );

        transform.rotation = Quaternion.Euler(0f, nextYAngle, 0f);
    }

    private void SetTargetY(Transform targetFloor, bool isFloorZero)
    {
        if (targetFloor != null)
        {
            targetPosition = new Vector3(transform.position.x, targetFloor.position.y, transform.position.z);
        }

        isHeadingToFloorZero = isFloorZero;

        // If leaving Floor 0 (heading to floors 1-5), rotate back to default orientation (-77.9°)
        if (!isFloorZero)
        {
            targetYAngle = defaultYAngle;
        }
    }
 */

 /*public void GoToFloor(int floorNumber)
{
    Transform target = floorNumber switch
    {
        0 => floor0,
        1 => floor1,
        2 => floor2,
        3 => floor3,
        4 => floor4,
        5 => floor5,
        _ => null
    };

    if (target == null) return;

    SetTargetY(target, isFloorZero: (floorNumber == 0));
}*/
}
