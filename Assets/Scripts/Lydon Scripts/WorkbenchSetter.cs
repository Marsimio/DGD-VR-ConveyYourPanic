using System;
using UnityEngine;

public class WorkbenchSetter : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && rb.CompareTag("Item"))
        {
            FixableItem item = rb.gameObject.GetComponent<FixableItem>();
            item.isFixable = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && rb.CompareTag("Item"))
        {
            FixableItem item = rb.gameObject.GetComponent<FixableItem>();
            item.isFixable = true;
        }
    }
}
