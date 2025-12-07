using UnityEngine;

public class Grabber : MonoBehaviour
{
    [Header("Setup")]
    public Camera grabCamera;
    public LayerMask pickableMask;      // Grabbable objects layer(s) (e.g. "Interactive")
    public LayerMask collisionMask;     // Environment layers (e.g. "Default", "Ground", "Walls")
    public PlayerMovement pMove;

    [Header("Distances")]
    public float maxPickupDistance = 100f;
    public float minHoldDistance = 1f;
    public float maxHoldDistance = 100f;

    private Transform grabbedTransform;
    private Rigidbody grabbedRb;

    // Scale & distance at pickup
    private Vector3 startingLocalScale;
    private float startingDistance;

    void Update()
    {
        HandleInput();
        UpdateHeldObject();

        // Enable or disable PlayerMovement
        if (pMove != null)
        {
            pMove.enabled = grabbedTransform == null;
        }
    }

    void HandleInput()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        // DROP
        if (grabbedTransform != null)
        {
            grabbedRb.isKinematic = false;
            grabbedTransform = null;
            grabbedRb = null;
            return;
        }

        // PICKUP
        Ray ray = grabCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, maxPickupDistance, pickableMask))
        {
            grabbedTransform = hit.collider.transform;
            grabbedRb = grabbedTransform.GetComponent<Rigidbody>();
            if (grabbedRb == null)
            {
                grabbedTransform = null;
                return;
            }

            grabbedRb.isKinematic = true;

            startingLocalScale = grabbedTransform.localScale;
            startingDistance = Vector3.Distance(grabCamera.transform.position, grabbedTransform.position);
            if (startingDistance < 0.01f)
                startingDistance = 0.01f;
        }
    }

    void UpdateHeldObject()
    {
        if (grabbedTransform == null)
            return;

        Ray ray = grabCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        float desiredDistance = startingDistance;
        Vector3 targetPos;

        // Raycast against environment to find where we are looking
        if (Physics.Raycast(ray, out RaycastHit hit, maxHoldDistance, collisionMask))
        {
            // Clamp distance along view ray
            desiredDistance = Mathf.Clamp(hit.distance, minHoldDistance, maxHoldDistance);

            // Base center position on the ray
            Vector3 center = ray.origin + ray.direction * desiredDistance;

            // Get collider and its half-extents in world space
            Collider col = grabbedTransform.GetComponent<Collider>();
            if (col != null)
            {
                // Approximate half-extents from bounds
                Vector3 halfExtents = col.bounds.extents;

                // How far the object extends along the hit normal
                float extentAlongNormal = Mathf.Abs(
                    Vector3.Dot(hit.normal.normalized, halfExtents.normalized)
                ) * halfExtents.magnitude;

                // Push the object out of the wall by its extent
                targetPos = hit.point + hit.normal.normalized * extentAlongNormal;
            }
            else
            {
                // Fallback: just use center
                targetPos = center;
            }
        }
        else
        {
            // Nothing hit: just place it along the ray at previous distance
            desiredDistance = Mathf.Clamp(desiredDistance, minHoldDistance, maxHoldDistance);
            targetPos = ray.origin + ray.direction * desiredDistance;
        }

        // Scale factor relative to original pickup distance
        float scaleFactor = desiredDistance / startingDistance;
        Vector3 proposedScale = startingLocalScale * scaleFactor;

        grabbedTransform.position = targetPos;
        grabbedTransform.localScale = proposedScale;
    }
}
