using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
public class CoinScore : MonoBehaviour
{
    public static CoinScore instance;
    public TMP_Text scoreBoard;
    private int score = 0;
    void Start()
    {
        resetScore();
    }
    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AddScore(1);    
        }
    }
    public void AddScore(int amount)
    {
        if (score == 0)
        {
            scoreBoard.text = "Score: 0";
        }
        score += amount;
        scoreBoard.text = "Score: " + score;
    }
    public void resetScore()
    {
        score = 0;
        scoreBoard.text = "Score: 0";
    }
}
