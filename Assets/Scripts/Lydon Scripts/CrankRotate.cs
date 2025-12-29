using System;
using Managers.GameManager;
using UnityEngine;

public class CrankRotate : MonoBehaviour
{
    private HingeJoint _hinge;

    private float _lastAngle;
    private float _accumulated;

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
        if (delta < 0f)
        {
            _accumulated += -delta;
        }

        _lastAngle = currentAngle;

        if (_accumulated >= 360f)
        {
            GameManager.instance.energy += 10;
            _accumulated -= 360f;
        }
    }
}
