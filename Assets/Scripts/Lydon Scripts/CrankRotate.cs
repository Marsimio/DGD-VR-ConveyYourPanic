using System;
using UnityEngine;

public class CrankRotate : MonoBehaviour
{
    private HingeJoint _hinge;

    private float _lastAngle;
    private float _accumulated;
    public int value;

    private void Awake()
    {
        _hinge = GetComponent<HingeJoint>();

        if (_hinge == null)
        {
            Debug.LogError("No HingeJoint found", this);
            enabled = false;
            return;
        }

        _lastAngle = _hinge.angle;
    }

    private void Update()
    {
        float currentAngle = _hinge.angle;
        float delta = Mathf.DeltaAngle(_lastAngle, currentAngle);

        _accumulated += delta;
        _lastAngle = currentAngle;

        if (Mathf.Abs(_accumulated) >= 360f)
        {
            int turns = Mathf.FloorToInt(Mathf.Abs(_accumulated) / 360f);
            StatManager.Instance.energy += turns * 10;

            _accumulated %= 360f;
        }
    }
}
