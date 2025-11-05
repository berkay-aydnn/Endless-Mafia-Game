using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
public class CoinScore : MonoBehaviour
{
    public static CoinScore instance;
    public TMP_Text scoreBoard;
    private int score = 0;
    void Start()
    {
        if (scoreBoard == null)
        {
            GameObject go = GameObject.FindWithTag("Score");
            if (go != null)
            {
                scoreBoard = go.GetComponent<TMP_Text>();
            }
        }
        if (scoreBoard != null)
        {
            scoreBoard.text = "Score: " + score;
        }
    }
    void Awake()
    {
        if (instance == null) {
            instance = this;
            if (transform.parent != null)
            {
                transform.SetParent(null, true); // Kök yap ve Canvas'tan ayır
            }
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scoreBoard == null)
        {
            GameObject go = GameObject.FindWithTag("Score");
            if (go != null)
            {
                scoreBoard = go.GetComponent<TMP_Text>();
            }
        }
        if (scoreBoard != null)
        {
            scoreBoard.text = "Score: " + score;
        }
    }
    public void AddScore(int amount)
    {
        if (score == 0 && scoreBoard != null)
        {
            scoreBoard.text = "Score: 0";
        }
        score += amount;
        if (scoreBoard != null)
        {
            scoreBoard.text = "Score: " + score;
        }
    }
    public void resetScore()
    {
        score = 0;
        if (scoreBoard != null)
        {
            scoreBoard.text = "Score: 0";
        }
    }
}
