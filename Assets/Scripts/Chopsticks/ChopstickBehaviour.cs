using System;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;

class ChopstickBehaviour : MonoBehaviour {

    public float CurrentLength { get; private set; }
    private GameObject _lastPart = null;
    public GameObject[] testParts;
    public AnimationCurve FetchPositionCurve;
    public float FetchDuration;
    public float FetchCheckRadius;

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

    public async void Fetch(Vector2 targetPosition, Action<GameObject> onFetch) {
        transform.localPosition = new Vector3(targetPosition.x, transform.localPosition.y, transform.localPosition.z);
        float t = 0;
        while (t < FetchDuration) {
            t += Time.deltaTime;
            float y = FetchPositionCurve.Evaluate(t / FetchDuration) * targetPosition.y;
            if (y > transform.localPosition.y) {
                GameObject food = Physics2D.OverlapCircleAll(transform.position, FetchCheckRadius)
                .Where(c => c.CompareTag("Food"))
                .OrderBy(collider => {
                    return Vector2.Distance(collider.transform.position, transform.position);
                }).FirstOrDefault()?.gameObject;
                onFetch(food);
            }
            transform.localPosition = new Vector3(targetPosition.x, y, transform.localPosition.z);
            await Task.Yield();
        }
    }

    private Vector3 _getNextPartPosition(float nextLength) {
        if (_lastPart == null) {
            return new Vector3(0, 0, 0);
        }
        return _lastPart.transform.localPosition - new Vector3(0, (_lastPart.transform.localScale.y + nextLength) * 0.5f, 0);
    }
}