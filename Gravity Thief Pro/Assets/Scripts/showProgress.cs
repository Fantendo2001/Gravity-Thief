using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showProgress : MonoBehaviour
{
    public List<GameObject> objects;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform trans in gameObject.transform)
        {
            objects.Add(trans.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        player thePLayer = GameObject.FindObjectOfType<player>();
        if (thePLayer.Task2Finished)
        {
            objects[0].SetActive(true);
        }
        else
        {
            objects[0].SetActive(false);
        }

        if (thePLayer.Task4Finished)
        {
            objects[1].SetActive(true);
        }
        else
        {
            objects[1].SetActive(false);
        }

        if (thePLayer.Task5Finished)
        {
            objects[2].SetActive(true);
        }
        else
        {
            objects[2].SetActive(false);
        }

        if (thePLayer.Task6Finished)
        {
            objects[3].SetActive(true);
        }
        else
        {
            objects[3].SetActive(false);
        }

        if (thePLayer.Task7Finished)
        {
            objects[4].SetActive(true);
        }
        else
        {
            objects[4].SetActive(false);
        }

        if (thePLayer.Task8Finished)
        {
            objects[5].SetActive(true);
        }
        else
        {
            objects[5].SetActive(false);
        }

        if (thePLayer.FinishAllLevels)
        {
            foreach (GameObject item in objects)
            {
                item.SetActive(true);
            }
        }

    }
}
