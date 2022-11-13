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
        if (SceneManager.GetActiveScene().name == "level1_tut") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "Main1_ZYF")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/0];
            }
        }

        if (SceneManager.GetActiveScene().name == "Main1_ZYF") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "level1_tut")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/0];
            }
        }
        if (SceneManager.GetActiveScene().name == "Main1_ZYF") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_2")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/1];
            }
        }



        if (SceneManager.GetActiveScene().name == "TD_2") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "Main1_ZYF")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/0];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_2") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_WJH")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/1];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_2") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_4")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/2];
            }
        }


        if (SceneManager.GetActiveScene().name == "TD_WJH") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_2")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/0];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_WJH") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_8")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/1];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_WJH") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_YXG")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/2];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_WJH") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_5")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/3];
            }
        }

        ///-----------------------------------------------------------------------------
        if (SceneManager.GetActiveScene().name == "TD_4") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_2")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/0];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_4") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_5")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/1];
            }
        }

        ///-----------------------------------------------------------------------------
        if (SceneManager.GetActiveScene().name == "TD_5") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_4")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/2];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_5") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_WJH")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/0];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_5") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "Level _06")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/1];
            }
        }
        /*-----------------------------------------------------------------------------*/
         if (SceneManager.GetActiveScene().name == "Level _06") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_YXG")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/1];
            }
        }
        if (SceneManager.GetActiveScene().name == "Level _06") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_5")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/0];
            }
        }
        /*-----------------------------------------------------------------------------*/
        if (SceneManager.GetActiveScene().name == "TD_YXG") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "Level _06")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/0];
            }
        }
        if (SceneManager.GetActiveScene().name == "TD_YXG") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_WJH")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/1];

            }
        }
        if (SceneManager.GetActiveScene().name == "TD_YXG") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_8")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/2];

            }
        }
        /*-----------------------------------------------------------------------------*/
        if (SceneManager.GetActiveScene().name == "TD_8") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_WJH")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/1];

            }
        }
        if (SceneManager.GetActiveScene().name == "TD_8") //������ڵ�scene��ĳ��
        {
            if (PlayerPrefs.GetString("LastSceneName") == "TD_YXG")//��һ��scene��ʲô
            {
                GameObject.FindObjectOfType<player>().transform.position = spawnPoints[/*��ǰ����ĳһ�㣬ͨ����spawnpointȷ�ϸ����*/0];

            }
        }
    }

}
