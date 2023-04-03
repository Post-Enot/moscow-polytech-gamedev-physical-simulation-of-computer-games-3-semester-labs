using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    [RequireComponent(typeof(Lab_8_Part_1_Task_2), typeof(Lab_8_Part_1_Task_2_UI), typeof(LensSpawner))]
    public class Lab_8_Part_1_Task_2_View : TaskViewBase
    {
        private Lab_8_Part_1_Task_2 _task;
        private Lab_8_Part_1_Task_2_UI _ui;
        private LensSpawner _lensSpawner;

        private void Awake()
        {
            _task = GetComponent<Lab_8_Part_1_Task_2>();
            _ui = GetComponent<Lab_8_Part_1_Task_2_UI>();
            _lensSpawner = GetComponent<LensSpawner>();
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
                _lensSpawner.DeleteLens();
                _lensSpawner.SpawnLens(
                    (LensType)_ui.InputLensTypeEnumField.value,
                    (LensType)_ui.OutputLensTypeEnumField.value,
                    _ui.LensRefractiveIndexSlider.value);
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

