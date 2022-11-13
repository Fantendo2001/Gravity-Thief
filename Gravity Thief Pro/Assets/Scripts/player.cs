using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Cinemachine;
using UnityEngine.SceneManagement;


public class player : MonoBehaviour
{
    //private Rigidbody2D rb;
    //private CapsuleCollider2D cc;
    //private Renderer rend;

    public Rigidbody2D rb;
    public CapsuleCollider2D cc;
    public Renderer rend;
    public Animator anim;


    [SerializeField] private GameObject dashEffect;
    [SerializeField] private GameObject bloodEffect;


    public float dashSpeed = 10000f;
    public float delayCounter = 0.4f;
    public float startDashTime;
    private float delayCounterCopy;

    //private Vector2 direction;
    //bool dashOn;
    //Vector2 initPos;

    public Vector2 direction;
    public bool dashOn;
    public Vector2 initPos;
    public Vector3 contactPoint;
    ContactPoint2D[] contacts = new ContactPoint2D[2];


    //private float dashTime;
    public float dashTime;

    private Vector3 mousePos;
    private Touch touch;
    private Vector3 begin, end, diff, tapPoint;

    ////Double Jump////////////////////////////////////////////////////////////////////////////////////////////
    //public bool CanDoubleJump = true;
    //private int JumpTimesRemain = 2;
    //private float TimeBetweenJumps = 0.5f;

    //////////////////////////////////////////////////////用于二段跳和特殊平台（playermove里面调用）/////////////////////////////////////////////////////
    public bool IsOnGround = false;
    public bool IsOnMovingObject = false;
    public bool IsOnRotateObject = false;
    public bool JumpFromMovingObject = false;
    public bool JumpFromRotateObject = false;

    public int DoubleJumpCounter = 0;
    public bool IsCountering = false;
    public int JumpTimesRemain = 2;

    public bool CanAttack = false;//////////////////////////////////////////////////攻击能力开关
    public bool CanDoubleJump = false;//////////////////////////////////////////////二段跳开关
    public bool CanLongDash = false;////////////////////////////////////////////////蓄力冲开关

    public GameObject MyMovingObject;

    //UI Health bar//////////////////////////////////////////////////////////////////////////////////////////
    public float MaxHP = 100.0f;
    public float CurrentHP;
    
    //////////////////////////////////////////摄像头//////////////////
    private Camera MC;
    private CinemachineBrain CB;
    private CinemachineVirtualCamera CVC;
    public int CameraSize = 10;

    public bool IsOnSpike;

    public Fungus.Flowchart myflowchart;

    ///////////////////////////////////////////////////////特殊收集物相关////////////////////////////////////////////////////////////////////////////
    //第二关
    private bool HaveTaoMuJian = false;

    //第四关
    private bool HaveBigTorch = false;
    private int Task4Count = 0;
    public bool Task4Finished = false;
    private bool T4Flag = true;//(不用存档，这个是局部为了防止碰一下烽火台触发多次OnTriggerEnter)

    //第五关
    private bool Task5Start = false;
    private int Task5Count = 0;
    public bool Task5Finished = false;
    private bool T5Flag = true;//(不用存档，这个是局部为了防止碰一下瑞兽触发多次OnTriggerEnter)

    //第六关
    private bool Task6Start = false;
    public bool Task6Finished = false;

    //第七关

    //第八关
    public bool HaveFoZhu = false;
    /// /////////////////////////////12.15新增
    public bool Task2Finished = false;
    public bool Task7Finished = false;
    public bool Task8Finished = false;

