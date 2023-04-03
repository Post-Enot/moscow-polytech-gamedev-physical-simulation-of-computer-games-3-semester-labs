using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public sealed class Lab_6_Part_2_UI : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;

        public Button StartButton { get; private set; }
        public Button StopButton { get; private set; }
        public Button BackButton { get; private set; }

        public FloatField VelocityField { get; private set; }
        public FloatField AngleField { get; private set; }

        private void Awake()
        {
            StartButton = _uiDocument.rootVisualElement.Q<Button>("start-button");
            StopButton = _uiDocument.rootVisualElement.Q<Button>("stop-button");
            BackButton = _uiDocument.rootVisualElement.Q<Button>("back-button");

            VelocityField = _uiDocument.rootVisualElement.Q<FloatField>("velocity-field");
            AngleField = _uiDocument.rootVisualElement.Q<FloatField>("angle-field");
        }
    }
}
