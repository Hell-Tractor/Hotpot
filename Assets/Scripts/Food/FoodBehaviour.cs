using UnityEngine;

public class FoodBehaviour : MonoBehaviour
{
    public float DurationSeconds = 10f;
    private float _remainTime;
    public AnimationCurve AlphaCurve;

    // Start is called before the first frame update
    void Start()
    {
        _remainTime = DurationSeconds;
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
