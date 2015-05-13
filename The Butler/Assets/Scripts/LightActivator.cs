﻿using UnityEngine;
using System.Collections;

public class LightActivator : MonoBehaviour 
{
    [Tooltip("Which light this activator will be influencing.")]
    public GameLight _light;

    Player _player;                 // Reference to player in scene
    SphereCollider _sphereCollider; // Reference to sphere collider

	void Start() 
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _sphereCollider = GetComponent<SphereCollider>();
	}

	void Update() 
    {
	    
	}

    public void OnTriggerStay(Collider a_other)
    {
        // Ignore if not player
        if (a_other.name != "Player")
        {
            return;
        }

        if (Input.GetKeyDown(_player._activateKey))
        {
            _light._activated = !_light._activated;
        }
    }
}
