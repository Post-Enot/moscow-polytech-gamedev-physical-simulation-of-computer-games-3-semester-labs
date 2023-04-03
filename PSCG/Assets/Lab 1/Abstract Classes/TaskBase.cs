using System;
using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;

namespace PSCG.Labs
{
    public abstract class TaskBase : MonoBehaviour
    {
        public bool IsSimulationStarted => _simulation.IsStarted;

        protected CoroutineShell _simulation;

        public event Action SimulationStarted;
        public event Action SimulationEnded;

        public void StartSimulation()
        {
            _simulation.Start();
            SimulationStarted?.Invoke();
        }

        public void StopSimulation()
        {
            _simulation.Stop();
            SimulationEnded?.Invoke();
        }

        protected abstract IEnumerator Simulation();
    }
}
