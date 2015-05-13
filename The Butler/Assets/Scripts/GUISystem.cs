using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUISystem : MonoBehaviour 
{
    public GameObject _successPrompt;   // Reference to success text GUI element
    public GameObject _failedPrompt;    // Reference to failed text GUI element

	void Start() 
    {
        _successPrompt  = GameObject.Find("Success Prompt");
        _failedPrompt   = GameObject.Find("Failure Prompt");

        _successPrompt.SetActive(false);
        _failedPrompt.SetActive(false);
	}
	
	void Update()
    {

	}

    public void EndLevel(bool a_success)
    {
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
