using UnityEngine;

namespace PSCG.Labs
{
    [RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public sealed class Barrier : MonoBehaviour
    {
        private BoxCollider2D _boxCollider;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.size = new Vector2(_boxCollider.size.x, _boxCollider.size.y);
        }
    }
}
