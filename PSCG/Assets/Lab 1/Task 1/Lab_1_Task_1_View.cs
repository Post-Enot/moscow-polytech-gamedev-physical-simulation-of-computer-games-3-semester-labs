using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public sealed class Lab_1_Task_1_View : TaskViewBase
    {
        [SerializeField] private GameObject _sphere;
        [SerializeField] private Lab_1_Task_1 _task;
        [SerializeField] private Lab_1_Task_1_UI _ui;

        private Vector3 _positionOffset;

        private void Start()
        {
            _positionOffset = _sphere.transform.position;
            _simulation = new CoroutineShell(this, DisplaySimulation);
            _task.SimulationStarted += StartSimulationDisplay;
            _task.SimulationEnded += StopSimulationDisplay;
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

        private void SpeedFieldVCC(ChangeEvent<Vector2> context)
        {
            _task.SpeedUnitPerSecond = context.newValue;
        }

        protected override IEnumerator DisplaySimulation()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                _sphere.transform.position = _positionOffset + new Vector3(
                    x: _task.CurrentPosition.x,
                    y: _task.CurrentPosition.y,
                    z: 0);
                _ui.TimeField.value = _task.TimeInSecond;
                _ui.PathField.value = _task.PathInUnit;
            }
        }
    }
}
