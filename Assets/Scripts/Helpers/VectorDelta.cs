using UnityEngine;
using System.Collections;

public struct VectorDelta
{

    private Vector3 _fromPos;
    private Vector3 _toPos;

    public Vector3 delta;
    public Vector3 direction;
    public float magnitude;
    public float angle;

    public VectorDelta(Transform fromTransform, Transform toTransform)
    {
        _fromPos = fromTransform.position;
        _toPos = toTransform.position;
        delta = _toPos - _fromPos;
        direction = delta.normalized;
        magnitude = delta.magnitude;
        angle = Mathf.Atan2(direction.z, direction.x);
    }

    public VectorDelta(Vector3 fromPos, Vector3 toPos)
    {
        _fromPos = fromPos;
        _toPos = toPos;
        delta = _toPos - _fromPos;
        direction = delta.normalized;
        magnitude = delta.magnitude;
        angle = Mathf.Atan2(direction.z, direction.x);
    }

}
