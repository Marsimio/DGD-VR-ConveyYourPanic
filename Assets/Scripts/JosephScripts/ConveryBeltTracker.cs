using Managers.GameManager;
using UnityEngine;

public class ConveryBeltTracker : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && rb.CompareTag("Item"))
        {
            GameManager.instance.itemsOffConveyor++;
            Debug.Log("items off of conveyor");
            Debug.Log(GameManager.instance.itemsOffConveyor);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && rb.CompareTag("Item"))
        {
            GameManager.instance.itemsOffConveyor--;
            Debug.Log("items on conveyor");
        }
    }
}
