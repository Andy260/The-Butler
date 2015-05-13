using UnityEngine;
using System.Collections;

public class ExitNode : MonoBehaviour
{
    GUISystem _guiSystem;

	void Start() 
    {
        _guiSystem = GameObject.Find("GUI System").GetComponent<GUISystem>();
	}
	
	void Update() 
    {
	    
	}

    public void OnTriggerEnter(Collider other)
    {
        _guiSystem.EndLevel(true);
    }
}
