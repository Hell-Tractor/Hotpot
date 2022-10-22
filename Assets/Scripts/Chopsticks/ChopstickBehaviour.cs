using UnityEngine;

class ChopstickBehaviour : MonoBehaviour {

    public float CurrentLength { get; private set; }
    private GameObject _lastPart = null;
    public GameObject[] testParts;

    private void Start() {
        CurrentLength = 0;
        foreach (var part in testParts) {
            AddPart(part);
        }
    }

    public void AddPart(GameObject partPrefab) {
        GameObject part = Instantiate(partPrefab);
        part.transform.parent = transform;
        part.transform.localPosition = _getNextPartPosition(part.transform.localScale.y);
        _lastPart = part;
        CurrentLength += part.GetComponent<ChopstickPartBehaviour>().Length;
    }

    private Vector3 _getNextPartPosition(float nextLength) {
        if (_lastPart == null) {
            return new Vector3(0, 0, 0);
        }
        return _lastPart.transform.localPosition - new Vector3(0, (_lastPart.transform.localScale.y + nextLength) * 0.5f, 0);
    }
}