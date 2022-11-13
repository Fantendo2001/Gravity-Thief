using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashObject : MonoBehaviour
{
    private Animator anim;
    public int WaitTime = 200;
    public int FlashTimeShow = 200;
    public int FlashTimeHide = 200;
    private int StartWaiting = 0;
    private int i = 0;
    private int j = 0;
    private bool IsShowing = true;
    private bool PlayerIsOnMe = false;

    public player MyPlayer;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
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
        //Debug.LogError(MyPlayer.initPos);
        


        if (StartWaiting < WaitTime)
        {
            StartWaiting++;
        }

        else
        {
            if (IsShowing == true)
            {
                if (i < FlashTimeShow)
                {
                    if (i > (FlashTimeShow-50))
                    {
                    anim.SetBool("Disappear",true);
                    }

                    i++;
                }
                else
                {
                    //Debug.LogWarning(PlayerIsOnMe);

                    if (PlayerIsOnMe)
                    {
                        MyPlayer.transform.position = MyPlayer.initPos;
                        MyPlayer.delayCounter = 0.4f;
                        MyPlayer.JumpTimesRemain = 2;
                    }


                    i = 0;
                    //gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    IsShowing = false;
                    anim.SetBool("Disappear",false);
                }
            }
            else
            {
                if (j < FlashTimeHide)
                {
                    if (j > (FlashTimeHide-50))
                    {
                    anim.SetBool("Appear",true);
                    }

                    j++;
                }
                else
                {
                    j = 0;
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    gameObject.GetComponent<CircleCollider2D>().enabled = true;
                    IsShowing = true;
                    anim.SetBool("Appear",false);
                }
            }
        }
    }
}
