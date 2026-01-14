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
        // Start OFF (red) and set up listener
        if (switchInteractable != null)
            switchInteractable.selectEntered.AddListener(OnSwitchPressed);
        
        isOn = false;
        UpdateVisuals();  // Red + OFF
    }

    // Updates the light color and intensity based on switch state
    private void UpdateVisuals()
    {
        if (indicatorLight != null)
        {
            indicatorLight.color = isOn ? Color.green : Color.red;
            indicatorLight.enabled = true;  // ALWAYS on, just changes color
            indicatorLight.intensity = isOn ? 2f : 1f;  // Brighter when green
        }
    }

    // When switch is pressed
    private void OnSwitchPressed(SelectEnterEventArgs args)
    {
        ToggleSwitch();
    }

//Calls functions to toggle switch state, update visuals, and play sound
public void ToggleSwitch()
    {
        isOn = !isOn;
        UpdateVisuals();
        PlaySound();

        // Log for debugging
        Debug.Log($"Workbench switch toggled: {(isOn ? "ON" : "OFF")}");
    }

//Plays Sound on machine start
private void PlaySound()
    {
        if (audioSource != null && toggleSound != null)
        {
            audioSource.PlayOneShot(toggleSound);
        }
    }

    public bool GetState() => isOn;
}
