using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;  // Assign in Inspector
    public Button soundButton; // Assign in Inspector
    public AudioSource backgroundMusic; // Assign AudioSource in Inspector
    public Image soundButtonImage; // Assign Image component of Sound button in Inspector
    public Sprite soundOnSprite; // Assign "Sound On" sprite in Inspector
    public Sprite soundOffSprite; // Assign "Sound Off" sprite in Inspector

    private bool isSoundOn;

    void Start()
    {
        // Load the last played level, defaulting to level 1
        int lastLevel = PlayerPrefs.GetInt("LastPlayedLevel", 1);

        // Ensure we don't go beyond available scenes
        if (lastLevel >= SceneManager.sceneCountInBuildSettings)
        {
            lastLevel = 1; // Reset to Level 1 if out of bounds
        }

        // Assign Play Button functionality
        playButton.onClick.AddListener(() => LoadLevel(lastLevel));

        // Load sound settings
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1; // Default to ON
        UpdateSoundState();

        // Assign Sound Button functionality
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

        // Update sound button sprite
        if (soundButtonImage != null)
        {
            soundButtonImage.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
        }
    }
}
