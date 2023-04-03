using System;
using UnityEngine;

namespace PSCG.Labs
{
    [Serializable]
    public sealed class LensData
    {
        [field: SerializeField] public LensType InputLensType { get; set; }
        [field: SerializeField] public LensType OutputLensType { get; set; }
        [field: SerializeField] public float ThicknessInUnit { get; set; }
        [field: SerializeField] public float HeightInUnit { get; set; }
        [field: SerializeField] public float RefractiveIndex { get; set; }
        [field: SerializeField] public float SagittalSize { get; set; }
    }
}