    public bool FinishAllLevels = false;////////////////////////这个啥时候修改为true还没写

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void SaveAbove()
    {
        //public bool CanAttack = false;//////////////////////////////////////////////////攻击能力开关
        //public bool CanDoubleJump = false;//////////////////////////////////////////////二段跳开关
        //public bool CanLongDash = false;////////////////////////////////////////////////蓄力冲开关
        if (CanAttack)
        {
            PlayerPrefs.SetString("CanAttack", "true");
        }
        else
        {
            PlayerPrefs.SetString("CanAttack", "false");
        }
        if (CanDoubleJump)
        {
            PlayerPrefs.SetString("CanDoubleJump", "true");
        }
        else
        {
            PlayerPrefs.SetString("CanDoubleJump", "false");
        }
        if (CanLongDash)
        {
            PlayerPrefs.SetString("CanLongDash", "true");
        }
        else
        {
            PlayerPrefs.SetString("CanLongDash", "false");
        }

        if (FinishAllLevels)
        {
            PlayerPrefs.SetString("FinishAllLevels", "true");
        }
        else
        {
            PlayerPrefs.SetString("FinishAllLevels", "false");
        }

        //第二关
        //private bool HaveTaoMuJian = false;
        if (!HaveTaoMuJian)
        {
            PlayerPrefs.SetString("HaveTaoMuJian", "false");
        }
        else
        {
            PlayerPrefs.SetString("HaveTaoMuJian", "true");
        }

        //第四关
        //private bool HaveBigTorch = false;
        if (!HaveBigTorch)
        {
            PlayerPrefs.SetString("HaveBigTorch", "false");
        }
        else
        {
            PlayerPrefs.SetString("HaveBigTorch", "true");
        }
        //private int Task4Count = 0;
        //PlayerPrefs.SetInt("Task4Count", Task4Count);
        
        //private bool Task4Finished = false;
        if (!Task4Finished)
        {
            PlayerPrefs.SetString("Task4Finished", "false");
        }
        else
        {
            PlayerPrefs.SetString("Task4Finished", "true");
        }

        //第五关
        //private bool Task5Start = false;
        if (!Task5Start)
        {
            PlayerPrefs.SetString("Task5Start", "false");
        }
        else
        {
            PlayerPrefs.SetString("Task5Start", "true");
        }
        //private int Task5Count = 0;
        //PlayerPrefs.SetInt("Task5Count", Task5Count);

        //private bool Task5Finished = false;
        if (!Task5Finished)
        {
            PlayerPrefs.SetString("Task5Finished", "false");
        }
        else
        {
            PlayerPrefs.SetString("Task5Finished", "true");
        }

        //第六关
        //private bool Task6Start = false;
        if (!Task6Start)
        {
            PlayerPrefs.SetString("Task6Start", "false");
        }
        else
        {
            PlayerPrefs.SetString("Task6Start", "true");
        }

        //private bool Task6Finished = false;
        if (!Task6Finished)
        {
            PlayerPrefs.SetString("Task6Finished", "false");
        }
        else
        {
            PlayerPrefs.SetString("Task6Finished", "true");
        }

        //第八关
        //public bool HaveFoZhu = false;
        if (!HaveFoZhu)
        {
            PlayerPrefs.SetString("HaveFoZhu", "false");
        }
        else
        {
            PlayerPrefs.SetString("HaveFoZhu", "true");
        }
    }

