using System;
using Managers.GameManager;
using Unity.VisualScripting;
using UnityEngine;

public class FixableItem : MonoBehaviour
{
    public bool isDeductable = false;
    public bool isInside = false;
    public bool wasInside = false;
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
        if (isFixed) GameManager.instance._totalFixedItems++; return;

        if (IsToolAllowed(toolType))
            Fix();
    }
    
    private bool IsToolAllowed(ToolType toolType)
    {
        if (allowedTool == toolType) return true;

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
