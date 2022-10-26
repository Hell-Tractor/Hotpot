using UnityEngine;

public class MaskController : MonoBehaviour {
    public float Speed = 1f;
    [ReadOnly]
    public float TargetY;

    private void Start() {
        TargetY = transform.position.y;
    }

    private void Update() {
        if (transform.position.y > TargetY) {
            transform.position = new Vector3(transform.position.x, transform.position.y - Speed * Time.deltaTime, transform.position.z);
        }
    }
}
