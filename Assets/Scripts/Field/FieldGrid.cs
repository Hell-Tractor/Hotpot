using System;
using UnityEngine;

[Serializable]
public class FieldGrid {
    [SerializeField]
    private Vector2[] Forces = new Vector2[0];
    [SerializeField]
    private float TimeOut;
    private float _elapsedTime;
    private int _currentForceIndex;
    public Vector2 Force {
        get {
            if (Forces.Length == 0)
                return new Vector2(0, 0);
            return Forces[_currentForceIndex];
        }
    }
    public FieldGrid() {
        _elapsedTime = 0;
        _currentForceIndex = 0;
    }

    public void Update() {
        if (Forces.Length <= 1)
            return;
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= TimeOut) {
            _elapsedTime = 0;
            _currentForceIndex = (_currentForceIndex + 1) % Forces.Length;
        }
    }
}