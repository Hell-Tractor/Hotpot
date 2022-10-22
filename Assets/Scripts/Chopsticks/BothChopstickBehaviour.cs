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

    public async void Fetch(Vector2 targetPosition, Action<GameObject> onFetch) {
        transform.localPosition = new Vector3(targetPosition.x, transform.localPosition.y, transform.localPosition.z);
        float _sumScaleY = (LeftChopstick.GetSumScale() + RightChopstick.GetSumScale()) * 0.5f;
        targetPosition += new Vector2(0, _sumScaleY / 2);
        float t = 0;
        while (t < FetchDuration) {
            t += Time.deltaTime;
            float y = FetchPositionCurve.Evaluate(t / FetchDuration) * targetPosition.y;
            if (y > transform.localPosition.y) {
                GameObject food = Physics2D.OverlapCircleAll(targetPosition - new Vector2(0, _sumScaleY / 2), FetchCheckRadius)
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
}
