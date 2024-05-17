using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetPosition : MonoBehaviour
{
    [SerializeField] private ActionBasedController leftHandController;
    [SerializeField] private GameObject[] objectsToReset;

    private Vector3[] originalPositions;
    private Quaternion[] originalRotations;
    private Rigidbody[] rigidbodies;

    private void Start()
    {
        // Store the original positions and rotations of the objects
        originalPositions = new Vector3[objectsToReset.Length];
        originalRotations = new Quaternion[objectsToReset.Length];
        rigidbodies = new Rigidbody[objectsToReset.Length];

        for (int i = 0; i < objectsToReset.Length; i++)
        {
            originalPositions[i] = objectsToReset[i].transform.position;
            originalRotations[i] = objectsToReset[i].transform.rotation;
            rigidbodies[i] = objectsToReset[i].GetComponent<Rigidbody>();
        }
    }

    private void OnEnable()
    {
        if (leftHandController != null)
        {
            leftHandController.selectAction.action.performed += OnResetButtonPressed;
        }
    }

    private void OnDisable()
    {
        if (leftHandController != null)
        {
            leftHandController.selectAction.action.performed -= OnResetButtonPressed;
        }
    }

    private void OnResetButtonPressed(InputAction.CallbackContext context)
    {
        ResetObjectsToOriginalPositions();
    }

    public void ResetObjectsToOriginalPositions()
    {
        for (int i = 0; i < objectsToReset.Length; i++)
        {
            // Reset position and rotation
            objectsToReset[i].transform.position = originalPositions[i];
            objectsToReset[i].transform.rotation = originalRotations[i];

            // Reset physics properties
            if (rigidbodies[i] != null)
            {
                rigidbodies[i].velocity = Vector3.zero;
                rigidbodies[i].angularVelocity = Vector3.zero;
                rigidbodies[i].Sleep();  
            }
        }
        Debug.Log("Objects have been reset to their original positions and states.");
    }
}
