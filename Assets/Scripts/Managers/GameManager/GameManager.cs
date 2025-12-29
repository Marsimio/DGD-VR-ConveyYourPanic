using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }
        
        [Header("Game Data")]
        public WaveData[] waves; // Wave count
        public ConveyorMove conveyor;
        public Transform itemSpawnPoint;
        
        [Header("Energy")]
        private int _maxEnergy; // Max Energy count
        public int energy; // Current Energy count. Affected by the crank in CrankRotate
        
        [Header("Play Data")]
        private List<GameObject> _spawnedItems = new List<GameObject>(); // Current item list
        public int waveCount; // Current wave count
        private WaveData currentWave; // Current Wave
        private Coroutine _waveCoroutine;
        private bool _allItemsSpawned; // Item Spawned flag
        private bool _waveComplete; // Wave Completed flag.
        private bool _gameResetting; // Wave resetting flag. needed to prevent items from calling a new wave by accident whilst being destroyed during reset
        private int _itemsOffConveyor;
        private int _strikes; // Strike Count. Three strikes and you're out.
        
        #region Getters/Setters
        public bool waveComplete
        {
            get => _waveComplete;
            set
            {
                _waveComplete = value;

                if (_waveComplete)
                {
                    StartNextWave();
                }
            }
        }

        public int itemsOffConveyor
        {
            get => _itemsOffConveyor;
            set
            {
                _itemsOffConveyor = value;
                
                // JOE TODO: IF ITEMS REACH 3 OR MORE ADD TO STRIKE COUNT AND START NEXT WAVE
            }
        }
        #endregion

        #region Game Start
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
            StartNextWave(); // Start running waves
        }
        #endregion
        
        #region Game Handling (Run Wave, Conveyor Speed, Item Spawn, Wave Reset)
        
        void StartNextWave()
        {
            ResetWave();
            waveCount++;
            currentWave = waves[Random.Range(0, waves.Length)];
            _waveCoroutine = StartCoroutine(RunWave(currentWave));
        }
        private IEnumerator RunWave(WaveData wave)
        {
            // 1. Wait out starting delay
            // 2. Set the Conveyor speed
            // 3. Spawn items as required and wait after each spawn
            // 4. Once all items have been spawned mark that all items have been spawned.
            
            yield return new WaitForSeconds(wave.waveStartDelay);
            SetConveyorSpeed(wave.conveyorSpeed);
            
            while (wave.itemsToSpawn > _spawnedItems.Count)
            {
                SpawnItems();
                yield return new WaitForSeconds(wave.itemsSpawnInterval);
            }
            
            _allItemsSpawned = true;
            
            yield break;
        }
        
        void SetConveyorSpeed(float waveConveyorSpeed)
        {
            conveyor.speed = waveConveyorSpeed;
        }
        
        void SpawnItems()
        {
            GameObject spawnedItem = Instantiate(currentWave.spawnableItems[Random.Range(0, currentWave.spawnableItems.Length)], itemSpawnPoint.position, itemSpawnPoint.rotation);
            _spawnedItems.Add(spawnedItem);
        }
        
        void ResetWave()
        {
            _gameResetting = true; // Prevents item destruction from starting a new wave prematurely
            if (_waveCoroutine != null)
            {
                StopCoroutine(_waveCoroutine);
                _waveCoroutine = null;
            }
            _spawnedItems.Clear();
            
            // JOE TODO: RESET ITEMS OFF CONVEYOR
            _allItemsSpawned = false;
            _waveComplete = false;
            _gameResetting = false;
        }
        #endregion

        #region Game Utility (Item Removal)
        
        public void RemoveSpawnedItem(GameObject item) // To be called BY THE OBJECT WHEN DESTROYED
        {
            if (item == null) return;

            if (_spawnedItems.Remove(item))
            {
                if (_spawnedItems.Count == 0 && _allItemsSpawned && !_gameResetting)
                {
                    waveComplete = true;
                }
            }
        }
        
        #endregion
    }
}
