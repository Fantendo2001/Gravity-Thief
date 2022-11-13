using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerMove : MonoBehaviour
{
    public player MyPlayer;
    [SerializeField] private GameObject joyStick;

    //private Rigidbody2D rb;
    //private CapsuleCollider2D cc;
    //private Renderer rend;

    //public float dashSpeed = 10000f;
    //public float delayCounter = 0.4f;
    //public float startDashTime;
    private float delayCounterCopy;

    //private Vector2 direction;
    //bool dashOn;
    //Vector2 initPos;
    //private float dashTime;
    private Touch touch;
    private Vector3 begin, now, end, diff, tapPoint;
    private Vector3 mousePos;
    [SerializeField] private Joystick joystick;
    public bool pressedDash = false;
    public bool unpressedDash = false;
    private Button dashButton;

    //Double Jump////////////////////////////////////////////////////////////////////////////////////////////
    //public bool CanDoubleJump = false;/////////二段跳开关
    public int TimeBetweenJumps = 100;
    //private bool IsCountering = false;


    /////////////////////////////////////////////////长按冲刺时间////////////////////////////////////////////////
    //public bool CanLongDash = false;///////////蓄力冲开关
    private float StartTime;
    public float HoldTime;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool StartADash = false;
    
    /////////////////////////////////////////////////////摄像头//////////////////////////////////////////////////
    private Camera MC;
    private CinemachineBrain CB;
    private CinemachineVirtualCamera CVC;

    public bool CanMoveCamera = true; //////////////拉远摄像头开关


    

    private Vector2 MovingPos;
    public Vector2 RelativePos;

    private Vector2 NewMovingPos;
    private Vector2 FinalRelativePos;




    // Start is called before the first frame update
    void Start()
    {
        delayCounterCopy = MyPlayer.delayCounter;
        MC = Camera.main;
        CB = MC.GetComponent<CinemachineBrain>();
        CVC = CB.ActiveVirtualCamera as CinemachineVirtualCamera;
        joystick = GameObject.FindObjectOfType<Joystick>();
        dashButton = GameObject.FindObjectOfType<Button>();

        //CVC.m_Lens.OrthographicSize = MyPlayer.CameraSize;
    }

    public void pressed()
    {
        pressedDash = true;

        StartTime = 0.0f;
        HoldTime = 0.0f;

    }

    public void unpressed()
    {
        pressedDash = false;
        unpressedDash = true;

    }

    // Update is called once per frame

    private void PositionGoBack()
    {//跳跃到空中后闪回出发点
        MyPlayer.DoubleJumpCounter = 0;
        if (MyPlayer.IsOnGround == false && MyPlayer.JumpTimesRemain < 2)
        {
            transform.position = MyPlayer.initPos;
            MyPlayer.delayCounter = delayCounterCopy;
        }
        MyPlayer.JumpTimesRemain = 2;
        MyPlayer.IsOnGround = true;
        MyPlayer.JumpFromMovingObject = false;
    }

    private void Update()
    {
        //MyPlayer.anim.SetFloat("dashTime", MyPlayer.dashTime);
        MyPlayer.anim.SetBool("dashOn", MyPlayer.dashOn);
        #region dashArrow
        SpriteRenderer dashArrow;
        dashArrow = transform.Find("dashArrow").GetComponent<SpriteRenderer>();

        if (MyPlayer.dashTime == MyPlayer.startDashTime)
        {
            dashArrow.enabled = true;
        }
        else
        {
            dashArrow.enabled = false;
        }
        #endregion

        //if (MyPlayer.IsOnMovingObject == true)
        //{
        //    MyPlayer.rb.velocity = MyPlayer.MyMovingObject.GetComponent<Rigidbody2D>().velocity;
        //}

        //if (MyPlayer.JumpFromMovingObject == true)
        //{
        //    NewMovingPos = MyPlayer.MyMovingObject.transform.position;

        //    //FinalRelativePos = RelativePos;
        //    //RelativePos.Normalize();
        //    //FinalRelativePos -= RelativePos * 0.3f;

        //    MyPlayer.initPos = NewMovingPos + RelativePos;

        //    //Debug.Log(MyPlayer.initPos);
        //    //Debug.Log(MyPlayer.initPos);
        //}


#if UNITY_EDITOR || UNITY_STANDALONE
        //Debug.LogWarning(MyPlayer.JumpTimesRemain);
        //鼠标按键时间-蓄力冲刺
        if (Input.GetMouseButton(1))
        {

            if (MyPlayer.CanLongDash && MyPlayer.IsOnMovingObject == false && MyPlayer.IsCountering == false)
            {
                if (StartTime == 0.0f)
                {
                    StartTime = Time.time;
                }
                else
                {
                    HoldTime = Time.time - StartTime;
                }

                if (HoldTime > 0.5f && HoldTime < 1.5f)
                {
                    MyPlayer.dashSpeed = 100f * HoldTime;

                    if (CanMoveCamera)
                    {
                        CVC.m_Lens.OrthographicSize = MyPlayer.CameraSize * (1 + (HoldTime - 0.5f) * 0.6f);
                    }

                }
                else if (HoldTime > 1.5f)
                {
                    MyPlayer.dashSpeed = 150f;
                }
                else
                {
                    MyPlayer.dashSpeed = 50f;
                }
            }

            else
            {
                StartTime = 0.0f;
                HoldTime = 0.0f;
            }

        }

        if (Input.GetMouseButtonUp(1))
        {
            if (MyPlayer.myflowchart.GetBooleanVariable("IsInDialog") == false) 
            { 
                StartADash = true; 
            }
            
            StartTime = 0.0f;
            HoldTime = 0.0f;
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartTime = 0.0f;
            HoldTime = 0.0f;
        }



        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //if (Input.GetMouseButtonDown(1) && MyPlayer.dashOn == false)
        if (StartADash == true && MyPlayer.dashOn == false)//开始起跳
        {
            StartADash = false;
            //CVC.m_Lens.OrthographicSize = 8;
            GameObject.FindObjectOfType<PlayerAnimation>().playDash();
            //Debug.LogWarning("324");
                         
            if (MyPlayer.dashTime == MyPlayer.startDashTime && MyPlayer.JumpTimesRemain > 0)
            {
                if (MyPlayer.JumpTimesRemain == 2 && MyPlayer.IsOnGround == true)
                {
                    MyPlayer.initPos = transform.position;
                    //Debug.LogError(MyPlayer.initPos);
                }

                if (MyPlayer.IsOnMovingObject == true)/////////////////////////////////////////////////////////////////新增
                {
                    gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    MyPlayer.JumpFromMovingObject = true;
                    MovingPos = MyPlayer.MyMovingObject.transform.position;
                    RelativePos = MyPlayer.initPos - MovingPos;
                }

                if (MyPlayer.IsOnRotateObject == true)
                {
                    MyPlayer.JumpFromRotateObject = true;
                }
                         
                MyPlayer.direction = mousePos - transform.position;
                //Debug.Log("mousePos"+mousePos);
                //Debug.Log("transPos"+transform.position);
                MyPlayer.direction.Normalize();
                //Debug.Log("direction"+direction);
                MyPlayer.rb.velocity = MyPlayer.direction * MyPlayer.dashSpeed;
                MyPlayer.dashSpeed = 50f;
                         
                MyPlayer.dashOn = true;
                MyPlayer.IsOnGround = false;
                MyPlayer.IsOnMovingObject = false;
                MyPlayer.IsOnRotateObject = false;
                MyPlayer.IsOnSpike = false;

                //Debug.Log("speed"+rb.velocity);
            }
        }
        else if (MyPlayer.dashOn == true)//处于跳跃中
        //dashTime!=startDashTime, which means "during a dash"
        {

            if (MyPlayer.dashTime <= 0 | MyPlayer.IsOnSpike == true)
            {
                //Debug.LogError(MyPlayer.IsOnGround);

                MyPlayer.rb.velocity = Vector2.zero;
                MyPlayer.delayCounter -= Time.deltaTime;
                //MyPlayer.rend.enabled = false;
                if (MyPlayer.delayCounter <= 0)//没能在冲刺结束前碰到墙壁等
                {
                    MyPlayer.rend.enabled = true;

                    MyPlayer.dashOn = false;
                    MyPlayer.dashTime = MyPlayer.startDashTime;
                         
                    if (MyPlayer.CanDoubleJump && MyPlayer.IsOnSpike == false)
                    {
                        MyPlayer.JumpTimesRemain--;
                        MyPlayer.IsCountering = true;                        
                    }
                         
                    else
                    {
                        transform.position = MyPlayer.initPos;
                        MyPlayer.delayCounter = delayCounterCopy;
                        MyPlayer.JumpTimesRemain = 2;
                        MyPlayer.IsOnGround = true;
                        MyPlayer.JumpFromMovingObject = false;

                        MyPlayer.IsOnSpike = false;

                    }

                }
            }
            else //dashTime>0 & dashTime<startDashTime
            {
                MyPlayer.dashTime -= Time.deltaTime;
                         
                /*Debug.Log(dashTime);*/
            }
        }









#elif UNITY_ANDROID || UNITY_IOS
        if (pressedDash)
        {

            if (MyPlayer.CanLongDash && MyPlayer.IsOnMovingObject == false && MyPlayer.IsCountering == false)
            {
                if (StartTime == 0.0f)
                {
                    StartTime = Time.time;
                }
                else
                {
                    HoldTime = Time.time - StartTime;
                }

                if (HoldTime > 0.5f && HoldTime < 1.5f)
                {
                    MyPlayer.dashSpeed = 100f * HoldTime;

                    if (CanMoveCamera)
                    {
                        CVC.m_Lens.OrthographicSize = MyPlayer.CameraSize * (1 + (HoldTime - 0.5f) * 0.6f);
                    }

                }
                else if (HoldTime > 1.5f)
                {
                    MyPlayer.dashSpeed = 150f;
                }
                else
                {
                    MyPlayer.dashSpeed = 50f;
                }
            }

            else
            {
                StartTime = 0.0f;
                HoldTime = 0.0f;
            }

        }

        if (unpressedDash)
        {
            if (MyPlayer.myflowchart.GetBooleanVariable("IsInDialog") == false) 
            { 
                StartADash = true; 
            }
            pressedDash = false;
            StartTime = 0.0f;
            HoldTime = 0.0f;
            unpressedDash = false;
        }


        //if (Input.GetMouseButtonDown(1) && MyPlayer.dashOn == false)
        if (StartADash == true && MyPlayer.dashOn == false)//开始起跳
        {
            StartADash = false;
            //CVC.m_Lens.OrthographicSize = 8;
            GameObject.FindObjectOfType<PlayerAnimation>().playDash();
            //Debug.LogWarning("324");
                         
            if (MyPlayer.dashTime == MyPlayer.startDashTime && MyPlayer.JumpTimesRemain > 0)
            {
                if (MyPlayer.JumpTimesRemain == 2 && MyPlayer.IsOnGround == true)
                {
                    MyPlayer.initPos = transform.position;
                    //Debug.LogError(MyPlayer.initPos);
                }

                if (MyPlayer.IsOnMovingObject == true)/////////////////////////////////////////////////////////////////新增
                {
                    gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    MyPlayer.JumpFromMovingObject = true;
                    MovingPos = MyPlayer.MyMovingObject.transform.position;
                    RelativePos = MyPlayer.initPos - MovingPos;
                }

                if (MyPlayer.IsOnRotateObject == true)
                {
                    MyPlayer.JumpFromRotateObject = true;
                }
                         
                MyPlayer.direction = joystick.Direction;
                MyPlayer.direction.Normalize();

                MyPlayer.rb.velocity = MyPlayer.direction * MyPlayer.dashSpeed;
                MyPlayer.dashSpeed = 50f;
                         
                MyPlayer.dashOn = true;
                MyPlayer.IsOnGround = false;
                MyPlayer.IsOnMovingObject = false;
                MyPlayer.IsOnRotateObject = false;
                MyPlayer.IsOnSpike = false;

                //Debug.Log("speed"+rb.velocity);
            }
        }
        else if (MyPlayer.dashOn == true)//处于跳跃中
        //dashTime!=startDashTime, which means "during a dash"
        {

            if (MyPlayer.dashTime <= 0 | MyPlayer.IsOnSpike == true)
            {
                //Debug.LogError(MyPlayer.IsOnGround);

                MyPlayer.rb.velocity = Vector2.zero;
                MyPlayer.delayCounter -= Time.deltaTime;
                //MyPlayer.rend.enabled = false;
                if (MyPlayer.delayCounter <= 0)//没能在冲刺结束前碰到墙壁等
                {
                    MyPlayer.rend.enabled = true;

                    MyPlayer.dashOn = false;
                    MyPlayer.dashTime = MyPlayer.startDashTime;
                         
                    if (MyPlayer.CanDoubleJump && MyPlayer.IsOnSpike == false)
                    {
                        MyPlayer.JumpTimesRemain--;
                        MyPlayer.IsCountering = true;                        
                    }
                         
                    else
                    {
                        transform.position = MyPlayer.initPos;
                        MyPlayer.delayCounter = delayCounterCopy;
                        MyPlayer.JumpTimesRemain = 2;
                        MyPlayer.IsOnGround = true;
                        MyPlayer.JumpFromMovingObject = false;

                        MyPlayer.IsOnSpike = false;

                    }

                }
            }
            else //dashTime>0 & dashTime<startDashTime
            {
                MyPlayer.dashTime -= Time.deltaTime;
                         
                /*Debug.Log(dashTime);*/
            }
        }
        unpressedDash = false;


//touch mechanics
//        if (pressedDash == true)
//         {
//             if (MyPlayer.CanLongDash)
//             {
//                 if (StartTime == 0.0f)
//                 {
//                     StartTime = Time.time;
//                 }
//                 else
//                 {
//                     HoldTime = Time.time - StartTime;
//                 }
// 
//                 if (HoldTime > 0.5f && HoldTime < 1.5f)
//                 {
//                     MyPlayer.dashSpeed = 100f * HoldTime;
// 
//                     if (CanMoveCamera)
//                     {
//                         CVC.m_Lens.OrthographicSize = MyPlayer.CameraSize * (1 + (HoldTime - 0.5f) * 0.6f);
//                     }
// 
//                 }
//                 else if (HoldTime > 1.5f)
//                 {
//                     MyPlayer.dashSpeed = 150f;
//                 }
//                 else
//                 {
//                     MyPlayer.dashSpeed = 50f;
//                 }
//             }
// 
//             else
//             {
//                 StartTime = 0.0f;
//                 HoldTime = 0.0f;
//             }
//             Debug.Log("part1");
// 
//         }
// 
//         if (unpressedDash == true)
//         {
//             StartADash = true;
//             pressedDash = false;
//             StartTime = 0.0f;
//             HoldTime = 0.0f;
//             Debug.Log("if pressed");
//             unpressedDash = false;
//         }
// 
// 
//         //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//         //if (Input.GetMouseButtonDown(1) && MyPlayer.dashOn == false)
//         if (StartADash == true && MyPlayer.dashOn == false)
//         {
//             StartADash = false;
//             //CVC.m_Lens.OrthographicSize = 8;
// 
//             if (MyPlayer.dashTime == MyPlayer.startDashTime && MyPlayer.JumpTimesRemain > 0)
//             {
//                 if (MyPlayer.JumpTimesRemain == 2)
//                 {
//                     MyPlayer.initPos = transform.position;
//                 }
// 
//                 MyPlayer.direction = joystick.Direction;
//                 //Debug.Log("mousePos"+mousePos);
//                 //Debug.Log("transPos"+transform.position);
//                 MyPlayer.direction.Normalize();
//                 //Debug.Log("direction"+direction);
//                 MyPlayer.rb.velocity = MyPlayer.direction * MyPlayer.dashSpeed;
//                 MyPlayer.dashSpeed = 50f;
// 
//                 MyPlayer.dashOn = true;
//                 MyPlayer.IsOnGround = false;
// 
//                 //Debug.Log("speed"+rb.velocity);
//             }
//         }
//         else if (MyPlayer.dashOn == true)
//         //dashTime!=startDashTime, which means "during a dash"
//         {
//             if (MyPlayer.dashTime <= 0)
//             {
//                 MyPlayer.rb.velocity = Vector2.zero;
//                 MyPlayer.delayCounter -= Time.deltaTime;
//                 //MyPlayer.rend.enabled = false;
//                 if (MyPlayer.delayCounter <= 0)//没能在冲刺结束前碰到墙壁等
//                 {
//                     MyPlayer.rend.enabled = true;
// 
//                     MyPlayer.dashOn = false;
//                     MyPlayer.dashTime = MyPlayer.startDashTime;
// 
//                     if (MyPlayer.CanDoubleJump)
//                     {
//                         MyPlayer.JumpTimesRemain--;
//                         MyPlayer.IsCountering = true;
// 
//                         //Invoke("PositionGoBack", TimeBetweenJumps);//延迟一定时间后再回到之前保存的位置
//                     }
// 
//                     else
//                     {
//                         transform.position = MyPlayer.initPos;
//                         MyPlayer.delayCounter = delayCounterCopy;
//                         MyPlayer.JumpTimesRemain = 2;
//                     }
// 
//                 }
//             }
//             else //dashTime>0 & dashTime<startDashTime
//             {
//                 MyPlayer.dashTime -= Time.deltaTime;
// 
//                 /*Debug.Log(dashTime);*/
//             }
//         }
//         unpressedDash = false;
       


#endif

    }

    private void FixedUpdate()
    {
        if (MyPlayer.IsCountering == true)//这个用来计时二段跳之间的间隔，请勿移入Update中
        {
            if (MyPlayer.DoubleJumpCounter < TimeBetweenJumps)
            {
                MyPlayer.DoubleJumpCounter++;
                //Debug.LogError(MyPlayer.DoubleJumpCounter);
            }
            else
            {
                MyPlayer.DoubleJumpCounter = 0;
                MyPlayer.IsCountering = false;
                PositionGoBack();
            }
        }
    }



}
