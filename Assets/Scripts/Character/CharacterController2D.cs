using System.Globalization;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour {
    [Header("Controls")]
    public string Axis_X = "Horizontal";
    public string Axis_Y = "Vertical";
    public string ButtonJump = "Jump";

    [Header("Properties")]
    public float Speed = 5f;
    public float JumpForce = 5f;
    public int MaxJumpTime = 1;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private int _currentJumpTime = 0;
    public Vector2 Direction { get; private set; }

    private void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _currentJumpTime = MaxJumpTime;
    }

    private void Update() {
        this._processMovement();
        this._processJump();
    }

    private void _processMovement() {
        var x = Input.GetAxis(Axis_X);
        var y = Input.GetAxis(Axis_Y);

        if (!Mathf.Approximately(x, 0)) {
            Direction = new Vector2(x, Direction.y);
        }

        if (MaxJumpTime > 0) {
            _rigidbody.velocity = new Vector2(x * Speed, _rigidbody.velocity.y);
        } else {
            _rigidbody.velocity = new Vector2(x * Speed, y * Speed);
            if (!Mathf.Approximately(y, 0)) {
                Direction = new Vector2(Direction.x, y);
            }
        }
    }

    private void _processJump() {
        _onGroundCheck();
        if (Input.GetButtonDown(ButtonJump) && _currentJumpTime > 0) {
            _currentJumpTime--;
            _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    private void _onGroundCheck() {
        if (_collider != null) {
            int hit = _collider.Cast(Vector2.down, new ContactFilter2D() {
                useTriggers = false,
                useLayerMask = true,
                layerMask = LayerMask.GetMask("Ground")
            }, new RaycastHit2D[1], 0.1f);

            if (hit > 0 && _rigidbody.velocity.y < 1e-4)
                _currentJumpTime = MaxJumpTime;
        }
    }
}
