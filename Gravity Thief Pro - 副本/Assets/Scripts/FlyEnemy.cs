using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] private GameObject dashEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log(c.gameObject.tag);
        if (c.gameObject.tag == "Player")
        {
            Debug.Log("touched");
            Invoke("DestroyAndEffect", 1.5f);
        }
    }

    private void DestroyAndEffect()
    {
        gameObject.SetActive(false);
        Instantiate(dashEffect, transform.position, Quaternion.identity);
        Instantiate(dashEffect, transform.position, Quaternion.identity);
        Instantiate(dashEffect, transform.position, Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
