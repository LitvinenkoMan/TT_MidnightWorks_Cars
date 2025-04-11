using Systems.Inventory;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private InventorySystem inventorySystem;
        private SaveManager _saveManager;
        private Wallet _wallet;

        public Wallet Wallet => _wallet;
        public SaveManager SaveManager => _saveManager;
        public InventorySystem InventorySystem => inventorySystem;

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            Initialize();
        }

        private void Start()
        {
            _saveManager.LoadGame();
        }

        private void OnApplicationQuit()
        {
            _saveManager.SaveGame();
        }

        private void Initialize()
        {
            _saveManager = new SaveManager();
            _wallet = new Wallet();
            
            _saveManager.AddSavableObject(_wallet);
            _saveManager.AddSavableObject(inventorySystem);
        }
    }
}
