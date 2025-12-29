using UnityEngine;

namespace Managers
{
    public class StatManager : MonoBehaviour
    {
        public static StatManager instance { get; private set; }

        private void Awake()
        {
            if (instance && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }
    }
}
