using System.Collections;
using UnityEngine;

namespace Managers.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }
    
        public int itemCount; // L Current item count TODO: SET GETTER AND SETTER. WHEN 0 LEFT
        public int waveCount; // L Current wave count
        private int _maxEnergy; // L Max Energy count
        public int energy; // L Current Energy count. Affected by the crank in CrankRotate
        private WaveData[] _waves; // L Wave count
        private bool _allItemsSpawned; // L Item Spawned flag
        private bool _waveComplete; // L Wave Completed flag.
        private int _strikes; // L Strike Count. Three strikes and you're out.

        private void Awake()
        {
            if (instance && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        void Start()
        {
            // TODO: START WAVE LOGIC
        }

        void SetConveyorSpeed()
        {
            // TODO: SET CONVEYOR SPEED
        }

        void SpawnItems()
        {
            // TODO: SPAWN ITEM LOGIC
        }

        IEnumerator StartWave()
        {
            // TODO: UPDATE UI FX
            
            // TODO: SetConveyorSpeed()
            // FOR LOOP SPAWN WAVEDATA ITEMS
            // SET ITEMS SPAWNED TRUE
            
            yield return null;
        }
    }
}
