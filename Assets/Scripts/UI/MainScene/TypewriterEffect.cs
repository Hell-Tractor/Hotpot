using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{

    public float charsPerSecond = 0.2f;//����ʱ����
    private string words;//������Ҫ��ʾ������
    public AudioSource se;
    private bool isActive = false;
    private float timer;//��ʱ��
    private Text myText;
    private int currentPos = 0;//��ǰ����λ��

    // Use this for initialization
    void Start()
    {
        
        timer = 0;
        isActive = true;
        //charsPerSecond = Mathf.Max(0.2f, charsPerSecond);
        myText = GetComponent<Text>();
        words = myText.text;
        myText.text = "";//��ȡText���ı���Ϣ�����浽words�У�Ȼ��̬�����ı���ʾ���ݣ�ʵ�ִ��ֻ���Ч��
        se.Play();
    }

    // Update is called once per frame
    void Update()
    {
        OnStartWriter();
    }
    void OnStartWriter()
    {

        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= charsPerSecond)
            {//�жϼ�ʱ��ʱ���Ƿ񵽴�
                timer = 0;
                currentPos++;
                myText.text = words.Substring(0, currentPos);//ˢ���ı���ʾ����
               

                if (currentPos >= words.Length)
                {
                    OnFinish();
                }
            }

        }
    }
    void OnFinish()
    {
        isActive = false;
        timer = 0;
        currentPos = 0;
        myText.text = words;
    }




}
