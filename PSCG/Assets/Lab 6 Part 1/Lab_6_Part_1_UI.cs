using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public class Lab_6_Part_1_UI : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;

        public Button StartButton { get; private set; }
        public Button StopButton { get; private set; }
        public Button BackButton { get; private set; }

        public FloatField Sphere1_VelocityField { get; private set; }
        public FloatField Sphere1_MassField { get; private set; }
        public FloatField Sphere2_VelocityField { get; private set; }
        public FloatField Sphere2_MassField { get; private set; }
        public FloatField DiflectionField { get; private set; }

        private void Awake()
        {
            StartButton = _uiDocument.rootVisualElement.Q<Button>("start-button");
            StopButton = _uiDocument.rootVisualElement.Q<Button>("stop-button");
            BackButton = _uiDocument.rootVisualElement.Q<Button>("back-button");

            Sphere1_VelocityField = _uiDocument.rootVisualElement.Q<FloatField>("sphere-1-velocity-field");
            Sphere1_MassField = _uiDocument.rootVisualElement.Q<FloatField>("sphere-1-mass-field");
            Sphere2_VelocityField = _uiDocument.rootVisualElement.Q<FloatField>("sphere-2-velocity-field");
            Sphere2_MassField = _uiDocument.rootVisualElement.Q<FloatField>("sphere-2-mass-field");
            DiflectionField = _uiDocument.rootVisualElement.Q<FloatField>("diflection-field");
        }
    }
}
