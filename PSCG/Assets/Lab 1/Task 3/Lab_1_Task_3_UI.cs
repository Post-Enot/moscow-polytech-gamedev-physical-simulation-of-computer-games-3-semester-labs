using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public sealed class Lab_1_Task_3_UI : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset _visualTreeAsset;
        [SerializeField] private UIDocument _uiDocument;

        public Button StartButton { get; private set; }
        public Button StopButton { get; private set; }
        public Button BackButton { get; private set; }

        public Vector2Field StartPositionField { get; private set; }
        public Vector2Field StartSpeedField { get; private set; }
        public Vector2Field AccelerationField { get; private set; }

        public FloatField TimeField { get; private set; }
        public FloatField PathField { get; private set; }
        public Vector2Field CurrentSpeedField { get; private set; }
        public Vector2Field CurrentPositionField { get; private set; }

        private void Awake()
        {
            _uiDocument.visualTreeAsset = _visualTreeAsset;
            StartButton = _uiDocument.rootVisualElement.Q<Button>("start-button");
            StopButton = _uiDocument.rootVisualElement.Q<Button>("stop-button");
            BackButton = _uiDocument.rootVisualElement.Q<Button>("back-button");

            StartSpeedField = _uiDocument.rootVisualElement.Q<Vector2Field>("start-speed-field");
            StartPositionField = _uiDocument.rootVisualElement.Q<Vector2Field>("start-position-field");
            AccelerationField = _uiDocument.rootVisualElement.Q<Vector2Field>("acceleration-field");

            TimeField = _uiDocument.rootVisualElement.Q<FloatField>("time-field");
            CurrentSpeedField = _uiDocument.rootVisualElement.Q<Vector2Field>("current-speed-field");
            PathField = _uiDocument.rootVisualElement.Q<FloatField>("path-field");
            CurrentPositionField = _uiDocument.rootVisualElement.Q<Vector2Field>("current-position-field");
        }
    }
}
