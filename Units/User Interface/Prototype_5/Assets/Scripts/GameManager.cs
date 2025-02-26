using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public List<GameObject> targets;
    
    private float spawnRate = 1.0f;
    private int score;
    private float gameTime = 60f; // Game duration in seconds
    private int highScore = 0;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI highScoreText;
    
    public Button restartButton;
    public Button resumeButton;
    public Button quitButton;
    public GameObject titleScreen;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public AudioSource backgroundMusic;
    public Slider musicSlider;
    
    private bool isPaused = false;

    void Awake()
    {
        
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = 1.0f;
            backgroundMusic.Play();
        }
    }

    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        timerText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        
        if (musicSlider != null)
        {
            musicSlider.onValueChanged.RemoveAllListeners();
            musicSlider.value = 1.0f;
            backgroundMusic.volume = 1.0f; // Ensure music volume is set correctly
            musicSlider.onValueChanged.AddListener(AdjustMusicVolume);
        }
        
        // Load high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenu.activeSelf)
            {
                CloseSettings();
            }
            else
            {
                TogglePause();
            }
        }
    }
    
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        
        // Check and update high score
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        
        highScoreText.text = "High Score: " + highScore;
        highScoreText.gameObject.SetActive(true);
    }
    
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    
    public void RestartGame()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        
        StartCoroutine(SpawnTarget());
        StartCoroutine(GameTimer());
        
        score = 0;
        UpdateScore(0);
        
        titleScreen.gameObject.SetActive(false);
        timerText.gameObject.SetActive(true);
    }
    
    IEnumerator GameTimer()
    {
        while (gameTime > 0 && isGameActive)
        {
            gameTime--;
            timerText.text = "Time: " + gameTime;
            yield return new WaitForSeconds(1f);
        }
        GameOver();
    }
    
    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }
    
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }
    
    public void AdjustMusicVolume(float volume)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = volume;
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
