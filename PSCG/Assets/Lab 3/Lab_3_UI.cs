using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public sealed class Lab_3_UI : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;

        public Button StartButton { get; private set; }
        public Button StopButton { get; private set; }
        public Button BackButton { get; private set; }

        public FloatField T1Field { get; private set; }
        public FloatField T2Field { get; private set; }
        public Vector2Field StartPositionField { get; private set; }
        public Vector2Field A_Field { get; private set; }
        public Vector2Field B_Field { get; private set; }

        public FloatField TimeField { get; private set; }
        public FloatField PathField { get; private set; }
        public Vector2Field SpeedField { get; private set; }
        public Vector2Field AccelerationField { get; private set; }

        private void Awake()
        {
            StartButton = _uiDocument.rootVisualElement.Q<Button>("start-button");
            StopButton = _uiDocument.rootVisualElement.Q<Button>("stop-button");
            BackButton = _uiDocument.rootVisualElement.Q<Button>("back-button");

            T1Field = _uiDocument.rootVisualElement.Q<FloatField>("timestamp1-field");
            T2Field = _uiDocument.rootVisualElement.Q<FloatField>("timestamp2-field");
            StartPositionField = _uiDocument.rootVisualElement.Q<Vector2Field>("start-position-field");
            A_Field = _uiDocument.rootVisualElement.Q<Vector2Field>("A-field");
            B_Field = _uiDocument.rootVisualElement.Q<Vector2Field>("B-field");

            TimeField = _uiDocument.rootVisualElement.Q<FloatField>("time-field");
            PathField = _uiDocument.rootVisualElement.Q<FloatField>("path-field");
            SpeedField = _uiDocument.rootVisualElement.Q<Vector2Field>("speed-field");
            AccelerationField = _uiDocument.rootVisualElement.Q<Vector2Field>("acceleration-field");
        }
    }
}
