using UnityEngine;

namespace UI
{
    public class FollowWorldTargetUI : MonoBehaviour
    {
        [SerializeField] private Transform targetWorldObject;  // Объект в сцене, к которому привязаться
        [SerializeField] private Vector3 screenOffset = new Vector3(0, 100f, 0);  // Смещение UI над объектом

        [SerializeField] private RectTransform myRectTransform;
        [SerializeField] private Canvas canvas;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        void LateUpdate()
        {
            if (targetWorldObject == null || canvas == null)
                return;

            // Конвертация позиции объекта в экранные координаты
            Vector3 screenPos = _camera.WorldToScreenPoint(targetWorldObject.position);

            // Если объект за камерой — скрываем UI
            if (screenPos.z < 0)
            {
                myRectTransform.gameObject.SetActive(false);
                return;
            }

            myRectTransform.gameObject.SetActive(true);
            myRectTransform.position = screenPos + screenOffset;
        }

        public void SetNewTarget(Transform newTransform)
        {
            targetWorldObject = newTransform;
        }
    }
}