    public void setFlow()
    {
        if (myflowchart.GetBooleanVariable("NCGameStart_First")) PlayerPrefs.SetString("NCGameStart_First", "true");
        else PlayerPrefs.SetString("NCGameStart_First", "false");

        if (myflowchart.GetBooleanVariable("NC1_First")) PlayerPrefs.SetString("NC1_First", "true");
        else PlayerPrefs.SetString("NC1_First", "false");

        if (myflowchart.GetBooleanVariable("NCStart2_First")) PlayerPrefs.SetString("NCStart2_First", "true");
        else PlayerPrefs.SetString("NCStart2_First", "false");

        if (myflowchart.GetBooleanVariable("NCLYF2_First")) PlayerPrefs.SetString("NCLYF2_First", "true");
        else PlayerPrefs.SetString("NCLYF2_First", "false");

        if (myflowchart.GetBooleanVariable("NCEnemy2_First")) PlayerPrefs.SetString("NCEnemy2_First", "true");
        else PlayerPrefs.SetString("NCEnemy2_First", "false");

        if (myflowchart.GetBooleanVariable("NCStart3_First")) PlayerPrefs.SetString("NCStart3_First", "true");
        else PlayerPrefs.SetString("NCStart3_First", "false");

        if (myflowchart.GetBooleanVariable("NC3GetAttack_First")) PlayerPrefs.SetString("NC3GetAttack_First", "true");
        else PlayerPrefs.SetString("NC3GetAttack_First", "false");

        if (myflowchart.GetBooleanVariable("NC3Terminal_First")) PlayerPrefs.SetString("NC3Terminal_First", "true");
        else PlayerPrefs.SetString("NC3Terminal_First", "false");

        if (myflowchart.GetBooleanVariable("HaveTaoMuJian")) PlayerPrefs.SetString("HaveTaoMuJian", "true");
        else PlayerPrefs.SetString("HaveTaoMuJian", "false");

        if (myflowchart.GetBooleanVariable("FirstGetTorch")) PlayerPrefs.SetString("FirstGetTorch", "true");
        else PlayerPrefs.SetString("FirstGetTorch", "false");

        if (myflowchart.GetBooleanVariable("NC4Start_First")) PlayerPrefs.SetString("NC4Start_First", "true");
        else PlayerPrefs.SetString("NC4Start_First", "false");

        if (myflowchart.GetBooleanVariable("NC4GetTorch_First")) PlayerPrefs.SetString("NC4GetTorch_First", "true");
        else PlayerPrefs.SetString("NC4GetTorch_First", "false");

        if (myflowchart.GetBooleanVariable("NC4Finish_First")) PlayerPrefs.SetString("NC4Finish_First", "true");
        else PlayerPrefs.SetString("NC4Finish_First", "false");

        if (myflowchart.GetBooleanVariable("NC5Start_First")) PlayerPrefs.SetString("NC5Start_First", "true");
        else PlayerPrefs.SetString("NC5Start_First", "false");

        if (myflowchart.GetBooleanVariable("NC5TaiHeDian_First")) PlayerPrefs.SetString("NC5TaiHeDian_First", "true");
        else PlayerPrefs.SetString("NC5TaiHeDian_First", "false");

        if (myflowchart.GetBooleanVariable("NC5GetLongDash_First")) PlayerPrefs.SetString("NC5GetLongDash_First", "true");
        else PlayerPrefs.SetString("NC5GetLongDash_First", "false");

        if (myflowchart.GetBooleanVariable("NC5Finish_First")) PlayerPrefs.SetString("NC5Finish_First", "true");
        else PlayerPrefs.SetString("NC5Finish_First", "false");

        if (myflowchart.GetBooleanVariable("NC6Start_First")) PlayerPrefs.SetString("NC6Start_First", "true");
        else PlayerPrefs.SetString("NC6Start_First", "false");

        if (myflowchart.GetBooleanVariable("NC6BDLG_First")) PlayerPrefs.SetString("NC6BDLG_First", "true");
        else PlayerPrefs.SetString("NC6BDLG_First", "false");

        if (myflowchart.GetBooleanVariable("NC6Finish_First")) PlayerPrefs.SetString("NC6Finish_First", "true");
        else PlayerPrefs.SetString("NC6Finish_First", "false");

        if (myflowchart.GetBooleanVariable("NC7Start_First")) PlayerPrefs.SetString("NC7Start_First", "true");
        else PlayerPrefs.SetString("NC7Start_First", "false");

        if (myflowchart.GetBooleanVariable("NC7GetDJ_First")) PlayerPrefs.SetString("NC7GetDJ_First", "true");
        else PlayerPrefs.SetString("NC7GetDJ_First", "false");

        if (myflowchart.GetBooleanVariable("GetJYZ_First")) PlayerPrefs.SetString("GetJYZ_First", "true");
        else PlayerPrefs.SetString("GetJYZ_First", "false");

        if (myflowchart.GetBooleanVariable("NC8Start_First")) PlayerPrefs.SetString("NC8Start_First", "true");
        else PlayerPrefs.SetString("NC8Start_First", "false");

        if (myflowchart.GetBooleanVariable("NC8TowerSkin_First")) PlayerPrefs.SetString("NC8TowerSkin_First", "true");
        else PlayerPrefs.SetString("NC8TowerSkin_First", "false");

        if (myflowchart.GetBooleanVariable("NC8TowerTop_First")) PlayerPrefs.SetString("NC8TowerTop_First", "true");
        else PlayerPrefs.SetString("NC8TowerTop_First", "false");

        if (myflowchart.GetBooleanVariable("NC8EnterDiGong_First")) PlayerPrefs.SetString("GetJYZ_FiNC8EnterDiGong_Firstrst", "true");
        else PlayerPrefs.SetString("NC8EnterDiGong_First", "false");

        if (myflowchart.GetBooleanVariable("NC8ExitDiGong_First")) PlayerPrefs.SetString("NC8ExitDiGong_First", "true");
        else PlayerPrefs.SetString("NC8ExitDiGong_First", "false");

        if (myflowchart.GetBooleanVariable("FinishAllLevels")) PlayerPrefs.SetString("FinishAllLevels", "true");
        else PlayerPrefs.SetString("FinishAllLevels", "false");

        if (myflowchart.GetBooleanVariable("Ending")) PlayerPrefs.SetString("Ending", "true");
        else PlayerPrefs.SetString("Ending", "false");

        if (myflowchart.GetBooleanVariable("EndingStart")) PlayerPrefs.SetString("EndingStart", "true");
        else PlayerPrefs.SetString("EndingStart", "false");
    }

