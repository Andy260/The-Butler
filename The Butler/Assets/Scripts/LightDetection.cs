using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightDetection : MonoBehaviour 
{
    Player _player;
    List<Light> _lights;
    List<SphereCollider> _lightColliders;

	void Start() 
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // Get all lighta within scene which are interacts with the player
        GameObject[] lightsArray = GameObject.FindGameObjectsWithTag("Interactable Light");
        _lights = new List<Light>(lightsArray.Length);

        for (int i = 0; i < _lights.Count; ++i)
        {
            _lights.Add(lightsArray[i].GetComponent<Light>());
        }

        // Get all light colliders
        _lightColliders = new List<SphereCollider>(lightsArray.Length);
        for (int i = 0; i < _lightColliders.Count; ++i)
        {
            _lightColliders.Add(lightsArray[i].GetComponent<SphereCollider>());

            // Ensure collider is size of light range
            _lightColliders[i].radius = _lights[i].range;
        }

        // TODO: Ensure all lights have colliders
	}
	
	void Update() 
    {
	    
	}

    public void OnCollisionEnter(Collision collision)
    {

    }

    public void OnCollisionExit(Collision collision)
    {

    }
}
