using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pendulumtest : MonoBehaviour
{
    public Transform gripPoint;
    public float forwardForceMagnitude = 1f; 
    public float centripetalForceMagnitude = 1f; 

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Calculate the forces and update velocity and position
        CalculateForcesAndUpdateVelocity();
    }

    void CalculateForcesAndUpdateVelocity()
    {
        // Vector from grip point to current position
        Vector3 offset = transform.position - gripPoint.position;
        // Get a normalized vector perpendicular to the offset (in the transform's forward direction)
        Vector3 moveDirection = Vector3.Cross(offset, transform.right).normalized;

        // Calculate the forward force
        Vector3 forwardForce = forwardForceMagnitude * -transform.forward; // Negate the forward vector

        // Determine the direction of swing
        float swingDirection = Mathf.Sign(Vector3.Dot(rb.angularVelocity, transform.right));

        // Apply the forward force in the opposite direction if swinging right, and vice versa
        rb.AddForce(-swingDirection * forwardForce, ForceMode.Acceleration);

        // Calculate the centripetal force
        Vector3 centripetalForce = centripetalForceMagnitude * -Vector3.Cross(rb.angularVelocity, offset.normalized); // Negate the centripetal vector

        // Apply the centripetal force
        rb.AddForce(centripetalForce, ForceMode.Acceleration);
    }
}