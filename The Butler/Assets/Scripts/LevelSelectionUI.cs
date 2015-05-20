using UnityEngine;
using System.Collections;

public class LevelSelectionUI : MonoBehaviour 
{

	void Start() 
    {
	    
	}
	
	void Update() 
    {
	    
	}

    public void LoadLevel(int a_scene)
    {
        Application.LoadLevel(a_scene);
    }
}
