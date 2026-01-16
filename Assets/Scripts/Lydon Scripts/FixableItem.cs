using System;
using Managers.GameManager;
using Unity.VisualScripting;
using UnityEngine;

public class FixableItem : MonoBehaviour
{
    public bool isDeductable = false;
    public bool isInside = false;
    public bool wasInside = false;
    public bool isFixable = false;
    public Coroutine trackerCoroutine;
    public bool isFixed = false;
    public MeshFilter targetMeshFilter;
    public Mesh meshToChange;
    public ToolType allowedTool;

    private void Awake()
    {
        if (targetMeshFilter == null)
            targetMeshFilter = GetComponentInParent<MeshFilter>();
    }

    public void AttemptFix(ToolType toolType)
    {
        Debug.Log("Fixing item");
        if (IsToolAllowed(toolType))
            Fix();
    }
    
    private bool IsToolAllowed(ToolType toolType)
    {
        if (allowedTool == toolType && isFixable && GameManager.instance.energy > 0 && !isFixed) return true; // Verifies that the item is being hit with the correct tool, is in a fixing zone, the player has enough energy and isn't fixed yet

        return false;
    }

    public void Fix()
    {
        if (meshToChange == null || targetMeshFilter == null) return;
        
        isFixed = true;
        targetMeshFilter.sharedMesh = meshToChange;
    }

    
    void OnDestroy()
    {
        if (GameManager.instance != null)
            GameManager.instance.RemoveSpawnedItem(gameObject);
    }
}
