using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public class Lab_4_Task_1_View : TaskViewBase
    {
        [SerializeField] private GameObject _sphere;
        [SerializeField] private Lab_4_Task_1 _task;
        [SerializeField] private Lab_4_Task_1_UI _ui;

        private Vector3 _positionOffset;

        private void Start()
        {
            _positionOffset = _sphere.transform.position;
            _simulation = new CoroutineShell(this, DisplaySimulation);
            _task.SimulationStarted += StartSimulationDisplay;
            _task.SimulationEnded += StopSimulationDisplay;
            _ui.StartPositionField.RegisterValueChangedCallback(StartPositionVCC);
            _ui.TimeOffsetField.RegisterValueChangedCallback(TimeOffsetVCC);
            _ui.SpeedField.RegisterValueChangedCallback(SpeedFieldVCC);
            _ui.RadiusField.RegisterValueChangedCallback(RadiusFieldVCC);
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

        private void StartPositionVCC(ChangeEvent<Vector3> context)
        {
            _task.StartPosition = context.newValue;
        }

        private void TimeOffsetVCC(ChangeEvent<float> context)
        {
            _task.TimeOffsetInSecond = context.newValue;
        }

        private void SpeedFieldVCC(ChangeEvent<Vector2> context)
        {
            _task.SpeedUnitInSecond = context.newValue;
        }

        private void RadiusFieldVCC(ChangeEvent<float> context)
        {
            _task.RadiusInUnit = context.newValue;
        }

        protected override IEnumerator DisplaySimulation()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                _sphere.transform.position = _positionOffset + _task.CurrentPosition;
                _ui.TimeField.value = _task.TimeInSecond;
                _ui.PathField.value = _task.PathInUnit;
            }
        }
    }
}
