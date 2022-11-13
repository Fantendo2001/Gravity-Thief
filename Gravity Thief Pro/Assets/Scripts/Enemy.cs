using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject alive; // the fox enemy itself
    private Rigidbody2D aliveRb; // the fox enemy rigidbody
    
    [SerializeField]
    private GameObject 
        dashEffect,
        dashEffect2; 

    private Animator anim;



    [SerializeField]
    private float
    groundCheckDistance,
    wallCheckDistance,
    movementSpeed;
    

    private Vector2 movement;


    //--MOVEMENT DECTECTION VARIABLES--------------------------------------------------------------------------------
    [SerializeField]
    private LayerMask whatIsGround;//for movement detection
    [SerializeField]
    private Transform //for movement detection
        groundCheck,
        wallCheck;
    private bool //for movement detection
        groundDetected,
        wallDetected;
    private int
        facingDirection;
    //---------------------------------------------------------------------------------------------------------------


    private enum State
    {
        Moving,
        Dead
    }

    private State currentState;

    public player thePlayer;

    //--LEGACY CODE--------------------------------------------------------------------------------
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        alive = gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        facingDirection = 1;
        thePlayer = GameObject.FindObjectOfType<player>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(c.gameObject.tag);
        if (collider.gameObject.tag == "Player")
        {
            //Debug.Log("touched");
            thePlayer = collider.gameObject.GetComponent<player>();

            if (thePlayer.CanAttack && thePlayer.dashOn)
            {
                EnterDeadState();
            }
        }
    }


    //--NEW CODE--------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    public void playStep()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    //--WALKING STATE--------------------------------------------------------------------------------

    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, -transform.up, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if (!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            //movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
            //Debug.Log(movement);
            //aliveRb.velocity = movement;
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, gameObject.transform.position+transform.right, Time.deltaTime * movementSpeed);
        }
    }

    private void ExitMovingState()
    {

    }


    //--DEAD STATE---------------------------------------------------------------------------------------

    private void EnterDeadState()
    {
        Instantiate(dashEffect, transform.position, dashEffect.transform.rotation);
        Instantiate(dashEffect2, transform.position, Quaternion.identity);
        Instantiate(dashEffect2, transform.position, Quaternion.identity);
        Instantiate(dashEffect, transform.position, Quaternion.identity);
        Instantiate(dashEffect, transform.position, Quaternion.identity);
        GameObject.FindObjectOfType<PlayerAnimation>().playBodyBoom();
        Destroy(gameObject);
        StartCoroutine(disableEnemy());
    }

    IEnumerator disableEnemy()
    {
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }




    //--OTHER FUNCTIONS--------------------------------------------------------------------------------
    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}
