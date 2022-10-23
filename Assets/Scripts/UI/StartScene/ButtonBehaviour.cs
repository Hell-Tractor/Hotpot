using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour {
    public float Distance = 30f;
    public GameObject Target;
    private Vector2 _originalPosition;

    private void Start() {
        _originalPosition = Target.GetComponent<RectTransform>().anchoredPosition;
    }

    private void Update() {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) {
            position = Input.mousePosition
        }, results);
        if (results.Any(x => x.gameObject == gameObject)) {
            Target.GetComponent<RectTransform>().anchoredPosition = _originalPosition + new Vector2(0, Distance);
        } else {
            Target.GetComponent<RectTransform>().anchoredPosition = _originalPosition;
        }
    }
}
