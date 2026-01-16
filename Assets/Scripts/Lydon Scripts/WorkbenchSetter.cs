using System;
using UnityEngine;

public class WorkbenchSetter : MonoBehaviour
{
    private void OnTriggerExit(Collider other) // sets the item to not be in a fixable state
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && rb.CompareTag("Item"))
        {
            FixableItem item = rb.gameObject.GetComponent<FixableItem>();
            item.isFixable = false;
        }
    }
    private void OnTriggerEnter(Collider other) // sets the item to be in a fixable state
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && rb.CompareTag("Item"))
        {
            FixableItem item = rb.gameObject.GetComponent<FixableItem>();
            item.isFixable = true;
        }
    }
}
