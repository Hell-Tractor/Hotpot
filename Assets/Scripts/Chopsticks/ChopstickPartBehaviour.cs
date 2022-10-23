using UnityEngine;

public class ChopstickPartBehaviour : MonoBehaviour {
    public float Length;
    private void Start() {
        Length = this.transform.localScale.y;
    }
}
