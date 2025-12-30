using UnityEngine;
using TMPro;

public class PaycheckManager : MonoBehaviour
{
    [Header("Input")]
    public TMP_InputField nameInputField;

    [Header("Paycheck")]
    public TextMeshProUGUI paycheckNameText; // Text element on paycheck that shows name
    public GameObject paycheckObject; // The actual paycheck GameObject (optional)

    public void OnSubmitName()
    {
        string playerName = nameInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            PrintPaycheck(playerName);
        }
        else
        {
            Debug.Log("Please enter a name!");
        }
    }

    private void PrintPaycheck(string name)
    {
        // Update paycheck with player's name
        if (paycheckNameText != null)
        {
            paycheckNameText.text = $"Employee: {name}";
        }

        // Optional: Spawn or activate paycheck object
        if (paycheckObject != null)
        {
            paycheckObject.SetActive(true);
        }

        Debug.Log($"Paycheck printed for: {name}");
    }
}
