using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FoodBehaviour : MonoBehaviour
{
    public float DurationSeconds = 10f;
    private float _remainTime;
    public AnimationCurve AlphaCurve;
    public float Speed;
    public float MaxAngularSpeed;
    public float SpeedUpdateInterval;
    public float SpeedUpdateDuration;
    private float _speedUpdateRemainTime;
    private Rigidbody2D _rigidbody2D;
    private SpeedState _currentSpeedState;
    private Vector2 _targetSpeed;
    private Vector2 _speedBeforeUpdate;

    private enum SpeedState {
        Stable,
        Altering
    };

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _remainTime = DurationSeconds;
        _rigidbody2D.velocity = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized * Speed;
        _rigidbody2D.angularVelocity = Random.Range(-MaxAngularSpeed, MaxAngularSpeed);
        Debug.Log(name + " speed: " + _rigidbody2D.velocity);
        _speedUpdateRemainTime = SpeedUpdateInterval;
        _currentSpeedState = SpeedState.Stable;
    }

    // Update is called once per frame
    void Update()
    {
        _remainTime -= Time.deltaTime;
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.color =new Color(sp.color.r, sp.color.g, sp.color.b, AlphaCurve.Evaluate(1 - _remainTime / DurationSeconds));
        if (_remainTime < 0)
        {
            Destroy(gameObject);
        }

        if (_currentSpeedState == SpeedState.Stable) {
            _speedUpdateRemainTime -= Time.deltaTime;
            if (_speedUpdateRemainTime < 0) {
                _currentSpeedState = SpeedState.Altering;
                _speedUpdateRemainTime = SpeedUpdateDuration;
                _targetSpeed = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                _targetSpeed = _targetSpeed.normalized * Speed;
                _speedBeforeUpdate = _rigidbody2D.velocity;
            }
        } else if (_currentSpeedState == SpeedState.Altering) {
            _speedUpdateRemainTime -= Time.deltaTime;
            if (_speedUpdateRemainTime < 0) {
                _currentSpeedState = SpeedState.Stable;
                _speedUpdateRemainTime = SpeedUpdateInterval;
            } else {
                float t = 1 - _speedUpdateRemainTime / SpeedUpdateDuration;
                _rigidbody2D.velocity = Vector2.Lerp(_speedBeforeUpdate, _targetSpeed, t);
            }
        }
    }

    public void OnSonarDetected() {
        
    }
}
