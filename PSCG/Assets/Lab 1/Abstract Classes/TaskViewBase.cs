using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;

namespace PSCG.Labs
{
    public abstract class TaskViewBase : MonoBehaviour
    {
        protected CoroutineShell _simulation;

        public void StartSimulationDisplay()
        {
            _simulation.Start();
        }

        public void StopSimulationDisplay()
        {
            _simulation.Stop();
        }

        protected abstract IEnumerator DisplaySimulation();
    }
}
