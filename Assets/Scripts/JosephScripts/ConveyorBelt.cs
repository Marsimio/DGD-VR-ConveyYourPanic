using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConveyorBelt : MonoBehaviour
{
    [Header("Conveyor Settings")]
    [SerializeField] private float conveyorSpeed = 2f;
    [SerializeField] private Vector3 moveDirection = Vector3.forward;

    [Header("Item Tracking")]
    [SerializeField] private int itemsRemovedCount = 0;
    [SerializeField] private int itemsPlacedCount = 0;

    [Header("Events")]
    public UnityEvent<int> OnItemRemoved;
    public UnityEvent<int> OnItemPlaced;

    private List<Rigidbody> itemsOnBelt = new List<Rigidbody>();
    private HashSet<GameObject> trackedItems = new HashSet<GameObject>();

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object has a Rigidbody and can be moved
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null && !itemsOnBelt.Contains(rb))
        {
            itemsOnBelt.Add(rb);

            // Track new items being placed
            if (!trackedItems.Contains(collision.gameObject))
            {
                trackedItems.Add(collision.gameObject);
                itemsPlacedCount++;
                OnItemPlaced?.Invoke(itemsPlacedCount);
                Debug.Log($"Item placed on conveyor. Total placed: {itemsPlacedCount}");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null && itemsOnBelt.Contains(rb))
        {
            itemsOnBelt.Remove(rb);

            // Track items being removed
            if (trackedItems.Contains(collision.gameObject))
            {
                itemsRemovedCount++;
                OnItemRemoved?.Invoke(itemsRemovedCount);
                Debug.Log($"Item removed from conveyor. Total removed: {itemsRemovedCount}");
            }
        }
    }

    // Public methods for external access
    public int GetItemsRemovedCount() => itemsRemovedCount;
    public int GetItemsPlacedCount() => itemsPlacedCount;
    public int GetCurrentItemsOnBelt() => itemsOnBelt.Count;

    public void ResetCounts()
    {
        itemsRemovedCount = 0;
        itemsPlacedCount = 0;
        Debug.Log("Conveyor counts reset");
    }
}
