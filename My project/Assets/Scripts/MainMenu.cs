using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button soundButton;
    public AudioSource backgroundMusic;
    public Image soundButtonImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private bool isSoundOn;

    void Start()
    {
        // Load the last played level (default to 1 if not set)
        int lastLevel = PlayerPrefs.GetInt("LastPlayedLevel", 1);

        // Ensure we don't load a non-existing scene
        if (lastLevel >= SceneManager.sceneCountInBuildSettings)
        {
            lastLevel = 1; // Reset to Level 1 if out of bounds
        }

        // Assign Play Button functionality
        playButton.onClick.AddListener(() => LoadLevel(lastLevel));

        // Load sound settings
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        UpdateSoundState();

        soundButton.onClick.AddListener(ToggleSound);
    }

    void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
        UpdateSoundState();
    }

    void UpdateSoundState()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.mute = !isSoundOn;
        }

        if (soundButtonImage != null)
        {
            soundButtonImage.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
        }
    }
}
