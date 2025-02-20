using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject levelComplete;

    void Start()
    {
        levelComplete.SetActive(false);
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        levelComplete.SetActive(true);
        Time.timeScale = 0f;

        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("LastLevel", nextLevel);
        PlayerPrefs.Save();
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        int lastLevel = PlayerPrefs.GetInt("LastLevel", 1);
        SceneManager.LoadScene(lastLevel);
    }

    
}
