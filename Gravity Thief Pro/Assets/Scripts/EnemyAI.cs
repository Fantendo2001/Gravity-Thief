using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAI : MonoBehaviour
{
    public Transform target;
    private Vector3 ogPosi;
    
    public float speed = 200f;
    float ogSpeed;
    public float nextPointDist = 3f;
    public Transform enemyGFX;
    private Transform ogTran;

    Path path;
    int currentWayPoint = 0;
    bool reachedEnd = false;

    [SerializeField]
    int trackDistance = 15,
        aggroDistance = 5,
        range = 22;
    [SerializeField]
    private GameObject
    dashEffect,
    dashEffect2;

    player thePlayer;
    GameObject og;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindObjectOfType<player>();
        target = thePlayer.gameObject.transform;
        ogPosi = transform.position;
        ogSpeed = speed;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        seeker.StartPath(rb.position, target.position, OnPathComplete);
        og = new GameObject("ogTran");
        og.transform.position = ogPosi;
    }

    void UpdatePath()
    {
        
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
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
                Instantiate(dashEffect, transform.position, dashEffect.transform.rotation);
                Instantiate(dashEffect2, transform.position, Quaternion.identity);
                Instantiate(dashEffect2, transform.position, Quaternion.identity);
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                GameObject.FindObjectOfType<PlayerAnimation>().playBodyBoom();
                Destroy(gameObject);
                StartCoroutine(disableEnemy());
            }
        }
        else
        {
            rb.velocity = new Vector2 (0,0);
        }
    }
    IEnumerator disableEnemy()
    {
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        target = thePlayer.gameObject.transform;
        //check aggro range for the enmey
        if (target != og.transform)
        {
            if (path.path.Count > trackDistance)
            {
                return;
            }
        }

        speed = ogSpeed;
        enemyGFX.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        enemyGFX.GetComponent<Animator>().speed = 1;
        if (Vector3.Distance(ogPosi, thePlayer.gameObject.transform.position) > range)
        {
            target = og.transform;
        }
        else
        {
            if (path.path.Count < aggroDistance)
            {
                speed = 6 * ogSpeed;
                enemyGFX.GetComponent<Animator>().speed = 6;
                enemyGFX.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        
        Debug.Log("range:" + Vector3.Distance(ogPosi, thePlayer.gameObject.transform.position));
        
        

        

        if (path == null)
        {
            return;
        }
        
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        }
        else
        {
            reachedEnd = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextPointDist)
        {
            currentWayPoint++;
        }

        if (rb.velocity.x >= 0.02f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.velocity.x <= 0.02f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    
}
