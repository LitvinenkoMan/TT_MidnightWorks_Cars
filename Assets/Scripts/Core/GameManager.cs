using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public SaveManager saveManager;
        public ResourceHolder resourceHolder;

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            saveManager.LoadGame();
        }

        private void OnApplicationQuit()
        {
            saveManager.SaveGame();
        }
    }
}
