/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;

    #region Sigleton
    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<PlayerStats>();
            return instance;
        }
    }
    #endregion

    public int torches;

    [SerializeField]
    public float health;
    [SerializeField]
    public float maxHealth;
    [SerializeField]
    public float maxTotalHealth;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public float MaxTotalHealth { get { return maxTotalHealth; } }

    [SerializeField]
    public float DeafaultHealth = 5f;
    [SerializeField]
    public float DeafaultmaxHealth = 5f;
    [SerializeField]
    public float DeafaultmaxTotalHealth = 30f;

    private void Start()
    {
        readHealth();
        ClampHealth();
        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
        //health = PlayerPrefs.GetFloat("Health");
        //maxHealth = PlayerPrefs.GetFloat("Max Health");
        //maxTotalHealth = PlayerPrefs.GetFloat("Max Total Health");
    }

    public void resetHealth()
    {
        PlayerPrefs.SetFloat("Health", DeafaultHealth);
        PlayerPrefs.SetFloat("Max Health", DeafaultmaxHealth);
        PlayerPrefs.SetFloat("Max Total Health", DeafaultmaxTotalHealth);
        PlayerPrefs.SetInt("torches", 0);
        readHealth();
    }
    public void saveHealth()
    {
        PlayerPrefs.SetFloat("Health", Health);
        PlayerPrefs.SetFloat("Max Health", maxHealth);
        PlayerPrefs.SetFloat("Max Total Health", maxTotalHealth);
        PlayerPrefs.SetInt("torches", torches);
    }

    void readHealth()
    {
        if (PlayerPrefs.HasKey("Health"))
        {
            health = PlayerPrefs.GetFloat("Health");
            maxHealth = PlayerPrefs.GetFloat("Max Health");
            maxTotalHealth = PlayerPrefs.GetFloat("Max Total Health");
            torches = PlayerPrefs.GetInt("torches");
        }
        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }

    public void Heal(float health)
    {
        this.health += health;
        ClampHealth();
        saveHealth();
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        
        ClampHealth();
        saveHealth();
        if (health <= 0)
        {
            health = maxHealth;
            torches = 0;
            saveHealth();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddHealth()
    {
        if (maxHealth < maxTotalHealth)
        {
            maxHealth += 1;
            health = maxHealth;

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }
        saveHealth();
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();

        saveHealth();
    }

    public void growHealth()
    {
        if (torches >= 10)
        {
            torches = torches-10;
            AddHealth();
            gameObject.GetComponent<AudioSource>().Play();
            Heal(1f);
            saveHealth();
        }
    }

    public void startNewGame()
    {
        LevelManager._instance.loadLevel(9);
    }

    public void contGame()
    {
        if (PlayerPrefs.HasKey("LastSceneInt"))
        {
            LevelManager._instance.loadLevel(PlayerPrefs.GetInt("LastSceneInt"));
        }
        else
        {
            LevelManager._instance.loadLevel(0);
        }
    }
}
