using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public sealed class Lab_1_Task_1_UI : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset _visualTreeAsset;
        [SerializeField] private UIDocument _uiDocument;

        public Button StartButton { get; private set; }
        public Button StopButton { get; private set; }
        public Button BackButton { get; private set; }
        public Vector2Field SpeedField { get; private set; }
        public FloatField PathField { get; private set; }
        public FloatField TimeField { get; private set; }

        private void Awake()
        {
            _uiDocument.visualTreeAsset = _visualTreeAsset;
            StartButton = _uiDocument.rootVisualElement.Q<Button>("start-button");
            StopButton = _uiDocument.rootVisualElement.Q<Button>("stop-button");
            BackButton = _uiDocument.rootVisualElement.Q<Button>("back-button");

            SpeedField = _uiDocument.rootVisualElement.Q<Vector2Field>("speed-field");
            PathField = _uiDocument.rootVisualElement.Q<FloatField>("path-field");
            TimeField = _uiDocument.rootVisualElement.Q<FloatField>("time-field");
        }
    }
}
