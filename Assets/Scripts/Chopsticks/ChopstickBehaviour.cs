using System;
using UnityEngine;

public class ChopstickBehaviour : MonoBehaviour {

    public float CurrentLength { get; private set; }
    private GameObject _lastPart = null;
    public GameObject[] InitialParts;

    private float _sumScaleY;

    private void Start() {
        CurrentLength = 0;
        _sumScaleY = 0;
        foreach (var part in InitialParts) {
            AddPart(part);
        }
    }

    public void AddPart(GameObject partPrefab) {
        // GameObject part = Instantiate(partPrefab);
        GameObject part = partPrefab;
        Destroy(part.GetComponent<FoodBehaviour>());
        part.transform.parent = transform;
        part.transform.localPosition = _getNextPartPosition(part.transform.localScale.y);
        part.transform.localRotation = Quaternion.identity;
        if (_lastPart != null) {
            float k = part.transform.localScale.x / _lastPart.transform.localScale.x;
            part.transform.localScale = new Vector3(_lastPart.transform.localScale.x, part.transform.localScale.y / k, 1);
        }
        _lastPart = part;
        CurrentLength += part.GetComponent<ChopstickPartBehaviour>().Length;
        _sumScaleY += part.transform.localScale.y;
    }

    private Vector3 _getNextPartPosition(float nextLength) {
        if (_lastPart == null) {
            return new Vector3(0, 0, 0);
        }
        return _lastPart.transform.localPosition - new Vector3(0, (_lastPart.transform.localScale.y + nextLength) * 0.5f, 0);
    }

    public float GetSumScale() {
        return _sumScaleY;
    }

    public float GetBottom() {
        return transform.position.y - _sumScaleY / 2;
    }
}