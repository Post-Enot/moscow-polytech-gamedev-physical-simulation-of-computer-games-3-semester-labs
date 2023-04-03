using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public class Lab_5_Task_1_View : TaskViewBase
    {
        [SerializeField] private GameObject _sphere;
        [SerializeField] private Lab_5_Task_1 _task;
        [SerializeField] private Lab_5_Task_1_UI _ui;

        private Vector3 _positionOffset;

        private void Start()
        {
            _positionOffset = _sphere.transform.position;
            _simulation = new CoroutineShell(this, DisplaySimulation);
            _task.SimulationStarted += StartSimulationDisplay;
            _task.SimulationEnded += StopSimulationDisplay;

            _ui.HeightField.RegisterValueChangedCallback(HeightFieldVCC);
            _ui.SpeedField.RegisterValueChangedCallback(SpeedFieldVCC);

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

        private void HeightFieldVCC(ChangeEvent<float> context)
        {
            _task.HeightInUnit = context.newValue;
        }

        private void SpeedFieldVCC(ChangeEvent<float> context)
        {
            _task.SpeedUnitPerSecond = context.newValue;
        }

        protected override IEnumerator DisplaySimulation()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                _sphere.transform.position = new Vector3(
                    _positionOffset.x + _task.CurrentPosition.x,
                    _positionOffset.y + _task.CurrentPosition.y,
                    _positionOffset.z);
                _ui.TimeField.value = _task.TimeInSecond;
                _ui.DistanceField.value = _task.DistanceInUnit;
            }
        }
    }
}
