using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showTorches : MonoBehaviour
{
    int torches;

    // Update is called once per frame
    void Update()
    {
        torches = GameObject.FindObjectOfType<PlayerStats>().torches;
        GetComponent<Text>().text = "<color=#feae34>"+torches.ToString() + "</color>" + "/10";
    }
}
