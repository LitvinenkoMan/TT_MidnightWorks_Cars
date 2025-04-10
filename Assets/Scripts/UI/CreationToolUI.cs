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
        
        [SerializeField] private Button CreatePartButton;
        [SerializeField] private Button UpgradeButton;

        public void Setup(PartCreationTool creationTool, Sprite sprite)
        {
            CreatePartButton.onClick.RemoveAllListeners();
            UpgradeButton.onClick.RemoveAllListeners();
            
            icon.sprite = sprite;
            levelText.text = $"Lv. {creationTool.PartLevelCreation}";
            levelUpgradeText.text = $"Lv. {creationTool.PartLevelCreation +1}";
            targeter.SetNewTarget(creationTool.gameObject.transform);
            CreatePartButton.onClick.AddListener(creationTool.CreatePart);
            UpgradeButton.onClick.AddListener(creationTool.UpgradeTool);
        }
    }
}
