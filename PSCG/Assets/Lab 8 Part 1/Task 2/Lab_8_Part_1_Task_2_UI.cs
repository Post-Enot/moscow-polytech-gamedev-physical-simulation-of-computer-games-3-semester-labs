using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public sealed class Lab_8_Part_1_Task_2_UI : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset _visualTreeAsset;
        [SerializeField] private UIDocument _uiDocument;

        public Button StartButton { get; private set; }
        public Button StopButton { get; private set; }
        public Button BackButton { get; private set; }

        public FloatField RayIncidenceAngleField { get; private set; }
        public FloatField RefractiveIndexField { get; private set; }
        public EnumField InputLensTypeEnumField { get; private set; }
        public EnumField OutputLensTypeEnumField { get; private set; }
        public Slider LensRefractiveIndexSlider { get; private set; }


        private void Awake()
        {
            _uiDocument.visualTreeAsset = _visualTreeAsset;

            StartButton = _uiDocument.rootVisualElement.Q<Button>("start-button");
            StopButton = _uiDocument.rootVisualElement.Q<Button>("stop-button");
            BackButton = _uiDocument.rootVisualElement.Q<Button>("back-button");

            RayIncidenceAngleField = _uiDocument.rootVisualElement.Q<FloatField>("ray-incidence-angle-field");
            RefractiveIndexField = _uiDocument.rootVisualElement.Q<FloatField>("refractive-index-field");
            InputLensTypeEnumField = _uiDocument.rootVisualElement.Q<EnumField>("input-lens-type-enum-field");
            InputLensTypeEnumField.Init(LensType.Flat);
            OutputLensTypeEnumField = _uiDocument.rootVisualElement.Q<EnumField>("output-lens-type-enum-field");
            OutputLensTypeEnumField.Init(LensType.Flat);
            LensRefractiveIndexSlider = _uiDocument.rootVisualElement.Q<Slider>("lens-refractive-index-slider");
        }
    }
}
