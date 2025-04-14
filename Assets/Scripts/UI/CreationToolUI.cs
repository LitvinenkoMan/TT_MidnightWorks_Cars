using Systems.CarAssemble;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CreationToolUI : MonoBehaviour
    {
        [SerializeField] private FollowWorldTargetUI targeter;
        
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text levelUpgradeText;
        [SerializeField] private TMP_Text creationCostText;
        [SerializeField] private TMP_Text upgradeCostText;
        
        [SerializeField] private Button createPartButton;
        [SerializeField] private Button upgradeButton;

        private PartCreationTool _currentCreationTool;

        public void Setup(PartCreationTool creationTool, Sprite sprite)
        {
            _currentCreationTool = creationTool;
            createPartButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.RemoveAllListeners();
            
            icon.sprite = sprite;
            targeter.SetNewTarget(creationTool.gameObject.transform);
            createPartButton.onClick.AddListener(creationTool.CreatePart);
            upgradeButton.onClick.AddListener(creationTool.UpgradeTool);
            upgradeButton.onClick.AddListener(UpdateTextUI);
        }

        public void UpdateTextUI()
        {
            levelText.text = $"Lv. {_currentCreationTool.PartLevelCreation}";
            levelUpgradeText.text = $"Lv. {_currentCreationTool.PartLevelCreation +1}";

            creationCostText.text = $"{_currentCreationTool.CreationCost}";
            upgradeCostText.text = $"{_currentCreationTool.UpgradeCost}";
        }
    }
}
