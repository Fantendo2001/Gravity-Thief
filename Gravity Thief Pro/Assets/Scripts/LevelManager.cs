using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager _instance;           //singleton
    public static int currentLevel { get; set; }

    public Animator animator;                       // set in inspector

    // save (PlayerPrefs)
    public const string LEVEL_PROGRESS = "LevelProgress";

    public static Dictionary<int, string> levelNameMapping = new Dictionary<int, string> {
        {-1, "Menu" },
        {0, "level1_tut" },
        {1, "Main1_ZYF" },
        {2, "02_Portal_tut" },
        {3, "03_portaltut" },
        {4, "04_GlassBreak" },
        {5, "05_shotAcross" },
        {6, "06_SpeedNumTut" },
        {7, "07_SpeedSum 1" },
        {8, "08_PortalRespawn" },
        {9, "10_trigger_tut" },
        {10, "11_trigger2" },
        {11, "12_trigger3" },
        {12, "13_trigger4" },
        {13, "14_gravity_tut" },
        {14, "15_gravity2" },
        {15, "09_SpeedSumFinal" },
        {16, "Level-Ending" },
        {17, "Level-Bonus" }
    };

    private void Awake()
    {
        if (_instance != null) Destroy(_instance);

        _instance = this;
    }

    public void RestartCurrentLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void BackToMenu()
    {
        loadLevel(0);
    }
    public void StartOver()
    {
        // clear save

        loadLevel(0);
    }

    public void LoadCurrentLevel()
    {
        loadLevel(currentLevel);
    }

    public void loadLevel(int levelIndex)
    {

        // player fade out animation
        if (animator != null)
            animator.SetTrigger("FadeOut");
        else
            Debug.Log("LevelManager: animator is not set.");

        if (levelIndex != -1)             //not menu
            currentLevel = levelIndex;
        PlayerPrefs.SetString("LastSceneName", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("LastSceneInt", SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            GameObject.FindObjectOfType<player>().setFlow();
            GameObject.FindObjectOfType<player>().SaveAbove();
        }
        StartCoroutine("WaitAndChangeLoadScene", levelIndex);
        
        PlayerPrefs.Save();
    }

    IEnumerator WaitAndChangeLoadScene(int levelIndex)
    {
        yield return new WaitForSeconds(0.9f);
        animator.ResetTrigger("FadeOut");
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }

    public void loadNextLevel()
    {
        if (levelNameMapping.ContainsKey(currentLevel + 1))
        {
            loadLevel(currentLevel + 1);
        } 
        else
        {
            Debug.Log("No next level available.");
            
            if (SceneManager.GetActiveScene().name == "Level-Bonus")
                BackToMenu();
            else
                loadLevel(currentLevel);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LinkToGithub()
    {
        Application.OpenURL("https://www.aliyundrive.com/s/9at8rPMghEj");
    }


    public void startNewGame()
    {
        PlayerPrefs.DeleteAll();
        loadLevel(9);
    }

    public void contGame()
    {
        if (PlayerPrefs.HasKey("LastSceneInt"))
        {
            loadLevel(PlayerPrefs.GetInt("LastSceneInt"));
        }
        else
        {
            loadLevel(9);
        }
    }

    
}
