using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public sealed class Lab_4_Task_2_UI : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset _visualTreeAsset;
        [SerializeField] private UIDocument _uiDocument;

        public Button StartButton { get; private set; }
        public Button StopButton { get; private set; }
        public Button BackButton { get; private set; }

        public Vector3Field StartPositionField { get; private set; }
        public Vector2Field StartSpeedField { get; private set; }
        public Vector2Field StartAccelerationField { get; private set; }
        public FloatField TimeOffsetField { get; private set; }
        public FloatField RadiusField { get; private set; }

        public Vector3Field CurrentPositionField { get; private set; }
        public FloatField PathField { get; private set; }
        public FloatField TimeField { get; private set; }

        private void Awake()
        {
            _uiDocument.visualTreeAsset = _visualTreeAsset;
            StartButton = _uiDocument.rootVisualElement.Q<Button>("start-button");
            StopButton = _uiDocument.rootVisualElement.Q<Button>("stop-button");
            BackButton = _uiDocument.rootVisualElement.Q<Button>("back-button");

            StartPositionField = _uiDocument.rootVisualElement.Q<Vector3Field>("start-position-field");
            StartSpeedField = _uiDocument.rootVisualElement.Q<Vector2Field>("start-speed-field");
            StartAccelerationField = _uiDocument.rootVisualElement.Q<Vector2Field>("start-acceleration-field");
            TimeOffsetField = _uiDocument.rootVisualElement.Q<FloatField>("time-offset-field");
            RadiusField = _uiDocument.rootVisualElement.Q<FloatField>("radius-field");

            CurrentPositionField = _uiDocument.rootVisualElement.Q<Vector3Field>("current-position-field");
            PathField = _uiDocument.rootVisualElement.Q<FloatField>("path-field");
            TimeField = _uiDocument.rootVisualElement.Q<FloatField>("time-field");
        }
    }
}
