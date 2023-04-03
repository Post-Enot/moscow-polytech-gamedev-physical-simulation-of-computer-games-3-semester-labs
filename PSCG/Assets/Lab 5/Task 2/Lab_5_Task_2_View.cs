using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    [RequireComponent(typeof(Lab_5_Task_2), typeof(Lab_5_Task_2_UI))]
    public class Lab_5_Task_2_View : TaskViewBase
    {
        [SerializeField] private GameObject _sphere;
        private Lab_5_Task_2 _task;
        private Lab_5_Task_2_UI _ui;

        private Vector3 _positionOffset;

        private void Awake()
        {
            _task = GetComponent<Lab_5_Task_2>();
            _ui = GetComponent<Lab_5_Task_2_UI>();
        }

        private void Start()
        {
            _positionOffset = _sphere.transform.position;

            _simulation = new CoroutineShell(this, DisplaySimulation);
            _task.SimulationStarted += StartSimulationDisplay;
            _task.SimulationEnded += StopSimulationDisplay;

            _ui.HeightField.RegisterValueChangedCallback(context => _task.Height = context.newValue);
            _ui.TimeOffsetField.RegisterValueChangedCallback(context => _task.TimeOffset = context.newValue);
            _ui.AccelerationField.RegisterValueChangedCallback(context => _task.SingleAcceleration = context.newValue);
            _ui.AngleToTheHorizonField.RegisterValueChangedCallback(context => _task.AngleToTheHorizon = context.newValue);

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


        protected override IEnumerator DisplaySimulation()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                _sphere.transform.position = _positionOffset + new Vector3(
                    _task.CurrentPosition.x,
                    _task.CurrentPosition.y,
                    0);
                _ui.TimeField.value = _task.CurrentTime;
                _ui.DistanceField.value = _task.Distance;
            }
        }
    }
}
