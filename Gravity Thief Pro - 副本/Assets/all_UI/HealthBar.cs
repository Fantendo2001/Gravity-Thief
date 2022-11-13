using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image HealthPoint;
    public Image HealthPointEffect;

    [SerializeField] private float HurtSpeed = 0.002f;

    //Get Player HP
    private player MyPlayer;

    private void Awake()
    {
        MyPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    }

    private void Update()
    {
        HealthPoint.fillAmount = MyPlayer.CurrentHP / MyPlayer.MaxHP;

        if(HealthPointEffect.fillAmount > HealthPoint.fillAmount)
        {
            HealthPointEffect.fillAmount -= HurtSpeed;
        }
        else
        {
            HealthPointEffect.fillAmount = HealthPoint.fillAmount;
        }
    }
}
