using System.Collections;
using System.Collections.Generic;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;

namespace PSCG.Labs
{
    [RequireComponent(typeof(RaySource))]
    public sealed class Lab_8_Part_1_Task_1 : TaskBase
    {
        public float RayAngle
        {
            get => _raySource.RayCastAngleInDegrees;
            set => _raySource.RayCastAngleInDegrees = value;
        }    
        public float RefractiveIndex
        {
            get => _raySource.EnvironmentRefractiveIndex;
            set => _raySource.EnvironmentRefractiveIndex = value;
        }
        public List<MaterialData> Materials { get; private set; } = new();

        private RaySource _raySource;

        private void Awake()
        {
            _raySource = GetComponent<RaySource>();
            _simulation = new CoroutineShell(this, Simulation);
        }

        protected override IEnumerator Simulation()
        {
            yield return null;
        }

        private void Update()
        {
            _raySource.CastRay();
        }
    }
}
