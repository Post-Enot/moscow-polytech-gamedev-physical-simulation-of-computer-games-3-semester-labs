using System;
using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    [RequireComponent(typeof(Lab_7), typeof(Lab_7_UI))]
    public class Lab_7_View : TaskViewBase
    {
        private Lab_7 _task;
        private Lab_7_UI _ui;

        private void Awake()
        {
            _task = GetComponent<Lab_7>();
            _ui = GetComponent<Lab_7_UI>();
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
            };
            _ui.StopButton.clicked += () =>
            {
                if (_task.IsSimulationStarted)
                {
                    _task.StopSimulation();
                }
            };

            _ui.StartPositionField.RegisterValueChangedCallback(StartPositionVCC);
            _ui.StartVelocityField.RegisterValueChangedCallback(context => _task.StartVelocity = context.newValue);
            _ui.A_Field.RegisterValueChangedCallback(context => _task.A = context.newValue);

            _ui.Floor1_FrictionField.RegisterValueChangedCallback(context =>
            {
                _task.Floor1_PhysicMaterial.staticFriction = context.newValue;
                _task.Floor1_PhysicMaterial.dynamicFriction = context.newValue;
            });
            _ui.Floor2_FrictionField.RegisterValueChangedCallback(context =>
            {
                _task.Floor2_PhysicMaterial.staticFriction = context.newValue;
                _task.Floor2_PhysicMaterial.dynamicFriction = context.newValue;
            });
            _ui.Floor3_FrictionField.RegisterValueChangedCallback(context =>
            {
                _task.Floor3_PhysicMaterial.staticFriction = context.newValue;
                _task.Floor3_PhysicMaterial.dynamicFriction = context.newValue;
            });
        }

        private void StartPositionVCC(ChangeEvent<string> context)
        {
            _task.StartPosition = context.newValue switch
            {
                Lab_7_UI.Floor1_DropdownChoice => _task.StartPositionOnFloor1,
                Lab_7_UI.Floor2_DropdownChoice => _task.StartPositionOnFloor2,
                Lab_7_UI.Floor3_DropdownChoice => _task.StartPositionOnFloor3,
                _ => throw new ArgumentOutOfRangeException(nameof(context.newValue)),
            };
            _task.SphereRigidbody.Sleep();
            _task.SphereRigidbody.transform.SetPositionAndRotation(_task.StartPosition.position, Quaternion.identity);
        }

        protected override IEnumerator DisplaySimulation()
        {
            yield return null;
        }
    }
}
