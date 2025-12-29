using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{
    public string difficulty;

    public int itemsToSpawn;
    public int conveyorSpeed;
    
}
