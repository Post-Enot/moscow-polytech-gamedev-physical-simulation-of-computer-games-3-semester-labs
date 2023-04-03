using System;
using UnityEngine;

namespace PSCG.Labs
{
    [Serializable]
    public sealed class MaterialData
    {
        [field: SerializeField] public float ThicknessInUnit { get; set; }
        [field: SerializeField] public float RefractiveIndex { get; set; }
    }
}
