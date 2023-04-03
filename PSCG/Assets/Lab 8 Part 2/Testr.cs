using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testr : MonoBehaviour
{
    [SerializeField] private Vector2 _point1;
    [SerializeField] private Vector2 _point2;

    void Update()
    {
        Debug.Log(Vector2.SignedAngle(_point1, _point2));
        Debug.DrawRay(Vector2.zero, _point1);
        Debug.DrawRay(Vector2.zero, _point2);
    }
}
