using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUISystem : MonoBehaviour 
{
    public GameObject _successPrompt;   // Reference to success text GUI element
    public GameObject _failedPrompt;    // Reference to failed text GUI element
    public GameObject _pausePrompt;     // Reference to pause text GUI element

    Player _player;                     // Reference to player in scene

    Image _healthGUI_image;             // Image which will blackout the screen
                                        // as player loses health

    bool _isPaused = false;             // Defines whether or not the game 
                                        // is currently paused
    bool isPaused
    {
        get
        {
            return _isPaused;
        }
    }

	void Start() 
    {
        _successPrompt.SetActive(false);
        _failedPrompt.SetActive(false);
        _pausePrompt.SetActive(false);

        _player             = GameObject.Find("Player").GetComponent<Player>();
        _healthGUI_image    = GameObject.Find("Health Indicator Overlay").GetComponent<Image>();
	}
	
	void Update()
    {
        RectTransform rectTransform = _healthGUI_image.rectTransform;
        rectTransform.position      = new Vector3(0.0f, 0.0f, 0.0f);
        rectTransform.sizeDelta     = new Vector2(Screen.width, Screen.height);

        if (!_isPaused)
        {
            if (Input.GetKeyDown(_player._pauseKey))
            {
                PauseGame();
            }
        }
	}

    public void EndLevel(bool a_success)
    {
        _player.isAlive = false;

        if (a_success)
        {
            _successPrompt.SetActive(true);
        }
        else
        {
            _failedPrompt.SetActive(true);
        }
    }

    public void ResetLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoadNextLevel()
    {
        int nextLevel       = Application.loadedLevel + 1;
        int levelSelection  = 0;

        if (Application.levelCount <= nextLevel)
        {
            Application.LoadLevel(nextLevel);
        }
        else
        {
            Application.LoadLevel(levelSelection);
        }
    }

    public void SwitchToLevelSelect()
    {
        Application.LoadLevel(0);
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        _isPaused = true;
        _pausePrompt.SetActive(true);

        Time.timeScale = 0.0f;
    }

    public void UnPauseGame()
    {
        _isPaused = false;
        _pausePrompt.SetActive(false);

        Time.timeScale = 1.0f;
    }
}
