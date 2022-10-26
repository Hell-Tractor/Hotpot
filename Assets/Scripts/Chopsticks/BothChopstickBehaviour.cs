using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class BothChopstickBehaviour : MonoBehaviour {
    public AnimationCurve FetchPositionCurve;
    public float FetchDuration;
    public float FetchCheckRadius;
    public ChopstickBehaviour LeftChopstick;
    public ChopstickBehaviour RightChopstick;
    public GameObject Sonar;
    public enum State
    {
        Idle, Fetching
    }
    public State CurrentState { get; private set; } = BothChopstickBehaviour.State.Idle;
    public float DeltaLength { get; private set; }

    public float CurrentLength {
        get {
            return Mathf.Max(LeftChopstick.CurrentLength, RightChopstick.CurrentLength);
        }
    }

    public async void Fetch(Vector2 targetPosition, Action<GameObject> onFetch, Action<GameObject> onComplete) {
        if (CurrentState == BothChopstickBehaviour.State.Fetching)
            return;
        if (Sonar.GetComponent<SonarFSM>().CurrentState == AI.FSM.FSMStateID.SonarSpread)
            return;
        CurrentState = BothChopstickBehaviour.State.Fetching;
        transform.localPosition = new Vector3(targetPosition.x, transform.localPosition.y, transform.localPosition.z);
        Vector3 startPosition = transform.localPosition;
        float _sumScaleY = (LeftChopstick.GetSumScale() + RightChopstick.GetSumScale()) * 0.5f;
        // Debug.Log(_sumScaleY);
        targetPosition += new Vector2(0, _sumScaleY);
        float t = 0;
        GameObject food = null;
        bool taked = false;
        while (t < FetchDuration) {
            t += Time.deltaTime;
            float y = FetchPositionCurve.Evaluate(t / FetchDuration) * (targetPosition.y - startPosition.y) + startPosition.y;
            if (y > transform.localPosition.y && !taked) {
                food = Physics2D.OverlapCircleAll(targetPosition - new Vector2(0, _sumScaleY), FetchCheckRadius)
                .Where(c => c.CompareTag("Food"))
                .OrderBy(collider => {
                    return Vector2.Distance(collider.transform.position, transform.position);
                }).FirstOrDefault()?.gameObject;
                onFetch(food);
                taked = true;
            }
            transform.localPosition = new Vector3(targetPosition.x, y, transform.localPosition.z);
            await Task.Yield();
        }
        transform.localPosition = startPosition;
        onComplete(food);
        CurrentState = BothChopstickBehaviour.State.Idle;
    }

    public void AddPart(GameObject partPrefab) {
        if (LeftChopstick.CurrentLength < RightChopstick.CurrentLength) {
            LeftChopstick.AddPart(partPrefab);
        } else {
            RightChopstick.AddPart(partPrefab);
        }
        UpdateSonarPosition();
    }

    public void UpdateSonarPosition() {
        // Debug.Log("pre pos:" + Sonar.transform.position);
        // Debug.Log(-Mathf.Max(LeftChopstick.GetSumScale(), RightChopstick.GetSumScale()));
        Sonar.transform.localPosition = new Vector3(
            Sonar.transform.localPosition.x,
            -Mathf.Max(LeftChopstick.GetSumScale(), RightChopstick.GetSumScale()),
            Sonar.transform.localPosition.z
        );
        // Debug.Log("pos: " + Sonar.transform.position);
    }
}
