using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoppableMovingPlatform : MonoBehaviour
{
    [SerializeField] private float Speed = 10;
    public List<Vector3> wayPoints;

    private bool comeBacking = false;
    private bool startMoving = false;
    private int currentTargetIndex = 0;
    private bool repeatIngnorePlayer;

    private void Start()
    {
        wayPoints.Add(transform.position);
        foreach (Transform child in transform)
        {
             wayPoints.Add(child.position);
        }

        foreach (Vector3 way in wayPoints)
        {

        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {//What happens when the player touch terrain(the foreground grid):
            comeBacking = true;
            startMoving = false;
            currentTargetIndex--;
        }
        else
        {
            Debug.Log("The stoppable platform collided with:" + collision.gameObject.name);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {//What happens when the player touch terrain(the foreground grid):
            comeBacking = true;
            startMoving = false;
            if (currentTargetIndex > 0)
            {
                currentTargetIndex--;
            }
        }
        else if (collision.gameObject.tag == "Player")
        {//What happens when the player touch the platform:
            collision.gameObject.transform.SetParent(transform);
            //collision.gameObject.GetComponent<player>().IsOnMovingObject = true;
            if (!comeBacking && !startMoving)
            {
                startMoving = true;
                currentTargetIndex = 1;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {//What happens when the player touch the platform:
            collision.gameObject.transform.SetParent(null);
            //collision.gameObject.GetComponent<player>().IsOnMovingObject = false;
            StartCoroutine(disableCollider());
        }
        
    }

    IEnumerator disableCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForEndOfFrame();
        //yield return new WaitForSeconds(1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void Update()
    {
        //Check whether platform reached the next target
        if (comeBacking)
        {

            if (AlmostEqual(gameObject.transform.position, wayPoints[0]))
            {
                comeBacking = false;
            }
            else
            {
                for (int i = 1; i < wayPoints.Count; i++)
                {

                    if (AlmostEqual(gameObject.transform.position, wayPoints[i]))
                    {
                        currentTargetIndex = i - 1;
                    }
                }
            }

        }
        else if (startMoving)
        {
            if (AlmostEqual(gameObject.transform.position, wayPoints[wayPoints.Count - 1]) )
            {
                startMoving = false;
                comeBacking = true;
            }
            else
            {
                for (int i = 1; i < wayPoints.Count; i++)
                {

                    if (AlmostEqual(gameObject.transform.position, wayPoints[i]))
                    {
                        if (currentTargetIndex < wayPoints.Count)
                        {
                            currentTargetIndex = i + 1;
                        }
                    }
                }
            }

        }


        if (comeBacking)
        {

            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, wayPoints[currentTargetIndex], Time.deltaTime * Speed/2);

        }
        
        else if (startMoving)
        {

            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, wayPoints[currentTargetIndex], Time.deltaTime * (Speed*10));

        }
        
    }

    public static bool AlmostEqual(Vector3 v1, Vector3 v2)
    {
        bool equal = true;

        if (Mathf.Abs(v1.x - v2.x) > 0.001) equal = false;
        if (Mathf.Abs(v1.y - v2.y) > 0.001) equal = false;
        if (Mathf.Abs(v1.z - v2.z) > 0.001) equal = false;

        return equal;
    }

}
