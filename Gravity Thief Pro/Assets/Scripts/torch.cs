using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torch : MonoBehaviour
{
    private SpriteRenderer sr;
    private player thePlayer;
    private Animator anim;
    private bool changed = false;
    public float  R_After, G_After, B_After;
    private bool changeAfter = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
       
        thePlayer = GameObject.FindObjectOfType<player>();
        //sr.material.color = new Color(1, 1, 1, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!changed)
        {
            changeState();
            
        }
        //if (changeAfter==true)
        //{
        //    sr.material.color = new Color(R_before, G_before, B_before);
        //}
        
    }


    public void changeState()
    {
        Vector3 playerPos = thePlayer.transform.position;
        if ((playerPos - this.transform.position).magnitude < 2)
        {
            //changeAfter = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 2f);
            StartCoroutine(FadeTo(4f, 1.0f));
            //sr.material.color = new Color(R_before, G_before, B_before);//new Color(0.3882353f, 0.572549f, 0.5019608f); 第二关

            anim.speed = anim.speed * 4f;
            changed = true;
            gameObject.GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<PlayerStats>().torches++;
            GameObject.FindObjectOfType<PlayerStats>().growHealth();
        }
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = sr.material.color.a;
        for (float t = 0.0f; t < 0.75f; t += Time.deltaTime / aTime)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(R_After, G_After, B_After, Mathf.Lerp(alpha, aValue, t));//new Color(0.1f, 2f, 0.2f,第二关
            
            yield return new  WaitForSeconds(5f);
        }
    }
}
