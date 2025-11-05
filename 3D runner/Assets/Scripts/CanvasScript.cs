using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject gameCanvas;


    private bool isPaused = false;

    void Start()
    {
        mainMenuCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 0f;
    }
    public void StartGame()
    {
        mainMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        if (isPaused) return;

        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuCanvas.SetActive(true);
    }

    public void ContinueGame()
    {
        StartCoroutine(ContinueCoroutine());
    }

    IEnumerator ContinueCoroutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
    }


    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
    }public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif        
    }
}
