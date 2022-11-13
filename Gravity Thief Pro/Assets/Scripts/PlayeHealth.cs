using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeHealth : MonoBehaviour
{
    private int currentHealth;
    private bool duringDash;
    private int hurtCountdown = 0;
    public float damage = 0.5f;

    [SerializeField] private GameObject bloodEffect;

    player thePlayer;
    


    private void Start()
    {
        thePlayer = GameObject.FindObjectOfType<player>();
    }

    private void FixedUpdate()
    {
        duringDash = gameObject.GetComponent<player>().dashOn;
        if (hurtCountdown > 0) hurtCountdown--;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Normal Enemy"
            || collider2D.gameObject.tag == "New Fly Enemy"
            || collider2D.gameObject.tag == "StaticEnemy"
            || collider2D.gameObject.tag == "FlyEnemy")
        {
            if (duringDash)
            {
                if (gameObject.GetComponent<player>().CanAttack)
                {//The attack effect doesn't happen here
                }
                else
                {//take damage
                    takeDamage();
                }
            }
            else
            {// when not in a dash
                if (hurtCountdown != 0)
                {//player just experience a hurt, no damage
                }

                else
                {//take damage
                    takeDamage();
                }
                
            }
        }
        if (collider2D.gameObject.tag == "Spikes"
            || collider2D.gameObject.tag == "Moving Spikes")
        {
            takeDamage();
        }
        if (collider2D.gameObject.tag == "TowerSkin" && thePlayer.IsOnGround == false && thePlayer.HaveFoZhu)
        {
            takeDamage();
        }

    }

    void takeDamage()
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        GameObject.FindObjectOfType<PlayerAnimation>().playerBoom();
        StartCoroutine(Flash());
        hurtCountdown = 100;
    }

    void takeDamage(float x)
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        gameObject.GetComponent<PlayerStats>().TakeDamage(x);
        StartCoroutine(Flash());
        hurtCountdown = 100;
    }

    IEnumerator Flash()
    {
        Color ogColor = new Color(1,1,1,1f);
        for (int n = 0; n < 2; n++)
        {
            GameObject.FindObjectOfType<PlayerAnimation>().gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 10f);
            yield return new WaitForSeconds(0.2f);
            GameObject.FindObjectOfType<PlayerAnimation>().gameObject.GetComponent<SpriteRenderer>().material.color = ogColor;
            yield return new WaitForSeconds(0.2f);
            GameObject.FindObjectOfType<PlayerAnimation>().gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 10f);
            yield return new WaitForSeconds(0.2f);
            GameObject.FindObjectOfType<PlayerAnimation>().gameObject.GetComponent<SpriteRenderer>().material.color = ogColor;
            yield return new WaitForSeconds(0.2f);
            GameObject.FindObjectOfType<PlayerAnimation>().gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 10f);
            yield return new WaitForSeconds(0.2f);
            GameObject.FindObjectOfType<PlayerAnimation>().gameObject.GetComponent<SpriteRenderer>().material.color = ogColor;
            //            // gameObject.GetComponent<SpriteRenderer>().material.color = ogColor;

        }
    }

}
