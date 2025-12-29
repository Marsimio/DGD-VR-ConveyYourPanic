using Managers.GameManager;
using UnityEngine;

public class FixableItem : MonoBehaviour
{
    void OnDestroy()
    {
        if (GameManager.instance != null)
            GameManager.instance.RemoveSpawnedItem(gameObject);
    }
}
