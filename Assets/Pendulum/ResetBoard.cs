using UnityEngine;

public class ResetBoard : MonoBehaviour
{
    public GameObject whiteboardObject; // Reference to the whiteboard object

    private Renderer whiteboardRenderer;
    private Texture2D originalTexture;

    void Start()
    {
        if (whiteboardObject == null)
        {
            whiteboardObject = gameObject;
        }

        whiteboardRenderer = whiteboardObject.GetComponent<Renderer>();

        originalTexture = (Texture2D)whiteboardRenderer.material.mainTexture;
    }

    public void ResetWhiteboardTexture()
    {
        whiteboardRenderer.material.mainTexture = originalTexture;
    }
}
