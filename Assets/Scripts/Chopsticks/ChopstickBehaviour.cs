using System;
using UnityEngine;

public class ChopstickBehaviour : MonoBehaviour {

    public float CurrentLength { get; private set; }
    private GameObject _lastPart = null;
    public GameObject[] InitialParts;

    [ReadOnly, SerializeField]
    private float _sumScaleY;

    private void Start() {
        CurrentLength = 0;
        _sumScaleY = 0;
        foreach (var part in InitialParts) {
            AddPart(part);
        }
    }

    public void AddPart(GameObject partPrefab) {
        GameObject part = Instantiate(partPrefab);
        part.transform.parent = transform;
        part.transform.localPosition = _getNextPartPosition(part.GetComponent<SpriteRenderer>().bounds.size.y);
        part.transform.localRotation = Quaternion.identity;
        if (_lastPart != null) {
            float k = part.GetComponent<SpriteRenderer>().bounds.size.x / _lastPart.GetComponent<SpriteRenderer>().bounds.size.x;
            part.transform.localScale = new Vector3(part.transform.localScale.x / k, part.transform.localScale.y / k, 1);
        }
        _lastPart = part;
        CurrentLength += part.GetComponent<ChopstickPartBehaviour>().Length;
        _sumScaleY += part.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private Vector3 _getNextPartPosition(float nextLength) {
        if (_lastPart == null) {
            return new Vector3(0, 0, 0);
        }
        return _lastPart.transform.localPosition - new Vector3(0, (_lastPart.GetComponent<SpriteRenderer>().bounds.size.y + nextLength) * 0.5f, 0);
    }

    public float GetSumScale() {
        return _sumScaleY;
    }

    public float GetBottom() {
        return transform.position.y - _sumScaleY / 2;
    }
}