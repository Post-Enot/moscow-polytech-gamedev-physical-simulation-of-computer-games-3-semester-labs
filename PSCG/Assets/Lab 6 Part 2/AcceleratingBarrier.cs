using UnityEngine;

namespace PSCG.Labs
{
    public sealed class AcceleratingBarrier : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            collision.rigidbody.velocity = collision.rigidbody.velocity * 2;
        }
    }
}
