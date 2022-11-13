using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnPoint : MonoBehaviour
{
    public List<Vector3> spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child.position);
        }
        if (SceneManager.GetActiveScene().name == "level1_tut") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "Main1_ZYF")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/0];
            }
        }

        if (SceneManager.GetActiveScene().name == "Main1_ZYF") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "level1_tut")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/0];
            }
        }
        if (SceneManager.GetActiveScene().name == "Main1_ZYF") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_2")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/1];
            }
        }



        if (SceneManager.GetActiveScene().name == "TD_2") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "Main1_ZYF")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/0];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_2") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_WJH")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/1];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_2") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_4")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/2];
            }
        }


        if (SceneManager.GetActiveScene().name == "TD_WJH") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_2")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/0];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_WJH") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_8")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/1];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_WJH") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_YXG")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/2];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_WJH") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_5")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/3];
            }
        }

        ///-----------------------------------------------------------------------------
        if (SceneManager.GetActiveScene().name == "TD_4") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_2")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/0];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_4") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_5")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/1];
            }
        }

        ///-----------------------------------------------------------------------------
        if (SceneManager.GetActiveScene().name == "TD_5") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_4")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/2];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_5") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_WJH")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/0];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_5") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "Level _06")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/1];
            }
        }
        /*-----------------------------------------------------------------------------*/
         if (SceneManager.GetActiveScene().name == "Level _06") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_YXG")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/1];
            }
        }
        if (SceneManager.GetActiveScene().name == "Level _06") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_5")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/0];
            }
        }
        /*-----------------------------------------------------------------------------*/
        if (SceneManager.GetActiveScene().name == "TD_YXG") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "Level _06")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/0];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_YXG") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_WJH")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/1];

            }
        }
        if (SceneManager.GetActiveScene().name == "TD_YXG") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_8")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/2];

            }
        }
        /*-----------------------------------------------------------------------------*/
        if (SceneManager.GetActiveScene().name == "TD_8") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_WJH")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/1];

            }
        }
        if (SceneManager.GetActiveScene().name == "TD_8") //如果现在的scene是某个
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_YXG")//上一个scene是什么
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*当前场景某一点，通过拖spawnpoint确认复活点*/0];

            }
        }
    }

}
