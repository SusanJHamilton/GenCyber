using UnityEngine;

public class MoveToPortal : MonoBehaviour
{
    public Transform portalTransform;
    public float moveSpeed = 3f;
    public float stoppingDistance = 0.3f;

    public MouseLook mouseLook; // Should be on the camera child

    private bool isMoving = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (mouseLook == null)
        {
            mouseLook = GetComponentInChildren<MouseLook>();
            if (mouseLook == null)
                Debug.LogWarning("[MoveToPortal] MouseLook script not found in children!");
            else
                Debug.Log("[MoveToPortal] MouseLook reference set from child.");
        }
    }

    public void StartMovingToPortal()
    {
        if (portalTransform == null)
        {
            Debug.LogError("[MoveToPortal] Portal transform not assigned!");
            return;
        }

        isMoving = true;

        if (mouseLook != null)
        {
            mouseLook.isInputEnabled = false;
            Debug.Log("[MoveToPortal] Disabled MouseLook input.");
        }
    }

    void FixedUpdate()
    {
        if (!isMoving || portalTransform == null)
            return;

        Vector3 toTarget = portalTransform.position - transform.position;
        toTarget.y = 0f; // Keep movement horizontal

        float distance = toTarget.magnitude;

        if (distance <= stoppingDistance)
        {
            rb.velocity = Vector3.zero;
            isMoving = false;

            if (mouseLook != null)
            {
                mouseLook.isInputEnabled = true;
                Debug.Log("[MoveToPortal] Re-enabled MouseLook input.");
            }

            return;
        }

        Vector3 direction = toTarget.normalized;
        Vector3 newPosition = transform.position + direction * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 0.5f * Time.fixedDeltaTime));
        }
    }
}
