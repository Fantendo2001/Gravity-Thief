using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class DoorToX : MonoBehaviour
{
    [SerializeField] int destinationSceneX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelManager._instance.loadLevel(destinationSceneX);
        }
    }
}
