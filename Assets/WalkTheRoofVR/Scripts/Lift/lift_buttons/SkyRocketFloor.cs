using UnityEngine;

public class SkyRocketFloor : MonoBehaviour
{
     public lift_movement lift;
    public int floorNumber;
    public string handTag = "Hand";
    public float cooldown = 1.0f;

    private bool isCoolingDown = false;

    private void OnTriggerStay(Collider other)
    {
        if (isCoolingDown) return;
        if (!other.CompareTag(handTag)) return;
        if (lift == null)
        {
            Debug.LogWarning("Lift reference not assigned on " + gameObject.name);
            return;
        }

        Debug.Log($"Triggered by: {other.name}, tag: {other.tag}");

        lift.GoToFloor(floorNumber);
        isCoolingDown = true;
        Invoke(nameof(ResetCooldown), cooldown);
    }

    private void ResetCooldown() => isCoolingDown = false;
}
