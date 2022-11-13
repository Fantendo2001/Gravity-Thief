using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Mapper : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] maps;
    void Start()
    {
        updateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void updateMap()
    {
        maps[0].SetActive(true);
        for (int i = 1; i < maps.Length; i++)
        {
            if (i == SceneManager.GetActiveScene().buildIndex)
            {
                maps[i].SetActive(true);
            }
            else
            {
                maps[i].SetActive(false);
            }
        }
        if (9 == SceneManager.GetActiveScene().buildIndex)
        {
            maps[1].SetActive(true);
        }
    }
}
