using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMplayer : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] songs;

    private void Awake()
    {
        playSong(SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().name == "Main1_ZYF")
        {
            playSong(1);   
        }
    }

    void playSong(int x)
    {
        gameObject.GetComponent<AudioSource>().clip = songs[x];
        gameObject.GetComponent<AudioSource>().loop = true;
        gameObject.GetComponent<AudioSource>().Play();
        if (x == 9)
        {
            gameObject.GetComponent<AudioSource>().loop = false;
        }
    }

}
