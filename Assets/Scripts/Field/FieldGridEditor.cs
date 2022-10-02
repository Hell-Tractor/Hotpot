using UnityEngine;

public class FieldGridEditor : MonoBehaviour {
    public FieldGrid data;
    public float LengthModulus = 5f;

    private void OnDrawGizmos() {
        Gizmos.color = new Color(0.5f, 0.5f, 1f);
        // draw force
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)data.Force / LengthModulus);
    }
}
