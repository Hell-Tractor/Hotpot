using UnityEngine;

public class ChopstickPartBehaviour : MonoBehaviour {
    public float Length {
        get {
            return GetComponent<SpriteRenderer>().bounds.size.y;
        }
    }
}
