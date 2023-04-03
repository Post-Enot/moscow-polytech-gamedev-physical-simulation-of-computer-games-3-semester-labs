using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public sealed class Lab_5_Task_2_UI : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;

        public Button StartButton { get; private set; }
        public Button StopButton { get; private set; }
        public Button BackButton { get; private set; }

        public FloatField HeightField { get; private set; }
        public FloatField TimeOffsetField { get; private set; }
        public FloatField AccelerationField { get; private set; }
        public FloatField AngleToTheHorizonField { get; private set; }

        public FloatField TimeField { get; private set; }
        public FloatField FlightTimeField { get; private set; }
        public FloatField AverageSpeedField { get; private set; }
        public FloatField LandingSpeedField { get; private set; }
        public FloatField DistanceField { get; private set; }

        private void Awake()
        {
            StartButton = _uiDocument.rootVisualElement.Q<Button>("start-button");
            StopButton = _uiDocument.rootVisualElement.Q<Button>("stop-button");
            BackButton = _uiDocument.rootVisualElement.Q<Button>("back-button");

            HeightField = _uiDocument.rootVisualElement.Q<FloatField>("height-field");
            TimeOffsetField = _uiDocument.rootVisualElement.Q<FloatField>("time-offset-field");
            AccelerationField = _uiDocument.rootVisualElement.Q<FloatField>("acceleration-field");
            AngleToTheHorizonField = _uiDocument.rootVisualElement.Q<FloatField>("angle-to-the-horizon-field");

            TimeField = _uiDocument.rootVisualElement.Q<FloatField>("time-field");
            FlightTimeField = _uiDocument.rootVisualElement.Q<FloatField>("flight-time-field");
            AverageSpeedField = _uiDocument.rootVisualElement.Q<FloatField>("average-speed-field");
            LandingSpeedField = _uiDocument.rootVisualElement.Q<FloatField>("landing-speed-field");
            DistanceField = _uiDocument.rootVisualElement.Q<FloatField>("distance-field");
        }
    }
}
