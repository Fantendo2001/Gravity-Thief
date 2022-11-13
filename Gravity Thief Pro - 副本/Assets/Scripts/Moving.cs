using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private float Speed = 10;
    private GameObject desi;
    private Vector3 desiVec2;
    private Vector3 ogPos;
    // Start is called before the first frame update
    void Start()
    {
        ogPos = gameObject.transform.position;
        foreach (Transform child in transform)
        {
            desiVec2 = child.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.transform.position == desiVec2)
        {
            gameObject.transform.position = ogPos;
        }
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, desiVec2, Time.deltaTime * Speed);  
    }
}
