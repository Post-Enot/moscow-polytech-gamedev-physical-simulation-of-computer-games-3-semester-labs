using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public sealed class MaterialDataView : VisualElement
    {
        public MaterialDataView()
        {
            ThicknessInUnitField = new FloatField("Толщина материала");
            RefractiveIndexField = new FloatField("Показатель переломления материала");
            Add(ThicknessInUnitField);
            Add(RefractiveIndexField);
        }

        public FloatField ThicknessInUnitField { get; }
        public FloatField RefractiveIndexField { get; }
        public MaterialData MaterialData { get; private set; }

        public void BindWith(MaterialData materialData)
        {
            MaterialData = materialData;
            ThicknessInUnitField.RegisterValueChangedCallback(ThicknessInUnitFieldVCC);
            RefractiveIndexField.RegisterValueChangedCallback(RefractiveIndexFieldVCC);
        }

        public void Unbind()
        {
            MaterialData = null;
            ThicknessInUnitField.UnregisterValueChangedCallback(ThicknessInUnitFieldVCC);
            RefractiveIndexField.UnregisterValueChangedCallback(RefractiveIndexFieldVCC);
        }

        private void ThicknessInUnitFieldVCC(ChangeEvent<float> context)
        {
            if (MaterialData != null)
            {
                MaterialData.ThicknessInUnit = context.newValue;
            }
        }

        private void RefractiveIndexFieldVCC(ChangeEvent<float> context)
        {
            if (MaterialData != null)
            {
                MaterialData.RefractiveIndex = context.newValue;
            }
        }
    }
}
