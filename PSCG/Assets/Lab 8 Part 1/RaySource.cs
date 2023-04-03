using UnityEngine;
using IUP.Toolkits;
using System.Collections.Generic;

namespace PSCG.Labs
{
    [RequireComponent(typeof(LineRenderer))]
    public sealed class RaySource : MonoBehaviour
    {
        [SerializeField] private string _mirrorTag = "Mirror";
        [SerializeField] private string _barrierTag = "Barrier";
        [SerializeField] private float _minDistanceBetweenPoints = 0.005f;

        public float EnvironmentRefractiveIndex { get; set; }
        public float RayCastAngleInDegrees { get; set; }

        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void CastRay()
        {
            _lineRenderer.positionCount = 0;
            bool isHit;
            SetLineRendererPoint(_lineRenderer.transform.position);
            float rayAngleInRadians = Mathf.Deg2Rad * RayCastAngleInDegrees;
            Vector3 rayCastSource = _lineRenderer.transform.position;
            Vector2 direction = ExtendedMath.RotateVector2(Vector2.right, rayAngleInRadians);
            do
            {
                RaycastHit2D hit = RaycastWithMinDistance(rayCastSource, direction, Color.red);
                isHit = hit.rigidbody != null;
                if (isHit)
                {
                    SetLineRendererPoint(hit.point);
                    if (hit.rigidbody.gameObject.CompareTag(_barrierTag))
                    {
                        return;
                    }
                    if (hit.rigidbody.TryGetComponent(out FinalPosition finalPosition))
                    {
                        finalPosition.Activate();
                        return;
                    }

                    if (hit.rigidbody.gameObject.CompareTag(_mirrorTag))
                    {
                        CalculateMirrorDirection(hit, ref direction);
                        rayCastSource = hit.point;
                        continue;
                    }

                    if (hit.rigidbody.TryGetComponent(out MaterialPresenter materialPresenter))
                    {
                        CalculateReflectionDirection(
                            hit,
                            EnvironmentRefractiveIndex,
                            materialPresenter.MaterialData.RefractiveIndex,
                            ref direction);
                        rayCastSource = hit.point;

                        hit = RaycastWithMinDistance(rayCastSource, direction, Color.green);
                        SetLineRendererPoint(hit.point);

                        CalculateReflectionDirection(
                            hit,
                            materialPresenter.MaterialData.RefractiveIndex,
                            EnvironmentRefractiveIndex,
                            ref direction);
                        rayCastSource = hit.point;
                    }
                    if (hit.rigidbody.TryGetComponent(out LensPresenter lensPresenter))
                    {
                        CalculateReflectionDirection(
                            hit,
                            EnvironmentRefractiveIndex,
                            lensPresenter.LensData.RefractiveIndex,
                            ref direction);
                        rayCastSource = hit.point;

                        hit = RaycastWithMinDistance(rayCastSource, direction, Color.green);
                        SetLineRendererPoint(hit.point);

                        CalculateReflectionDirection(
                            hit,
                            lensPresenter.LensData.RefractiveIndex,
                            EnvironmentRefractiveIndex,
                            ref direction);
                        rayCastSource = hit.point;
                    }
                }
                else
                {
                    Vector3 point = rayCastSource + new Vector3(direction.x, direction.y, 0) * 100;
                    SetLineRendererPoint(point);
                }
            }
            while (isHit);
        }

        private void CalculateMirrorDirection(
            RaycastHit2D hit,
            ref Vector2 direction)
        {
            var edgeCollider = hit.collider as EdgeCollider2D;
            Vector2 edgePerpendicular = GetHitPerpendicular(edgeCollider, hit.point);

            Vector2 inversedDirection = ExtendedMath.RotateVector2(direction.normalized, Mathf.PI);
            if (Vector2.Angle(inversedDirection, edgePerpendicular) > (90f))
            {
                edgePerpendicular = ExtendedMath.RotateVector2(edgePerpendicular, Mathf.PI);
            }
            Debug.DrawRay(hit.point, edgePerpendicular);
            //

            float angleFromPToDInDegrees = Vector2.SignedAngle(edgePerpendicular, inversedDirection);
            float angleFromPToDInRadians = -(angleFromPToDInDegrees * Mathf.Deg2Rad);

            float mirrorAngle = angleFromPToDInRadians;
            direction = ExtendedMath.RotateVector2(edgePerpendicular, mirrorAngle);
        }

