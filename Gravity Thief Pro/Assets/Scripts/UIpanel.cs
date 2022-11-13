using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIpanel : MonoBehaviour
{
    public GameObject map;
    public GameObject coll;
    public GameObject setting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void toggleMap()
    {
        if (map.activeSelf)
        {
            map.SetActive(false);
        }
        else
        {
            map.SetActive(true);
        }
    }
    
    public void closeMap()
    {
        if (map.activeSelf)
        {
            map.SetActive(false);
        }
    }

    public void toggleColl()
    {
        if (coll.activeSelf)
        {
            coll.SetActive(false);
        }
        else
        {
            coll.SetActive(true);
        }
    }
    
    public void closeColl()
    {
        if (coll.activeSelf)
        {
            coll.SetActive(false);
        }
    }

    public void toggleSetting()
    {
        if (setting.activeSelf)
        {
            setting.SetActive(false);
        }
        else
        {
            setting.SetActive(true);
        }
    }
    
    public void closeSetting()
    {
        if (setting.activeSelf)
        {
            setting.SetActive(false);
        }
    }

    public void loadMenu()
    {
        LevelManager._instance.loadLevel(0);
    }

    public void reset()
    {
        GameObject.FindObjectOfType<PlayerStats>().resetHealth();
    }

    public void loadNext()
    {
        if (SceneManager.GetActiveScene().buildIndex == 9)
        {
            LevelManager._instance.loadLevel(1);
        }
        else
        {
            LevelManager._instance.loadLevel(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }

    public void unlockAttack()
    {
        GameObject.FindObjectOfType<player>().CanAttack = true;
    }

    public void unlockDouble()
    {
        GameObject.FindObjectOfType<player>().CanDoubleJump = true;
    }

    public void unlockLong()
    {
        GameObject.FindObjectOfType<player>().CanLongDash = true;
    }

    public void toggleDamage()
    {
        if (GameObject.FindObjectOfType<PlayeHealth>().damage == 0)
        {
            GameObject.FindObjectOfType<PlayeHealth>().damage = 0.5f;
        }
        else
        {
            GameObject.FindObjectOfType<PlayeHealth>().damage = 0;
        }
    }

    public void unlockEnding()
    {
        PlayerPrefs.SetString("NCStart3_First", "false");
        GameObject.FindObjectOfType<player>().myflowchart.SetBooleanVariable("NCStart3_First", false);

        PlayerPrefs.SetString("NC3GetAttack_First", "false");
        GameObject.FindObjectOfType<player>().myflowchart.SetBooleanVariable("NC3GetAttack_First", false);

        PlayerPrefs.SetString("NC3Terminal_First", "false");
        GameObject.FindObjectOfType<player>().myflowchart.SetBooleanVariable("NC3Terminal_First", false);

        PlayerPrefs.SetString("FinishAllLevels", "true");
        GameObject.FindObjectOfType<player>().myflowchart.SetBooleanVariable("FinishAllLevels", true);
        GameObject.FindObjectOfType<player>().FinishAllLevels = true;

        PlayerPrefs.SetString("Ending", "true");
        GameObject.FindObjectOfType<player>().myflowchart.SetBooleanVariable("Ending", true);

        PlayerPrefs.SetString("EndingStart", "true");
        GameObject.FindObjectOfType<player>().myflowchart.SetBooleanVariable("EndingStart", true);
    }
}