    void readFlow()
    {
        String[] strings =
        {
            "NCGameStart_First", "NC1_First", "NCStart2_First", "NCLYF2_First", "NCEnemy2_First", "NCStart3_First",
            "NC3GetAttack_First", "NC3Terminal_First","HaveTaoMuJian","FirstGetTorch","NC4Start_First","NC4GetTorch_First",
            "NC4Finish_First","NC5Start_First","NC5TaiHeDian_First", "NC5GetLongDash_First", "NC5Finish_First","NC6Start_First",
            "NC6BDLG_First","NC6Finish_First","NC7Start_First","NC7GetDJ_First","GetJYZ_First","NC8Start_First","NC8TowerSkin_First",
            "NC8TowerTop_First","NC8EnterDiGong_First","NC8ExitDiGong_First","FinishAllLevels","Ending","EndingStart"
        };


        foreach (String flow in strings)
        {
            if (PlayerPrefs.GetString(flow) == "true")
            {
                myflowchart.SetBooleanVariable(flow, true);
            } 
            else
            {
                myflowchart.SetBooleanVariable(flow, false);
            }
        }
    }


    void readSave()
    {
//     private bool Task2Finished = false;
//     private bool Task7Finished = false;
//     private bool Task8Finished = false;


        //public bool CanAttack = false;//////////////////////////////////////////////////攻击能力开关
        //public bool CanDoubleJump = false;//////////////////////////////////////////////二段跳开关
        //public bool CanLongDash = false;////////////////////////////////////////////////蓄力冲开关
        if (PlayerPrefs.GetString("CanAttack") == "false")
        {
            CanAttack = false;
        }
        else
        {
            CanAttack = true;
        }
        if (PlayerPrefs.GetString("CanDoubleJump") == "false")
        {
            CanDoubleJump = false;
        }
        else
        {
            CanDoubleJump = true;
        }
        if (PlayerPrefs.GetString("CanLongDash") == "false")
        {
            CanLongDash = false;
        }
        else
        {
            CanLongDash = true;
        }

        if (PlayerPrefs.GetString("FinishAllLevels") == "true")
        {
            FinishAllLevels = true;
        }
        else
        {
            FinishAllLevels = false;
        }

        //第二关
        //private bool HaveTaoMuJian = false;
        if (PlayerPrefs.GetString("HaveTaoMuJian") == "false")
        {
            HaveTaoMuJian = false;
        }
        else
        {
            HaveTaoMuJian = true;
        }

        if (PlayerPrefs.GetString("Task2Finished") == "false")
        {
            Task2Finished = false;
        }
        else
        {
            Task2Finished = true;
        }

        //第四关
        //private bool HaveBigTorch = false;
        if (PlayerPrefs.GetString("HaveTaoMuJian") == "false")
        {
            HaveBigTorch = false;
        }
        else
        {
            HaveBigTorch = true;
        }
        //private int Task4Count = 0;
        //Task4Count = PlayerPrefs.GetInt("Task4Count");

        //private bool Task4Finished = false;
        if (PlayerPrefs.GetString("Task4Finished") == "false")
        {
            Task4Finished = false;
        }
        else
        {
            Task4Finished = true;
        }

        //第五关
        //private bool Task5Start = false;
        if (PlayerPrefs.GetString("Task5Start") == "false")
        {
            Task5Start = false;
        }
        else
        {
            Task5Start = true;
        }
        //private int Task5Count = 0;
        //Task5Count = PlayerPrefs.GetInt("Task5Count");

        //private bool Task5Finished = false;
        if (PlayerPrefs.GetString("Task5Finished") == "false")
        {
            Task5Finished = false;
        }
        else
        {
            Task5Finished = true;
        }

        //第六关
        //private bool Task6Start = false;
        if (PlayerPrefs.GetString("Task6Start") == "false")
        {
            Task6Start = false;
        }
        else
        {
            Task6Start = true;
        }

        //private bool Task6Finished = false;
        if (!Task6Finished)
        {
            PlayerPrefs.SetString("Task6Finished", "false");
        }
        else
        {
            PlayerPrefs.SetString("Task6Finished", "true");
        }

        if (PlayerPrefs.GetString("Task7Finished") == "false")
        {
            Task7Finished = false;
        }
        else
        {
            Task7Finished = true;
        }

        //第八关
        //public bool HaveFoZhu = false;
        if (!HaveFoZhu)
        {
            PlayerPrefs.SetString("HaveFoZhu", "false");
        }
        else
        {
            PlayerPrefs.SetString("HaveFoZhu", "true");
        }

        if (PlayerPrefs.GetString("Task8Finished") == "false")
        {
            Task8Finished = false;
        }
        else
        {
            Task8Finished = true;
        }
    }

