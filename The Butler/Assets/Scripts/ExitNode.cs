using UnityEngine;
using System.Collections;

public class ExitNode : MonoBehaviour
{
    [Tooltip("Scene to change to upon player success.")]
    public int _scene;

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
