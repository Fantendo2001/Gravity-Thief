using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    player thePlayer;
    public AudioSource bodyBoom;
    public AudioSource dash;
    public AudioSource playerboom;
    private void Start()
    {
        thePlayer = GameObject.FindObjectOfType<player>();
    }

    private void FixedUpdate()
    {
        transform.up = -thePlayer.contactPoint;
    }

    public void playBodyBoom()
    {
        bodyBoom.Play();
    }

    public void playDash()
    {
        dash.Play();
    }

    public void playerBoom()
    {
        playerboom.Play();
    }
}
