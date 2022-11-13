using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private Rigidbody2D RigidBody;
    
    public float RotateTime = 2.0f;
    private float RotateCount;
    private int i;

    private bool PlayerIsOnMe = false;
    public player MyPlayer;


    // Start is called before the first frame update
    void Start()
    {
        RigidBody = gameObject.GetComponent<Rigidbody2D>();
        RotateCount = 100 * RotateTime;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)//Player进入方块周围
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerIsOnMe = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)//Player离开方块周围
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerIsOnMe = false;

        }
    }

    void FixedUpdate()
    {
        if (MyPlayer.JumpFromRotateObject == false)
        {
            if (i < RotateCount)
            {
                i++;
            }
            else
            {
                RigidBody.rotation += 90;
                i = 0;

                if (PlayerIsOnMe == true)
                {
                    MyPlayer.transform.RotateAround(transform.position, transform.forward, 90);
                    MyPlayer.transform.Rotate(0, 0, -90);
                }
            }
        }

    }
}
