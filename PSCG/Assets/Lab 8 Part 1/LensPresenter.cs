using System;
using System.Collections.Generic;
using UnityEngine;

namespace PSCG.Labs
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public sealed class LensPresenter : MonoBehaviour
    {
        [SerializeField] private int _lensPointCount;

        public LensData LensData { get; private set; }

        private float Radius => LensData.HeightInUnit / 2;
        private Vector2 BoxSize => new(LensData.ThicknessInUnit / 2, Radius);

        private EdgeCollider2D _aabaCollider;
        private EdgeCollider2D _babbCollider;
        private EdgeCollider2D _bbabCollider;
        private EdgeCollider2D _abaaCollider;
        private MeshFilter _meshFilter;

        public void Init(LensData lensData)
        {
            if (_aabaCollider != null)
            {
                Destroy(_aabaCollider);
            }
            if (_babbCollider != null)
            {
                Destroy(_babbCollider);
            }
            if (_bbabCollider != null)
            {
                Destroy(_bbabCollider);
            }
            if (_abaaCollider != null)
            {
                Destroy(_abaaCollider);
            }

            LensData = lensData;

            Vector2 aa = new(-BoxSize.x, BoxSize.y);
            Vector2 ba = new(BoxSize.x, BoxSize.y);
            Vector2 bb = new(BoxSize.x, -BoxSize.y);
            Vector2 ab = new(-BoxSize.x, -BoxSize.y);

            _aabaCollider = gameObject.AddComponent<EdgeCollider2D>();
            _aabaCollider.SetPoints(new List<Vector2>() { aa, ba });
            _bbabCollider = gameObject.AddComponent<EdgeCollider2D>();
            _bbabCollider.SetPoints(new List<Vector2>() { bb, ab });
            InitInputLensCollider();
            InitOutputLensCollider();
            InitCustomMesh();
        }

        private void InitCustomMesh()
        {
            int verticesCount = _aabaCollider.pointCount +
                _babbCollider.pointCount +
                _bbabCollider.pointCount +
                _abaaCollider.pointCount;
            var points = new Vector2[verticesCount];

            int i = 0;
            foreach (Vector2 point in _aabaCollider.points)
            {
                points[i] = new Vector2(point.x, point.y);
                i += 1;
            }
            foreach (Vector2 point in _babbCollider.points)
            {
                points[i] = new Vector2(point.x, point.y);
                i += 1;
            }
            foreach (Vector2 point in _bbabCollider.points)
            {
                points[i] = new Vector2(point.x, point.y);
                i += 1;
            }
            foreach (Vector2 point in _abaaCollider.points)
            {
                points[i] = new Vector2(point.x, point.y);
                i += 1;
            }
            PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
            polygonCollider.SetPath(0, points);
            Mesh lensMesh = polygonCollider.CreateMesh(false, false);
            Destroy(polygonCollider);
            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter.mesh = lensMesh;
        }

        private void InitInputLensCollider()
        {
            _abaaCollider = gameObject.AddComponent<EdgeCollider2D>();
            List<Vector2> points = new();
            GetLensPointsByType(
                LensData.InputLensType,
                LensData.SagittalSize,
                Radius,
                _lensPointCount,
                ref points);
            if (LensData.InputLensType is LensType.Concave or LensType.Convex)
            {
                ReverseUnflatLensPoints(ref points);
            }
            float xOffset = BoxSize.x;
            for (int i = 0; i < points.Count; i += 1)
            {
                points[i] = new Vector2(
                    points[i].x - xOffset,
                    points[i].y);
            }
            _abaaCollider.SetPoints(points);
        }

        private void InitOutputLensCollider()
        {
            _babbCollider = gameObject.AddComponent<EdgeCollider2D>();
            List<Vector2> points = new();
            GetLensPointsByType(
                LensData.OutputLensType,
                LensData.SagittalSize,
                Radius,
                _lensPointCount,
                ref points);
            float xOffset = BoxSize.x;
            for (int i = 0; i < points.Count; i += 1)
            {
                points[i] = new Vector2(
                    points[i].x + xOffset,
                    points[i].y);
            }
            _babbCollider.SetPoints(points);
        }

        private static void GetLensPointsByType(
            LensType lensType,
            float sagittalSize,
            float radius,
            int pointsCount,
            ref List<Vector2> points)
        {
            switch (lensType)
            {
                case LensType.Flat:
                    points.Add(new Vector2(0, radius));
                    points.Add(new Vector2(0, -radius));
                    return;

                case LensType.Concave:
                    GetUnflatLensPoints(sagittalSize, radius, pointsCount, ref points);
                    ReverseUnflatLensPoints(ref points);
                    return;

                case LensType.Convex:
                    GetUnflatLensPoints(sagittalSize, radius, pointsCount, ref points);
                    return;

                default:
                    throw new ArgumentOutOfRangeException(nameof(lensType));
            }
        }

        private static void ReverseUnflatLensPoints(ref List<Vector2> points)
        {
            for (int i = 0; i < points.Count; i += 1)
            {
                points[i] = new Vector2(
                    -points[i].x,
                    points[i].y);
            }
        }

        private static void GetUnflatLensPoints(
            float sagittalSize,
            float radius,
            int pointsCount,
            ref List<Vector2> points)
        {
            float xOffset = radius - sagittalSize;
            float offsetInRadians = Mathf.Acos(xOffset / radius);
            float startAngleInRadians = (3 * Mathf.PI / 2) + ((Mathf.PI / 2) - offsetInRadians);
            float finalAngleInRadians = (Mathf.PI * 2) + offsetInRadians;
            float stepInRadians = (finalAngleInRadians - startAngleInRadians) / pointsCount;

            Vector2 point = GetCirclePointByAngle(radius, startAngleInRadians);
            float yFactor = radius / point.y;

            for (int i = 0; i <= pointsCount; i += 1)
            {
                float angleInRadians = startAngleInRadians + (stepInRadians * i);
                point = GetLensPoint(radius, angleInRadians, xOffset, yFactor);
                points.Add(point);
            }
        }

        private static Vector2 GetLensPoint(float radius, float angleInRadians, float xOffset, float yFactor)
        {
            Vector2 point = GetCirclePointByAngle(radius, angleInRadians);
            point.x -= xOffset;
            point.y *= yFactor;
            return point;
        }

        private static Vector2 GetCirclePointByAngle(float radius, float angleInRadians)
        {
            return new Vector2(
                x: radius * Mathf.Cos(angleInRadians),
                y: radius * Mathf.Sin(angleInRadians));
        }
    }
}
