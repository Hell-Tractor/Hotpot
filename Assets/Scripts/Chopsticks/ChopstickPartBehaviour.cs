using UnityEngine;

public class ChopstickPartBehaviour : MonoBehaviour {
    public float Length = -1;

    private void Awake() {
        if (Length == -1) {
            Length = GetComponent<SpriteRenderer>().bounds.size.y;
        }
    }
}
