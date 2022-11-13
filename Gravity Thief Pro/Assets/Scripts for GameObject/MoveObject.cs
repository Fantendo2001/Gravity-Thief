using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private Rigidbody2D RB;
    
    private Vector3 StartingPoint;
    private Vector3 DestinationPoint;
    private Vector2 DistanceVector;
    private Vector2 DirectionVector;
    private float Distance;
    private float MovingTime;
    private float MovingUpdates;

    private bool StartToDest = true;
    int i = 0;
    int j = 0;

    public float MoveSpeed = 1f;

    public player MyPlayer;
    public PlayerMove MyPlayerMove;

    private bool PlayerIsOnMe = false;

    private Vector2 MovingPos;
    public Vector2 RelativePos;

    private Vector2 MyInitPos;
    private Vector2 RelativeInitPos;

    private Vector2 MyNewPos;
    private Vector2 FinalRelaPos;

    private Vector2 NextPos;




    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        StartingPoint = transform.position;
        DestinationPoint = transform.Find("Destination").position;

        DistanceVector = DestinationPoint - StartingPoint;
        DirectionVector = DistanceVector;
        DirectionVector.Normalize();

        Distance = (DistanceVector.x + DistanceVector.y) / (DirectionVector.x + DirectionVector.y);
        MovingTime = Distance / MoveSpeed;
        MovingUpdates = MovingTime * 100;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)//Player进入方块周围
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.LogError("Enter!!!!");
            PlayerIsOnMe = true;
            //MyPlayer.JumpFromMovingObject = false;

            //RelativeInitPos = MyPlayer.transform.position - transform.position;
            //FinalRelaPos = RelativeInitPos;
            //RelativeInitPos.Normalize();
            //FinalRelaPos -= RelativeInitPos * 0.3f;
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
        MyNewPos = transform.position;

        if (StartToDest == true)
        {
            if (i < MovingUpdates)
            {
                RB.velocity = DirectionVector * MoveSpeed;
                i++;
                NextPos = MyNewPos + RB.velocity * 0.03f;
            }
            else
            {
                StartToDest = false;
                i = 0;
                NextPos = MyNewPos;
            }
        }
        else
        {
            if (j < MovingUpdates)
            {
                RB.velocity = DirectionVector * MoveSpeed * -1;
                j++;
                NextPos = MyNewPos + RB.velocity * 0.03f;
            }
            else
            {
                StartToDest = true;
                j = 0;
                NextPos = MyNewPos;
            }
        }

        if (PlayerIsOnMe == true)//当Player在方块附近时
        {
            MyPlayer.rb.velocity = RB.velocity;
        }


        if (MyPlayer.JumpFromMovingObject == true && gameObject == MyPlayer.MyMovingObject)
        {

            FinalRelaPos = MyPlayerMove.RelativePos;
            MyPlayer.initPos = NextPos + FinalRelaPos;
        }
    }
}
