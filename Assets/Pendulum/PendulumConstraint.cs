using UnityEngine;

public class PendulumConstraint : MonoBehaviour
{
    public Transform pivotPoint;   // The fixed joint pivot point
    public Transform orbTransform; // The orb's transform
    public Transform stringTransform; // The string's transform
    public float checkInterval = 0.5f; // Time interval for checking the joint state
    public float maxAngularVelocity = 5.0f; // Maximum allowed angular velocity

    private Rigidbody orbRigidbody;
    private bool hasError = false;

    private Vector3[] originalPositions;
    private Quaternion[] originalRotations;
    private Rigidbody[] rigidbodies;

    void Start()
    {
        orbRigidbody = orbTransform.GetComponent<Rigidbody>();

        // Store the original positions and rotations of the objects
        originalPositions = new Vector3[] { pivotPoint.position, orbTransform.position, stringTransform.position };
        originalRotations = new Quaternion[] { pivotPoint.rotation, orbTransform.rotation, stringTransform.rotation };
        rigidbodies = new Rigidbody[] { pivotPoint.GetComponent<Rigidbody>(), orbRigidbody, stringTransform.GetComponent<Rigidbody>() };

        // Start checking the joint state periodically
        InvokeRepeating("CheckJointState", 0f, checkInterval);
    }

    void CheckJointState()
    {
        if (hasError)
        {
            // Reset the pendulum only if there's an error
            ResetPendulum();
            return;
        }

        // Check if any of the connected objects are null (indicating a physics error)
        if (pivotPoint == null || orbTransform == null || stringTransform == null)
        {
            Debug.Log("Physics error: One of the connected objects is null. Resetting...");
            hasError = true;
            return;
        }

        // Check if the orb's angular velocity exceeds the threshold
        if (orbRigidbody.angularVelocity.magnitude > maxAngularVelocity)
        {
            Debug.Log("Physics error: Orb's angular velocity exceeded threshold. Resetting...");
            hasError = true;
            return;
        }

        // No error detected
        hasError = false;
    }

    void ResetPendulum()
    {
        // Reset the positions and rotations of all connected objects
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            if (rigidbodies[i] != null)
            {
                // Reset position and rotation
                rigidbodies[i].position = originalPositions[i];
                rigidbodies[i].rotation = originalRotations[i];

                // Reset physics properties
                rigidbodies[i].velocity = Vector3.zero;
                rigidbodies[i].angularVelocity = Vector3.zero;
                rigidbodies[i].Sleep();
            }
        }

        Debug.Log("Pendulum reset.");

        // Reset the error flag
        hasError = false;
    }
}
