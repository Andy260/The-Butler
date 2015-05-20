using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUISystem : MonoBehaviour 
{
    public GameObject _successPrompt;   // Reference to success text GUI element
    public GameObject _failedPrompt;    // Reference to failed text GUI element

    Player _player;

    Image _healthGUI_image;

	void Start() 
    {
        _successPrompt.SetActive(false);
        _failedPrompt.SetActive(false);

        _player             = GameObject.Find("Player").GetComponent<Player>();
        _healthGUI_image    = GameObject.Find("Health Indicator Overlay").GetComponent<Image>();
	}
	
	void Update()
    {
        RectTransform rectTransform = _healthGUI_image.rectTransform;
        rectTransform.position      = new Vector3(0.0f, 0.0f, 0.0f);
        rectTransform.sizeDelta     = new Vector2(Screen.width, Screen.height);
	}

    public void EndLevel(bool a_success)
    {
        _player.canMove = false;

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
        Application.LoadLevel(Application.loadedLevel + 1);
    }
}
