using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpike : MonoBehaviour
{
    //��ֹ����
    public Vector3 des, orz;
    //��¼������ʼ��ľ���
    float x = 0;
    float y = 0;
    public  float speed = 5;
    public bool right ;
    public bool Y;
    void Start()
    {
        //��¼�ʼ��Z�����ϵ�λ��
        des = gameObject.transform.position;
        orz = transform.Find("Destination").position;
        x = transform.position.x;
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
       
        
        if (right==true&& Y==false)
        {
            float distance = Mathf.PingPong(Time.time * speed, Vector3.Distance(orz, des));
            transform.position = new Vector3(x + distance, transform.position.y, transform.position.z);
        }
        else if(right == false && Y == false)
        {
            float distance = Mathf.PingPong(Time.time * speed, Vector3.Distance(orz, des)) * -1;
            transform.position = new Vector3(x + distance, transform.position.y, transform.position.z);

        }
        else if (right == false && Y == true)
        {
            float distance = Mathf.PingPong(Time.time * speed, Vector3.Distance(orz, des)) * -1;
            transform.position = new Vector3(transform.position.x, y + distance, transform.position.z);

        }
        if (right == true && Y == true)
        {
            float distance = Mathf.PingPong(Time.time * speed, Vector3.Distance(orz, des));
            transform.position = new Vector3(transform.position.x, y + distance, transform.position.z);
        }


    }
}