        private void CalculateReflectionDirection(
            RaycastHit2D hit,
            float refrectiveIndexInput,
            float refrectiveIndexOutput,
            ref Vector2 direction)
        {
            var edgeCollider = hit.collider as EdgeCollider2D;
            Vector2 edgePerpendicular = GetHitPerpendicular(edgeCollider, hit.point);

            Vector2 d = direction.normalized;
            float dAngle = Mathf.Atan2(d.y, d.x);
            float pAngle = Mathf.Atan2(edgePerpendicular.y, edgePerpendicular.x);
            float incidenceAngleInRadians = dAngle - pAngle;

            if (Mathf.Abs(incidenceAngleInRadians) >= (Mathf.PI / 2))
            {
                edgePerpendicular = ExtendedMath.RotateVector2(edgePerpendicular, Mathf.PI);
                pAngle = Mathf.Atan2(edgePerpendicular.y, edgePerpendicular.x);
                incidenceAngleInRadians = dAngle - pAngle;
            }
            Debug.DrawRay(hit.point, edgePerpendicular);

            float sinY = Mathf.Sin(incidenceAngleInRadians) / (refrectiveIndexOutput / refrectiveIndexInput);
            float reflectionAngleInRadian = Mathf.Asin(sinY);
            direction = ExtendedMath.RotateVector2(edgePerpendicular, reflectionAngleInRadian);
        }

        private void SetLineRendererPoint(Vector2 position)
        {
            _lineRenderer.positionCount += 1;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, position);
        }

        private (Vector2, Vector2) GetTwoNearestPointsToHitPoint(
            EdgeCollider2D edgeCollider,
            Vector2 hitPoint)
        {
            Vector2 a = default;
            float aHitDistance = float.MaxValue;
            Vector2 b = default;
            float bHitDistance = float.MaxValue;
            foreach (Vector2 point in edgeCollider.points)
            {
                float hitDistance = Vector2.Distance(
                    hitPoint,
                    edgeCollider.transform.TransformPoint(point));
                if (hitDistance < aHitDistance)
                {
                    b = a;
                    bHitDistance = aHitDistance;
                    a = edgeCollider.transform.TransformPoint(point);
                    aHitDistance = hitDistance;
                    continue;
                }
                else if (hitDistance < bHitDistance)
                {
                    b = edgeCollider.transform.TransformPoint(point);
                    bHitDistance = hitDistance;
                }
            }
            return (a, b);
        }

        private Vector2 GetHitPerpendicular(EdgeCollider2D edgeCollider, Vector2 hitPoint)
        {
            (Vector2, Vector2) twoNearestPoints = GetTwoNearestPointsToHitPoint(edgeCollider, hitPoint);
            Vector2 edgeVector = twoNearestPoints.Item2 - twoNearestPoints.Item1;
            return Vector2.Perpendicular(edgeVector);
        }

        private RaycastHit2D RaycastWithColliderMask(
            Vector2 source,
            Vector2 direction,
            HashSet<Collider2D> ignoredColliders,
            Color debugColor)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(source, direction);
            Debug.DrawRay(source, direction.normalized, debugColor, 0.5f);
            RaycastHit2D hit = new();
            foreach (RaycastHit2D hit_0 in hits)
            {
                if (ignoredColliders.Add(hit_0.collider))
                {
                    hit = hit_0;
                    break;
                }
            }
            return hit;
        }

        private RaycastHit2D RaycastWithMinDistance(
            Vector2 source,
            Vector2 direction,
            Color debugColor)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(source, direction);
            Debug.DrawRay(source, direction.normalized, debugColor, 0.5f);
            RaycastHit2D hit = new();
            foreach (RaycastHit2D hit_0 in hits)
            {
                float distance = Vector2.Distance(source, hit_0.point);
                if (distance > _minDistanceBetweenPoints)
                {
                    hit = hit_0;
                    break;
                }
            }
            return hit;
        }
    }
}
