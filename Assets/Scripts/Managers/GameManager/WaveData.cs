using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{
    public float waveStartDelay;
    public float itemsSpawnInterval;
    public float waveEndDelay;

    public GameObject[] spawnableItems;
    public int itemsToSpawn;
    public float conveyorSpeed;

}
