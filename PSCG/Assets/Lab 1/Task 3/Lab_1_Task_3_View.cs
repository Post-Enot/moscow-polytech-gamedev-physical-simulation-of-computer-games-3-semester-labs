using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public sealed class Lab_1_Task_3_View : TaskViewBase
    {
        [SerializeField] private GameObject _sphere;
        [SerializeField] private Lab_1_Task_3 _task;
        [SerializeField] private Lab_1_Task_3_UI _ui;
        private Vector3 _positionOffset;

        private void Start()
        {
            _positionOffset = _sphere.transform.position;
            _simulation = new CoroutineShell(this, DisplaySimulation);
            _task.SimulationStarted += StartSimulationDisplay;
            _task.SimulationEnded += StopSimulationDisplay;

            _ui.StartSpeedField.RegisterValueChangedCallback(StartSpeedFieldVCC);
            _ui.AccelerationField.RegisterValueChangedCallback(AccelerationFieldVCC);
            _ui.StartPositionField.RegisterValueChangedCallback(StartPositionFieldVCC);

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

        private void StartSpeedFieldVCC(ChangeEvent<Vector2> context)
        {
            _task.StartSpeedUnitPerSecond = context.newValue;
        }

        private void StartPositionFieldVCC(ChangeEvent<Vector2> context)
        {
            _task.StartPosition = context.newValue;
        }

        private void AccelerationFieldVCC(ChangeEvent<Vector2> context)
        {
            _task.Acceleration = context.newValue;
        }

        protected override IEnumerator DisplaySimulation()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                _sphere.transform.position = _positionOffset +
                    new Vector3(
                        x: _task.CurrentPosition.x,
                        y: _task.CurrentPosition.y,
                        z: 0);
                _ui.TimeField.value = _task.TimeInSecond;
                _ui.CurrentPositionField.value = _task.CurrentPosition;
                _ui.PathField.value = _task.PathInUnit;
                _ui.CurrentSpeedField.value = _task.CurrentSpeedUnitPerSecond;
            }
        }
    }
}
