using System;
using Managers.GameManager;
using UnityEngine;

public class DisposalBin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<FixableItem>(out FixableItem fixableItem))
        {
            fixableItem.isDeductable = false;
            if (!fixableItem.isFixed)
            {
                GameManager.instance.strikes++; // on not fixed item increase strike count by one
            }
            else
            {
                GameManager.instance.totalFixedItems++; // on a fixed item increase the total by one
            }
            Destroy(other.gameObject);
        }
    }
}
