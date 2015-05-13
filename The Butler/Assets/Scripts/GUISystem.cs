using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUISystem : MonoBehaviour 
{
    public GameObject _successPrompt;   // Reference to success text GUI element
    public GameObject _failedPrompt;    // Reference to failed text GUI element

    Player _player;

	void Start() 
    {
        _successPrompt.SetActive(false);
        _failedPrompt.SetActive(false);

        _player = GameObject.Find("Player").GetComponent<Player>();
	}
	
	void Update()
    {

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

    public void ChangeScene(int a_scene)
    {
        Application.LoadLevel(a_scene);
    }
}
