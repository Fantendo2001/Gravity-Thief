using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class DashArrow : MonoBehaviour
{
    public PlayerMove MyPlayerMove;

    private Vector2 direction;
    Vector2 initPos;
    private Vector3 mousePos;
    private Vector3 lastMousePos;
    private float mouseTimer = 0;
    private Touch touch;
    private Vector3 begin, now, end, diff, tapPoint;
    SpriteRenderer sr;
    private float angle;

    [SerializeField] private Joystick joystick;
    // Start is called before the first frame update
    void Start()
    {
        joystick = GameObject.FindObjectOfType<Joystick>();
         sr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);

        if (collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "Door")
        {
            sr.color = Color.green;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "Door")
        {
            sr.color = Color.gray;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float MyHoldTime = MyPlayerMove.HoldTime;

        if (MyHoldTime > 0.5f && MyHoldTime < 1.5f)
        {
            sr.transform.localScale = new Vector3(sr.transform.localScale.x, MyHoldTime * 2.52f, sr.transform.localScale.z);
        }
        else if (MyHoldTime >= 1.5f)
        {
            sr.transform.localScale = new Vector3(sr.transform.localScale.x, 3.78f, sr.transform.localScale.z);
        }
        else
        {
            sr.transform.localScale = new Vector3(sr.transform.localScale.x, 1.26f, sr.transform.localScale.z);
        }



        #region BASIC
#if UNITY_EDITOR || UNITY_STANDALONE
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        initPos = transform.position;
        direction = (mousePos - transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        if (angle < 0) angle += 360;


        /*gameObject.transform.localEulerAngles = new Vector3(0,0, angle);*/

        // phone below
        //if (joystick.Direction != Vector2.zero)
        //{
        //    direction=joystick.Direction;
        //    direction.Normalize();
        //    angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        //    ShowArrow();

        //}
        //else
        //{
        //    HideArrow();
        //}




#elif UNITY_ANDROID || UNITY_IOS
// phone below
        if (joystick.Direction != Vector2.zero)
        {
            direction=joystick.Direction;
            direction.Normalize();
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
            ShowArrow();
            
        }
        else
        {
            HideArrow();
        }
#endif
        gameObject.transform.localEulerAngles = new Vector3(0,0, angle);
        #endregion

    }

    void ShowArrow()
    {
        sr.material.color =  new Color(1, 1, 1, 1f);
    }
    void HideArrow()
    {
        sr.material.color = new Color(1, 1, 1, 0f);
    }

    private void FixedUpdate()
    {
        
    }
}
