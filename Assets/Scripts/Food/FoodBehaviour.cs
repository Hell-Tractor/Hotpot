using UnityEngine;

public class FoodBehaviour : MonoBehaviour
{
    public float DurationSeconds = 10f;
    private float _remainTime;
    public AnimationCurve AlphaCurve;
    public Vector2 Speed;
    private Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _remainTime = DurationSeconds;
        _rigidbody2D.velocity = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        _remainTime -= Time.deltaTime;
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.color =new Color(sp.color.r,sp.color.g,sp.color.b, AlphaCurve.Evaluate(1 - _remainTime / DurationSeconds));
        if (_remainTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
