using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class ToolManager : MonoBehaviour
{
    [System.Serializable]
    public class ToolReference
    {
        public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable toolGrabInteractable;
        public Transform homePosition;
    }

    [SerializeField] private List<ToolReference> tools = new List<ToolReference>();
    [SerializeField] private List<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable> recallButtons = new List<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>(); // Buttons to trigger recall
    [SerializeField] private float recallSpeed = 5f; // Speed of recall movement

    private void Start()
    {
        // Register button listeners
        foreach (UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable button in recallButtons)
        {
            if (button != null)
            {
                button.selectEntered.AddListener(OnRecallPressed);
            }
        }
    }

    private void OnRecallPressed(SelectEnterEventArgs args)
    {
        RecallAllTools();
    }

    public void RecallAllTools()
    {
        foreach (ToolReference tool in tools)
        {
            if (tool.toolGrabInteractable != null && tool.homePosition != null)
            {
                StartCoroutine(MoveToolToHome(tool.toolGrabInteractable, tool.homePosition));
            }
        }
    }

    private System.Collections.IEnumerator MoveToolToHome(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable tool, Transform homePos)
    {
        bool wasEnabled = tool.enabled;
        tool.enabled = false;

        Transform toolTransform = tool.transform;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            toolTransform.position = Vector3.Lerp(toolTransform.position, homePos.position, t * recallSpeed * Time.deltaTime);
            yield return null;
        }

        toolTransform.position = homePos.position;
        toolTransform.rotation = homePos.rotation;
        tool.enabled = wasEnabled;
    }
}
