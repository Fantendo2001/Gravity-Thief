using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleEnemy : MonoBehaviour
{
    [SerializeField] private GameObject dashEffect;
    [SerializeField] private GameObject dashEffect2;
    player thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindObjectOfType<player>();
    }


    private void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log(c.gameObject.tag);
        if (c.gameObject.tag == "Player" 
            && thePlayer.CanAttack 
            && thePlayer.dashOn)
        {
            Instantiate(dashEffect2, transform.position, Quaternion.identity);
            Instantiate(dashEffect2, transform.position, Quaternion.identity);
            Instantiate(dashEffect, transform.position, Quaternion.identity);
            Instantiate(dashEffect, transform.position, Quaternion.identity);
            GameObject.FindObjectOfType<PlayerAnimation>().playBodyBoom();
            Destroy(gameObject);
            StartCoroutine(disableEnemy());
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator disableEnemy()
    {
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
    }
}
