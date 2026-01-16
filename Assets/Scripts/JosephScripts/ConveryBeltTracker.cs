using System.Collections;
using Managers.GameManager;
using UnityEngine;

public class ConveryBeltTracker : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && rb.CompareTag("Item")) // Makes sure its an item
        {
            FixableItem item = rb.gameObject.GetComponent<FixableItem>();
            item.isInside = false;
            if (item.trackerCoroutine != null)
                item.StopCoroutine(item.trackerCoroutine); // stops ongoing coroutines

            item.trackerCoroutine = item.StartCoroutine(CheckForExit(rb, item));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && rb.CompareTag("Item"))
        {
            FixableItem item = rb.gameObject.GetComponent<FixableItem>();
            item.isInside = true;
            if (item.trackerCoroutine != null)
                item.StopCoroutine(item.trackerCoroutine);

            item.trackerCoroutine = item.StartCoroutine(CheckForEntry(rb, item));
        }
    }

    IEnumerator CheckForExit(Rigidbody rb, FixableItem item)
    {
        yield return new WaitForSeconds(1f); // only does the check after a second to prevent grab bounce
        if (item.isDeductable && !item.isInside && item.isInside != item.wasInside)
        {
            item.wasInside = item.isInside;
            GameManager.instance.itemsOffConveyor++;
            Debug.Log("Item removed from conveyor");
        }
    }
    IEnumerator CheckForEntry(Rigidbody rb, FixableItem item)
    {
        yield return new WaitForSeconds(1f); // only does the check after a second to prevent grab bounce
        if (item.isDeductable)
        {
            if (item.isInside && item.isInside != item.wasInside)
            {
                item.wasInside = item.isInside;
                GameManager.instance.itemsOffConveyor--;
                Debug.Log("Item added on conveyor");
            }
        }
        else
        {
            item.isDeductable = true;
            item.isInside = true;
            item.wasInside = true;
        }
    }
}
