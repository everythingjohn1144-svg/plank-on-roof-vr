using UnityEngine;
using UnityEngine.InputSystem;

public class lift_movement : MonoBehaviour
{
 /*[Header("Floor Targets")]
    public Transform ground_floor;
    public Transform top_floor;
    public Transform rocket_floor;
    public Transform plank_floor;

    [Header("Lift Settings")]
    public float speed = 3.0f;
    public float rotationSpeed = 90.0f; // Degrees per second

    [Header("Player")]
    public Transform player; // Drag your XR Origin (VR) here

    private Vector3 targetPosition;
    private float defaultYAngle;
    private float targetYAngle;
    private bool isHeadingToFloorZero = false;
    private bool isMoving = false; // tracks whether lift is currently mid-transit

    void Start()
    {
        targetPosition = transform.position;
        defaultYAngle = transform.eulerAngles.y;
        targetYAngle = defaultYAngle;
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.digit0Key.wasPressedThisFrame) SetTargetY(ground_floor, isFloorZero: true);
        if (Keyboard.current.digit1Key.wasPressedThisFrame) SetTargetY(rocket_floor, isFloorZero: false);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) SetTargetY(plank_floor, isFloorZero: false);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) SetTargetY(top_floor, isFloorZero: false);

        bool hasArrivedAtPosition = Vector3.Distance(transform.position, targetPosition) < 0.01f;

        if (isHeadingToFloorZero && hasArrivedAtPosition)
        {
            targetYAngle = defaultYAngle + 180f;
        }

        float currentYAngle = transform.eulerAngles.y;
        bool isRotationFinished = Mathf.Abs(Mathf.DeltaAngle(currentYAngle, targetYAngle)) < 0.5f;

        if (isHeadingToFloorZero || isRotationFinished)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                speed * Time.deltaTime
            );
        }

        float nextYAngle = Mathf.MoveTowardsAngle(
            currentYAngle,
            targetYAngle,
            rotationSpeed * Time.deltaTime
        );

        transform.rotation = Quaternion.Euler(0f, nextYAngle, 0f);

        // --- Player parenting logic ---
        bool arrivedNow = Vector3.Distance(transform.position, targetPosition) < 0.01f
                           && Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetYAngle)) < 0.5f;

        bool shouldBeMoving = !arrivedNow;

        if (shouldBeMoving && !isMoving)
        {
            // Just started moving -> parent player to lift
            if (player != null)
            {
                player.SetParent(transform, worldPositionStays: true);
            }
            isMoving = true;
        }
        else if (!shouldBeMoving && isMoving)
        {
            // Just arrived -> unparent player back to world
            if (player != null)
            {
                player.SetParent(null, worldPositionStays: true);
            }
            isMoving = false;
        }
    }

    private void SetTargetY(Transform targetFloor, bool isFloorZero)
    {
        if (targetFloor != null)
        {
            targetPosition = new Vector3(transform.position.x, targetFloor.position.y, transform.position.z);
        }

        isHeadingToFloorZero = isFloorZero;

        if (!isFloorZero)
        {
            targetYAngle = defaultYAngle;
        }
    }

    public void GoToFloor(int floorNumber)
    {
        Transform target = floorNumber switch
        {
            0 => ground_floor,
            1 => rocket_floor,
            2 => plank_floor,
            3 => top_floor,
            _ => null
        };

        if (target == null) return;

        SetTargetY(target, isFloorZero: (floorNumber == 0));
    } */

     [Header("Floor Targets")]
    public Transform ground_floor;
    public Transform top_floor;
    public Transform rocket_floor;
    public Transform plank_floor;

    [Header("Lift Settings")]
    public float speed = 3.0f;
    public float rotationSpeed = 90.0f; // Degrees per second

    [Header("Player")]
    public Transform player; // Drag your XR Origin (VR) here

    [Header("Doors")]
    public lift_door_open leftDoor;
    public lift_door_open rightDoor;

    private Vector3 targetPosition;
    private float defaultYAngle;
    private float targetYAngle;
   // private bool isHeadingToFloorZero = false;
    private bool isHeadingToRotateFloor = false;
    private bool isMoving = false; // tracks whether lift is currently mid-transit

    void Start()
    {
        targetPosition = transform.position;
        defaultYAngle = transform.eulerAngles.y;
        targetYAngle = defaultYAngle;
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.digit0Key.wasPressedThisFrame) SetTargetY(ground_floor, isRotateFloor: true);
        if (Keyboard.current.digit1Key.wasPressedThisFrame) SetTargetY(rocket_floor, isRotateFloor: false);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) SetTargetY(plank_floor, isRotateFloor: false);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) SetTargetY(top_floor, isRotateFloor: true);

        bool hasArrivedAtPosition = Vector3.Distance(transform.position, targetPosition) < 0.01f;

        // Once position is reached on a "rotate floor" (ground_floor or top_floor), rotate 180 from default
        if (isHeadingToRotateFloor && hasArrivedAtPosition)
        {
            targetYAngle = defaultYAngle + 180f;
        }

        float currentYAngle = transform.eulerAngles.y;
        bool isRotationFinished = Mathf.Abs(Mathf.DeltaAngle(currentYAngle, targetYAngle)) < 0.5f;

        // Move first if heading to a rotate floor. Otherwise wait for rotation back to default first.
        if (isHeadingToRotateFloor || isRotationFinished)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                speed * Time.deltaTime
            );
        }

        float nextYAngle = Mathf.MoveTowardsAngle(
            currentYAngle,
            targetYAngle,
            rotationSpeed * Time.deltaTime
        );

        transform.rotation = Quaternion.Euler(0f, nextYAngle, 0f);

        // --- Player parenting + door logic ---
        bool arrivedNow = Vector3.Distance(transform.position, targetPosition) < 0.01f
                           && Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetYAngle)) < 0.5f;

        bool shouldBeMoving = !arrivedNow;

        if (shouldBeMoving && !isMoving)
        {
            if (player != null)
            {
                player.SetParent(transform, worldPositionStays: true);
            }
            CloseDoors();
            isMoving = true;
        }
        else if (!shouldBeMoving && isMoving)
        {
            if (player != null)
            {
                player.SetParent(null, worldPositionStays: true);
            }
            OpenDoors();
            isMoving = false;
        }
    }

    private void OpenDoors()
    {
        if (leftDoor != null) leftDoor.Open();
        if (rightDoor != null) rightDoor.Open();
    }

    private void CloseDoors()
    {
        if (leftDoor != null) leftDoor.Close();
        if (rightDoor != null) rightDoor.Close();
    }

    private void SetTargetY(Transform targetFloor, bool isRotateFloor)
    {
        if (targetFloor != null)
        {
            targetPosition = new Vector3(transform.position.x, targetFloor.position.y, transform.position.z);
        }

        isHeadingToRotateFloor = isRotateFloor;

        // If heading to a non-rotate floor (rocket/plank), rotate back to default orientation first
        if (!isRotateFloor)
        {
            targetYAngle = defaultYAngle;
        }
    }

    public void GoToFloor(int floorNumber)
    {
        Transform target = floorNumber switch
        {
            0 => ground_floor,
            1 => rocket_floor,
            2 => plank_floor,
            3 => top_floor,
            _ => null
        };

        if (target == null) return;

        bool isRotateFloor = (floorNumber == 0 || floorNumber == 3); // ground_floor and top_floor
        SetTargetY(target, isRotateFloor: isRotateFloor);
    }
}
