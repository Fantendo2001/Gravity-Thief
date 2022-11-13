using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearObject : MonoBehaviour
{
    public float StayTime = 3f;
    private float DestroyCount;
    private bool PlayerIsOnMe = false;
    private bool WillDestroy = false;
    private int i = 0;

    public player MyPlayer;

    // Start is called before the first frame update
    void Start()
    {
        DestroyCount = StayTime * 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerIsOnMe = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerIsOnMe = false;
        }
    }


    private void FixedUpdate()
    {
        if (PlayerIsOnMe == true)
        {
            WillDestroy = true;
        }

        if (WillDestroy == true)
        {
            if (i < DestroyCount)
            {
                i++;
            }
            else
            {
                if (PlayerIsOnMe == true)
                {
                    MyPlayer.transform.position = MyPlayer.initPos;
                    MyPlayer.delayCounter = 0.4f;
                    MyPlayer.JumpTimesRemain = 2;
                }
                    
                Destroy(gameObject);
            }
        }
    }
}
