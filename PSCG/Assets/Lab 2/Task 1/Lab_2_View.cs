using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public sealed class Lab_2_View : TaskViewBase
    {
        [SerializeField] private GameObject _sphere;
        [SerializeField] private Lab_2 _task;
        [SerializeField] private Lab_2_UI _ui;

        private Vector3 _positionOffset;

        private void Start()
        {
            _positionOffset = _sphere.transform.position;
            _simulation = new CoroutineShell(this, DisplaySimulation);
            _task.SimulationStarted += StartSimulationDisplay;
            _task.SimulationEnded += StopSimulationDisplay;
            _ui.RadiusField.RegisterValueChangedCallback(RadiusFieldVCC);
            _ui.RotationFrequencyField.RegisterValueChangedCallback(RotationFrequencyFieldVCC);
            _ui.StartButton.clicked += () =>
            {
                if (!_task.IsSimulationStarted)
                {
                    _task.StartSimulation();
                }
            };
            _ui.StopButton.clicked += () =>
            {
                if (_task.IsSimulationStarted)
                {
                    _task.StopSimulation();
                }
            };
        }

        private void RadiusFieldVCC(ChangeEvent<float> context)
        {
            _task.Radius = context.newValue;
        }

        private void RotationFrequencyFieldVCC(ChangeEvent<float> context)
        {
            _task.RotationFrequencyInSecond = context.newValue;
        }

        protected override IEnumerator DisplaySimulation()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                _sphere.transform.position = _positionOffset +
                    new Vector3(_task.CurrentPosition.x, _task.CurrentPosition.y, 0);
                _ui.TimeField.value = _task.TimeInSecond;
                _ui.PathField.value = _task.PathInUnit;
                _ui.AngularVelocityField.value = _task.AngularVelocity;
                _ui.LinearVelocityField.value = _task.LinearVelocity;
                _ui.RotationAngleField.value = _task.RotationAngle;
                _ui.CurrentPositionField.value = _task.CurrentPosition;
                _ui.RevolutionCountField.value = _task.RevolutionCount;
            }
        }
    }
}
