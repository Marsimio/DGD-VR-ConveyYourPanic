using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{
    public string difficulty;
    public float waveStartDelay;
    public float itemsSpawnInterval;

    public GameObject[] spawnableItems;
    public int itemsToSpawn;
    public float conveyorSpeed;

}
