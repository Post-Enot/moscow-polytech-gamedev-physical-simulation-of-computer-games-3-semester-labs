using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public sealed class Lab_2_UI : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset _visualTreeAsset;
        [SerializeField] private UIDocument _uiDocument;

        public Button StartButton { get; private set; }
        public Button StopButton { get; private set; }
        public Button BackButton { get; private set; }

        public FloatField RadiusField { get; private set; }
        public FloatField RotationFrequencyField { get; private set; }

        public FloatField TimeField { get; private set; }
        public FloatField PathField { get; private set; }
        public FloatField AngularVelocityField { get; private set; }
        public FloatField LinearVelocityField { get; private set; }
        public FloatField RotationAngleField { get; private set; }
        public Vector2Field CurrentPositionField { get; private set; }
        public IntegerField RevolutionCountField { get; private set; }

        private void Awake()
        {
            _uiDocument.visualTreeAsset = _visualTreeAsset;
            StartButton = _uiDocument.rootVisualElement.Q<Button>("start-button");
            StopButton = _uiDocument.rootVisualElement.Q<Button>("stop-button");
            BackButton = _uiDocument.rootVisualElement.Q<Button>("back-button");

            RadiusField = _uiDocument.rootVisualElement.Q<FloatField>("radius-field");
            RotationFrequencyField = _uiDocument.rootVisualElement.Q<FloatField>("rotation-frequency-field");

            TimeField = _uiDocument.rootVisualElement.Q<FloatField>("time-field");
            AngularVelocityField = _uiDocument.rootVisualElement.Q<FloatField>("angular-velocity-field");
            LinearVelocityField = _uiDocument.rootVisualElement.Q<FloatField>("linear-velocity-field");
            PathField = _uiDocument.rootVisualElement.Q<FloatField>("path-field");
            RotationAngleField = _uiDocument.rootVisualElement.Q<FloatField>("rotation-angle-field");
            CurrentPositionField = _uiDocument.rootVisualElement.Q<Vector2Field>("current-position-field");
            RevolutionCountField = _uiDocument.rootVisualElement.Q<IntegerField>("revolution-count-field");
        }
    }
}
