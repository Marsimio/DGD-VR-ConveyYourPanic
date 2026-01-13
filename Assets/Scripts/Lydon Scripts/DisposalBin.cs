using System;
using Managers.GameManager;
using UnityEngine;

public class DisposalBin : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<FixableItem>(out FixableItem fixableItem))
        {
            if (!fixableItem.isFixed)
            {
                GameManager.instance.strikes++;
            }
            Destroy(other.gameObject);
        }
    }
}
