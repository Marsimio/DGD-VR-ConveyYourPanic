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
                GameManager.instance.strikes++;
            }
            else
            {
                GameManager.instance.totalFixedItems++;
            }
            Destroy(other.gameObject);
        }
    }
}
