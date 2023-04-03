using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public sealed class Lab_7_UI : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset _visualTreeAsset;
        [SerializeField] private UIDocument _uiDocument;

        public const string Floor1_DropdownChoice = "Поверхность 1 (зелёная)";
        public const string Floor2_DropdownChoice = "Поверхность 2 (красная)";
        public const string Floor3_DropdownChoice = "Поверхность 3 (жёлтая)";

        public Button StartButton { get; private set; }
        public Button StopButton { get; private set; }
        public Button BackButton { get; private set; }

        public DropdownField StartPositionField { get; private set; }
        public FloatField Floor1_FrictionField { get; private set; }
        public FloatField Floor2_FrictionField { get; private set; }
        public FloatField Floor3_FrictionField { get; private set; }
        public FloatField StartVelocityField { get; private set; }
        public FloatField A_Field { get; private set; }


        private void Awake()
        {
            _uiDocument.visualTreeAsset = _visualTreeAsset;

            StartButton = _uiDocument.rootVisualElement.Q<Button>("start-button");
            StopButton = _uiDocument.rootVisualElement.Q<Button>("stop-button");
            BackButton = _uiDocument.rootVisualElement.Q<Button>("back-button");

            StartPositionField = _uiDocument.rootVisualElement.Q<DropdownField>("start-position-dropdown");
            Floor1_FrictionField = _uiDocument.rootVisualElement.Q<FloatField>("floor-1-friction-field");
            Floor2_FrictionField = _uiDocument.rootVisualElement.Q<FloatField>("floor-2-friction-field");
            Floor3_FrictionField = _uiDocument.rootVisualElement.Q<FloatField>("floor-3-friction-field");
            StartVelocityField = _uiDocument.rootVisualElement.Q<FloatField>("start-velocity-field");
            A_Field = _uiDocument.rootVisualElement.Q<FloatField>("A-field");

            StartPositionField.choices = new List<string>
            {
                Floor1_DropdownChoice,
                Floor2_DropdownChoice,
                Floor3_DropdownChoice
            };
        }
    }
}
