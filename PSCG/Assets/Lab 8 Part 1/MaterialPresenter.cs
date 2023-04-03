using System.Collections.Generic;
using UnityEngine;

namespace PSCG.Labs
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class MaterialPresenter : MonoBehaviour
    {
        [SerializeField] private float _materialAlphaValue = 0.25f;

        public MaterialData MaterialData { get; private set; }

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Init(MaterialData materialData)
        {
            MaterialData = materialData;
            _spriteRenderer.size = new Vector2(materialData.ThicknessInUnit, _spriteRenderer.size.y);
            Vector2 size = _spriteRenderer.size - new Vector2(0.05f, 0.05f);

            Vector2 aa = new(-size.x / 2, size.y / 2);
            Vector2 ba = new(size.x / 2, size.y / 2);
            Vector2 bb = new(size.x / 2, -size.y / 2);
            Vector2 ab = new(-size.x / 2, -size.y / 2);

            EdgeCollider2D sideAABA = gameObject.AddComponent<EdgeCollider2D>();
            _ = sideAABA.SetPoints(new List<Vector2>() { aa, ba });
            EdgeCollider2D sideBABB = gameObject.AddComponent<EdgeCollider2D>();
            _ = sideBABB.SetPoints(new List<Vector2>() { ba, bb });
            EdgeCollider2D sideBBAB = gameObject.AddComponent<EdgeCollider2D>();
            _ = sideBBAB.SetPoints(new List<Vector2>() { bb, ab });
            EdgeCollider2D sideABAA = gameObject.AddComponent<EdgeCollider2D>();
            _ = sideABAA.SetPoints(new List<Vector2>() { ab, aa });

            _spriteRenderer.color = Random.ColorHSV();
            _spriteRenderer.color = new Color(
                _spriteRenderer.color.r,
                _spriteRenderer.color.b,
                _spriteRenderer.color.g,
                _materialAlphaValue);
        }
    }
}