    void initAbove()
    {
        //public bool CanAttack = false;//////////////////////////////////////////////////攻击能力开关
        //public bool CanDoubleJump = false;//////////////////////////////////////////////二段跳开关
        //public bool CanLongDash = false;////////////////////////////////////////////////蓄力冲开关
        if (!PlayerPrefs.HasKey("CanAttack"))
        {
            PlayerPrefs.SetString("CanAttack", "false");
        }
        if (!PlayerPrefs.HasKey("CanDoubleJump"))
        {
            PlayerPrefs.SetString("CanDoubleJump", "false");
        }
        if (!PlayerPrefs.HasKey("CanLongDash"))
        {
            PlayerPrefs.SetString("CanLongDash", "false");
        }


        //第二关
        //private bool HaveTaoMuJian = false;
        if (!PlayerPrefs.HasKey("HaveTaoMuJian"))
        {
            PlayerPrefs.SetString("HaveTaoMuJian", "false");
        }

        //第四关
        //private bool HaveBigTorch = false;
        if (!PlayerPrefs.HasKey("HaveBigTorch"))
        {
            PlayerPrefs.SetString("HaveBigTorch", "false");
        }
        //private int Task4Count = 0;
        if (!PlayerPrefs.HasKey("Task4Count"))
        {
            PlayerPrefs.SetInt("Task4Count", 0);
        }
        //private bool Task4Finished = false;
        if (!PlayerPrefs.HasKey("Task4Finished"))
        {
            PlayerPrefs.SetString("Task4Finished", "false");
        }

        if (!PlayerPrefs.HasKey("Task2Finished"))
        {
            PlayerPrefs.SetString("Task2Finished", "false");
        }

        if (!PlayerPrefs.HasKey("Task7Finished"))
        {
            PlayerPrefs.SetString("Task7Finished", "false");
        }

        if (!PlayerPrefs.HasKey("Task8Finished"))
        {
            PlayerPrefs.SetString("Task8Finished", "false");
        }

        //第五关
        //private bool Task5Start = false;
        if (!PlayerPrefs.HasKey("Task5Start"))
        {
            PlayerPrefs.SetString("Task5Start", "false");
        }
        //private int Task5Count = 0;
        if (!PlayerPrefs.HasKey("Task5Count"))
        {
            PlayerPrefs.SetInt("Task5Count", 0);
        }
        //private bool Task5Finished = false;
        if (!PlayerPrefs.HasKey("Task5Finished"))
        {
            PlayerPrefs.SetString("Task5Finished", "false");
        }

        //第六关
        //private bool Task6Start = false;
        if (!PlayerPrefs.HasKey("Task6Start"))
        {
            PlayerPrefs.SetString("Task6Start", "false");
        }
        //private bool Task6Finished = false;
        if (!PlayerPrefs.HasKey("Task6Finished"))
        {
            PlayerPrefs.SetString("Task6Finished", "false");
        }

        //第八关
        //public bool HaveFoZhu = false;
        if (!PlayerPrefs.HasKey("HaveFoZhu"))
        {
            PlayerPrefs.SetString("HaveFoZhu", "false");
        }


    }


