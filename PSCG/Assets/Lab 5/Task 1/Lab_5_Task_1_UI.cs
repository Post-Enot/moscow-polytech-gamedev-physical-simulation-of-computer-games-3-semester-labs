using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public sealed class Lab_5_Task_1_UI : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset _visualTreeAsset;
        [SerializeField] private UIDocument _uiDocument;

        public Button StartButton { get; private set; }
        public Button StopButton { get; private set; }
        public Button BackButton { get; private set; }

        public FloatField HeightField { get; private set; }
        public FloatField SpeedField { get; private set; }
        public FloatField DistanceField { get; private set; }
        public FloatField TimeField { get; private set; }

        private void Awake()
        {
            _uiDocument.visualTreeAsset = _visualTreeAsset;
            StartButton = _uiDocument.rootVisualElement.Q<Button>("start-button");
            StopButton = _uiDocument.rootVisualElement.Q<Button>("stop-button");
            BackButton = _uiDocument.rootVisualElement.Q<Button>("back-button");

            HeightField = _uiDocument.rootVisualElement.Q<FloatField>("height-field");
            SpeedField = _uiDocument.rootVisualElement.Q<FloatField>("speed-field");
            DistanceField = _uiDocument.rootVisualElement.Q<FloatField>("distance-field");
            TimeField = _uiDocument.rootVisualElement.Q<FloatField>("time-field");
        }
    }
}
