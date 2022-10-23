using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour {
    public float Distance = 30f;
    public GameObject Target;
    public AudioClip OnHoverSound;
    private Vector2 _originalPosition;
    private int _currentState = -1;

    private void Start() {
        _originalPosition = Target.GetComponent<RectTransform>().anchoredPosition;
    }

    private void Update() {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) {
            position = Input.mousePosition
        }, results);
        if (results.Any(x => x.gameObject == gameObject)) {
            if (_currentState != 1) {
                Target.GetComponent<RectTransform>().anchoredPosition = _originalPosition + new Vector2(0, Distance);
                if (OnHoverSound)
                    GameManager.Instance.SESource.PlayOneShot(OnHoverSound);
                _currentState = 1;
            }
        } else {
            Target.GetComponent<RectTransform>().anchoredPosition = _originalPosition;
            _currentState = 0;
        }
    }
}