    private void Awake()
    {
        CurrentHP = MaxHP;

    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    private CircleCollider2D circ;


    void Start()
    {
        if (!PlayerPrefs.HasKey("Task6Finished"))
        {
            initAbove();
            setFlow();
        }
        if (SceneManager.GetActiveScene().name == "level1_tut")
        {
            if (PlayerPrefs.HasKey("NCGameStart_First"))
            {
                //setFlow();
                Debug.Log("Set flow here" + SceneManager.GetActiveScene().name);
            }
        }
        

        readFlow();
        readSave();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        delayCounterCopy = delayCounter;
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        dashTime = startDashTime;
        rend = GameObject.FindObjectOfType<PlayerAnimation>().gameObject.GetComponent<Renderer>();
        rend.enabled = true;
        anim = GameObject.FindObjectOfType<PlayerAnimation>().gameObject.GetComponent<Animator>();
        anim.enabled = true;
        ///////////////////////////////////////////////////////////////
        circ = GetComponent<CircleCollider2D>();

        MC = Camera.main;
        CB = MC.GetComponent<CinemachineBrain>();
        CVC = CB.ActiveVirtualCamera as CinemachineVirtualCamera;
        CVC.m_Lens.OrthographicSize = CameraSize;
        PlayerPrefs.SetInt("LastSceneInt", SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetContacts(contacts);
        contactPoint = contacts[0].normal;

        //Debug.Log("Hit Wall!");
        CVC.m_Lens.OrthographicSize = 8; //CameraSize;

        CVC.m_Lens.OrthographicSize = CameraSize; //CameraSize;

        if (collision.gameObject.tag == "Normal Enemy"|| collision.gameObject.tag == "FlyEnemy")
        {

        }

        else if (collision.gameObject.tag == "StaticEnemy" && IsOnGround == false)
        {
            dashTime = 0;
            CurrentHP -= 20.0f;
            IsOnSpike = true;
        }

        else if (collision.gameObject.tag == "Spikes" && IsOnGround == false)
        {
            dashTime = 0;
            CurrentHP -= 20.0f;

            IsOnSpike = true;
        }

        else if (collision.gameObject.tag == "TowerSkin" && IsOnGround == false)
        {
            if (HaveFoZhu == false)
            {
                dashTime = 0;
                CurrentHP -= 20.0f;
                IsOnSpike = true;
            }
            else
            {
                T4Flag = true;
                T5Flag = true;

                gameObject.GetComponent<CircleCollider2D>().enabled = true;

                rb.velocity = Vector2.zero;

                dashOn = false;
                dashTime = startDashTime;
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                Instantiate(dashEffect, transform.position, Quaternion.identity);
 
                DoubleJumpCounter = 0;
                IsCountering = false;
                JumpTimesRemain = 2;

                JumpFromMovingObject = false;
                JumpFromRotateObject = false;

                IsOnGround = true;
            }
        }


        else if (collision.gameObject.tag == "Moving Spikes")
        {
            CurrentHP -= 20.0f;
        }
        else if (collision.gameObject.tag == "DashArrow")
        {
        }

        /////////////////////////////////////////////////获取能力////////////////////////////////////////////////////
        else if (collision.gameObject.tag == "TriggerDoubleJump")
        {
            CanDoubleJump = true;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "TriggerLongDash")
        {
            CanLongDash = true;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "TriggerCanAttack")
        {
            CanAttack = true;
            Destroy(collision.gameObject);
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////特殊/关键收集物以及任务/////////////////////////////////////////////////////////////
        else if (collision.gameObject.tag == "TaoMuJian")
        {
            myflowchart.SetBooleanVariable("HaveTaoMuJian", true);//注：这个bool在再次去找李玉凤后便在flowchart中设为false了（防止后半段对话重复发生）
            HaveTaoMuJian = true;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Level4BigTorch")
        {
            HaveBigTorch = true;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Level4FengHuoTai" && HaveBigTorch == true && T4Flag == true && collision.gameObject.GetComponent<ParticleSystemRenderer>().enabled == false)
        {
            collision.gameObject.GetComponent<ParticleSystemRenderer>().enabled = true;
            T4Flag = false;
            Task4Count += 1;
            if (Task4Count >= 7)
            {
                Task4Finished = true;
                myflowchart.SetBooleanVariable("NC4Finish_First", true);//注：这个bool在触发对话后便在flowchart中设为false了（防止对话重复发生）
                
            }
        }

        else if (collision.gameObject.tag == "Level5Book")
        {
            Task5Start = true;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Level5Collections" && Task5Start == true && T5Flag == true)
        {
            //Debug.LogWarning(Task5Start);
            Destroy(collision.gameObject);
            T5Flag = false;
            Task5Count += 1;
            if (Task5Count >= 3)
            {
                Task5Finished = true;
                myflowchart.SetBooleanVariable("NC5Finish_First", true);//注：这个bool在触发对话后便在flowchart中设为false了（防止对话重复发生）
            }


        }

        else if (collision.gameObject.tag == "NC6BDLG")
        {
            Task6Start = true;
        }

        else if (collision.gameObject.tag == "Level6Collection" && Task6Start == true)
        {
            Destroy(collision.gameObject);
            Task6Finished = true;
            myflowchart.SetBooleanVariable("NC6Finish_First", true);//注：这个bool在触发对话后便在flowchart中设为false了（防止对话重复发生）
        }

        else if (collision.gameObject.tag == "Level8FoZhu")
        {
            HaveFoZhu = true;
            Destroy(collision.gameObject);
        }

        ///////////////////////12.15新增
        //else if (collision.gameObject.tag == "NC8ExitDiGong")
        //{
        //    FinishAllLevels = true;
        //    myflowchart.SetBooleanVariable("FinishAllLevels", true);
        //}
        else if (collision.gameObject.tag == "NCLYF2")
        {
            if (HaveTaoMuJian == true)
            {
                Task2Finished = true;
            }
        }
        else if (collision.gameObject.tag == "JingYunZhong")
        {
            Task7Finished = true;
        }
        else if (collision.gameObject.tag == "NC8ExitDiGong")
        {
            Task8Finished = true;
        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                else if (IsOnSpike == false && (collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "Door" ||
            collision.gameObject.tag == "FlashObject"  || collision.gameObject.tag == "MoveObject" || collision.gameObject.tag == "DisappearObject" ||
            collision.gameObject.tag == "RotateObject" || collision.gameObject.tag == "Stoppable Moving Platform" || collision.gameObject.tag == "TowerSkin"))//跳跃落在墙面上
        {

            T5Flag = true;
            T4Flag = true;

            gameObject.GetComponent<CircleCollider2D>().enabled = true;

            rb.velocity = Vector2.zero;
            
            dashOn = false;
            dashTime = startDashTime;
            Instantiate(dashEffect, transform.position, Quaternion.identity);
            Instantiate(dashEffect, transform.position, Quaternion.identity);
            Instantiate(dashEffect, transform.position, Quaternion.identity);


            DoubleJumpCounter = 0;
            IsCountering = false;
            JumpTimesRemain = 2;

            //IsOnGround = true;
            JumpFromMovingObject = false;
            JumpFromRotateObject = false;

            /*Vector3 var = new Vector3(1f, 1f, 0f);*/
            //camera.GetComponent<CameraShake>().Shake();

            //Debug.LogWarning(collision);


            if (collision.gameObject.tag == "Terrain")
            {
                IsOnGround = true;
            }

            else if (collision.gameObject.tag == "MoveObject")
            {
                IsOnGround = true;
                IsOnMovingObject = true;
                MyMovingObject = collision.gameObject;
            }

            else if (collision.gameObject.tag == "RotateObject")
            {
                IsOnGround = true;
                IsOnRotateObject = true;
            }
        }
        

    }

    //private void OntriggerExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "NarrativeCollider1")
    //    {
    //        Destroy(collision.gameObject);
    //        Debug.LogError("sdfs dsd ");
    //    }
    //}




private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("bumped with:" + collision.collider.gameObject.name);

    }

    //private void OnTriggerExit2D(Collider2D collision)//解决从movingobject出发后卡墙问题
    //{
    //    if (collision .gameObject.tag == "MoveObject")
    //    {
    //        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    //    }
    //}


    void Update()
    {
        Debug.Log("task2finished" + Task2Finished);
        Debug.Log("task4finished" + Task4Finished);
        Debug.Log("task5finished" + Task5Finished);
        Debug.Log("task6finished" + Task6Finished);
        Debug.Log("task7finished" + Task7Finished);
        Debug.Log("task8finished" + Task8Finished);

        if (Task2Finished && Task4Finished && Task5Finished && Task6Finished && Task7Finished && Task8Finished)
        {
            FinishAllLevels = true;
            myflowchart.SetBooleanVariable("EndingStart", true);
            myflowchart.SetBooleanVariable("Ending", true);
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        //#region dashArrow
        //SpriteRenderer dashArrow;
        //dashArrow = transform.Find("dashArrow").GetComponent<SpriteRenderer>();
        //if (dashTime == startDashTime)
        //{
        //    dashArrow.enabled = true;
        //}
        //else
        //{
        //    dashArrow.enabled = false;
        //}
        //#endregion




        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //if (Input.GetMouseButtonDown(1) && dashOn == false)
        //{
        //    if (dashTime == startDashTime && JumpTimesRemain > 0)
        //    {
        //        if (JumpTimesRemain == 2)
        //        {
        //            initPos = transform.position;
        //        }
        //        direction = mousePos - transform.position;
        //        //Debug.Log("mousePos"+mousePos);
        //        //Debug.Log("transPos"+transform.position);
        //        direction.Normalize();
        //        //Debug.Log("direction"+direction);
        //        rb.velocity = direction * dashSpeed;
        //        dashOn = true;
        //        IsOnGround = false;

        //        //Debug.Log("speed"+rb.velocity);
        //    }
        //}
        //else if (dashOn == true)
        ////dashTime!=startDashTime, which means "during a dash"
        //{
        //    if (dashTime <= 0)
        //    {
        //        rb.velocity = Vector2.zero;
        //        delayCounter -= Time.deltaTime;
        //        rend.enabled = false;
        //        if (delayCounter <= 0)//没能在冲刺结束前碰到墙壁等
        //        {
        //            rend.enabled = true;

        //            dashOn = false;
        //            dashTime = startDashTime;

        //            if (CanDoubleJump)
        //            {
        //                JumpTimesRemain--;
        //                Invoke("PositionGoBack", TimeBetweenJumps);//延迟一秒后再回到之前保存的位置
        //            }

        //            else
        //            {
        //                transform.position = initPos;
        //                delayCounter = delayCounterCopy;
        //                JumpTimesRemain = 2;
        //            }

        //        }
        //    }
        //    else //dashTime>0 & dashTime<startDashTime
        //    {
        //        dashTime -= Time.deltaTime;

        //        /*Debug.Log(dashTime);*/
        //    }
        //}

#elif UNITY_ANDROID || UNITY_IOS


// touch mechanics
// 		          if ((Input.touchCount > 0) && dashOn == false)
// 		          {
// 		              touch = Input.GetTouch(0);
// 		              switch (Input.touches[0].phase)
// 		              {
// 		                  case TouchPhase.Began:
// 		                      begin = touch.position;
// 		                      break;
// 		                  case TouchPhase.Ended:
// 		                      end = touch.position;
// 		                      if ((begin - end).magnitude < 50)
// 		                      {
// 		                         Debug.Log("TAPPED");
// 		                         if (dashTime == startDashTime)
// 		                          {
// 		                              initPos = transform.position;
// 		                              tapPoint = Camera.main.ScreenToWorldPoint(end);
// 		                              direction = tapPoint - transform.position;
// 		                              //Debug.Log("mousePos"+mousePos);
// 		                              //Debug.Log("transPos"+transform.position);
// 		                              direction.Normalize();
// 		                              //Debug.Log("direction"+direction);
// 		                              rb.velocity = direction * dashSpeed;
// 		                              dashOn = true;
// 		                              //Debug.Log("speed"+rb.velocity);
// 		                          }
// 		                      }
// 		                      else
// 		                      {
// 		                          Debug.Log(begin - end);
// 		                          Debug.Log((begin - end).magnitude);
// 		                          if (dashTime == startDashTime)
// 		                          {
// 		                              initPos = transform.position;
// 		                              direction = end - begin;
// 		                              //Debug.Log("mousePos"+mousePos);
// 		                              //Debug.Log("transPos"+transform.position);
// 		                              direction.Normalize();
// 		                              //Debug.Log("direction"+direction);
// 		                              rb.velocity = direction * dashSpeed;
// 		                              dashOn = true;
// 		                              //Debug.Log("speed"+rb.velocity);
// 		                          }
// 		                      }
// 		                      break;
// 		                  default:
// 		                      break;
// 		              }
// 		          }
// 		          else if (dashOn == true)
// 		          {
// 		              if (dashTime <= 0)
// 		              {
// 		                  rb.velocity = Vector2.zero;
// 		                  delayCounter -= Time.deltaTime;
// 		                  rend.enabled = false;
// 		                  if (delayCounter <= 0)
// 		                  {
// 		                      rend.enabled = true;
// 		                      transform.position = initPos;
// 		                      dashOn = false;
// 		                      dashTime = startDashTime;
// 		                      delayCounter = delayCounterCopy;
// 		                      //Debug.Log("set to 0");
// 		                  }
// 		              }
// 		              else //dashTime>0 & dashTime<startDashTime
// 		              {
// 		                  dashTime -= Time.deltaTime;
// 		  
// 		                  /*Debug.Log(dashTime);*/
// 		              }
// 		 		 }
       


#endif
    }


    //private void PositionGoBack()
    //{//跳跃到空中后闪回出发点
    //    if (IsOnGround == false && JumpTimesRemain < 2)
    //    {
    //        transform.position = initPos;
    //        delayCounter = delayCounterCopy;
    //    }
    //    JumpTimesRemain = 2;
    //}
}