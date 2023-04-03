using IUP.Toolkits.CoroutineShells;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

namespace PSCG.Labs
{
    [RequireComponent(typeof(LensLevelGenerator), typeof(Lab_8_Part_2), typeof(Lab_8_Part_2_UI))]
    public sealed class Lab_8_Part_2_View : TaskViewBase
    {
        private Lab_8_Part_2 _task;
        private Lab_8_Part_2_UI _ui;
        private LensLevelGenerator _levelGenerator;

        private void Awake()
        {
            _task = GetComponent<Lab_8_Part_2>();
            _ui = GetComponent<Lab_8_Part_2_UI>();
            _levelGenerator = GetComponent<LensLevelGenerator>();
        }

        private void Start()
        {
            _simulation = new CoroutineShell(this, DisplaySimulation);
            _task.SimulationStarted += StartSimulationDisplay;
            _task.SimulationEnded += StopSimulationDisplay;

            _ui.StartButton.clicked += () =>
            {
                if (_task.IsSimulationStarted)
                {
                    _task.StopSimulation();
                }
                _task.StartSimulation();
                _levelGenerator.GenerateLevel();
            };
            _ui.StopButton.clicked += () =>
            {
                if (_task.IsSimulationStarted)
                {
                    _task.StopSimulation();
                }
            };

            _ui.RayIncidenceAngleField.RegisterValueChangedCallback(
                context => _task.RayAngle = context.newValue);
            _ui.RefractiveIndexField.RegisterValueChangedCallback(
                context => _task.RefractiveIndex = context.newValue);
            _task.RefractiveIndex = _ui.RefractiveIndexField.value;
        }

        protected override IEnumerator DisplaySimulation()
        {
            yield return null;
        }
    }
}
