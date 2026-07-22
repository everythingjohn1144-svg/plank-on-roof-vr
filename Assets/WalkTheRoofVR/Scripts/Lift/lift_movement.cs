using UnityEngine;
using UnityEngine.InputSystem;

public class lift_movement : MonoBehaviour
{
   
      [Header("Floor Targets")]
    public Transform ground_floor;
    public Transform top_floor;
    public Transform rocket_floor;
    public Transform plank_floor;

    [Header("Lift Settings")]
    public float speed = 3.0f;
    public float rotationSpeed = 90.0f;

    [Header("Player")]
    public Transform player;

    [Header("Doors")]
    public lift_door_open leftDoor;
    public lift_door_open rightDoor;

    private Vector3 targetPosition;
    private float defaultYAngle;
    private float targetYAngle;
    private bool isHeadingToRotateFloor = false;
    private bool isMoving = false;

    private character_control playerControl; // NEW

    void Start()
    {
        targetPosition = transform.position;
        defaultYAngle = transform.eulerAngles.y;
        targetYAngle = defaultYAngle;

        if (player != null)
        {
            playerControl = player.GetComponent<character_control>(); // NEW
        }
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.digit0Key.wasPressedThisFrame) SetTargetY(ground_floor, isRotateFloor: true);
        if (Keyboard.current.digit1Key.wasPressedThisFrame) SetTargetY(rocket_floor, isRotateFloor: false);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) SetTargetY(plank_floor, isRotateFloor: false);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) SetTargetY(top_floor, isRotateFloor: true);

        bool hasArrivedAtPosition = Vector3.Distance(transform.position, targetPosition) < 0.01f;

        if (isHeadingToRotateFloor && hasArrivedAtPosition)
        {
            targetYAngle = defaultYAngle + 180f;
        }

        float currentYAngle = transform.eulerAngles.y;
        bool isRotationFinished = Mathf.Abs(Mathf.DeltaAngle(currentYAngle, targetYAngle)) < 0.5f;

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
            if (playerControl != null) playerControl.SetGravityEnabled(false); // NEW
            CloseDoors();
            isMoving = true;
        }
        else if (!shouldBeMoving && isMoving)
        {
            if (player != null)
            {
                player.SetParent(null, worldPositionStays: true);
            }
            if (playerControl != null) playerControl.SetGravityEnabled(true); // NEW
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

        bool isRotateFloor = (floorNumber == 0 || floorNumber == 3);
        SetTargetY(target, isRotateFloor: isRotateFloor);
    }
}

       