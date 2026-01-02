using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WorkbenchSwitch : MonoBehaviour
{
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable switchInteractable;
    [SerializeField] private Light indicatorLight;
    [SerializeField] private AudioClip toggleSound;
    [SerializeField] private AudioSource audioSource;

    private bool isOn = false;

    private void Start()
    {
        // Connect the switch interaction
        if (switchInteractable != null)
        {
            switchInteractable.selectEntered.AddListener(OnSwitchPressed);
        }

        // Set initial state
        UpdateVisuals();
    }

    private void OnSwitchPressed(SelectEnterEventArgs args)
    {
        ToggleSwitch();
    }

    public void ToggleSwitch()
    {
        isOn = !isOn;
        UpdateVisuals();
        PlaySound();

        // Log for debugging
        Debug.Log($"Workbench switch toggled: {(isOn ? "ON" : "OFF")}");
    }

    private void UpdateVisuals()
    {
        // Update light color
        if (indicatorLight != null)
        {
            indicatorLight.color = isOn ? Color.green : Color.red;
            indicatorLight.enabled = isOn;
        }

    }

    private void PlaySound()
    {
        if (audioSource != null && toggleSound != null)
        {
            audioSource.PlayOneShot(toggleSound);
        }
    }

    public bool GetState() => isOn;
}
