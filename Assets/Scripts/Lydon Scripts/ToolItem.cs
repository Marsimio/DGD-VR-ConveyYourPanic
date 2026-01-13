using System;
using UnityEngine;

public enum ToolType
{
    Hammer,
    Knife,
    Wrench,
    Screwdriver
}

public class ToolItem : MonoBehaviour
{
    public ToolType ToolType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<FixableItem>(out FixableItem fixableItem))
        {
            fixableItem.AttemptFix(ToolType);
        }
    }
}