using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
    public bool _isLit;
    List<Light> _lights;    // Lights influencing player

	void Start() 
    {
        GameObject[] lightsArray = GameObject.FindGameObjectsWithTag("Interactable Lights");
        _lights = new List<Light>(lightsArray.Length);
	}
	
	void Update() 
    {
	    
	}
}
