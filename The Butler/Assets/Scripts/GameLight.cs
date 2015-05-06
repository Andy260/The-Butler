using UnityEngine;
using System.Collections;

public class GameLight : MonoBehaviour 
{
    public Color _activateColour;
    Color _normalColour;

    Light _light;
    SphereCollider _collider;
    Player _player;

	void Start() 
    {
        _light      = GetComponent<Light>();
        _collider   = GetComponent<SphereCollider>();

        _player = GameObject.FindWithTag("Player").GetComponent<Player>();

        _normalColour = _light.color;
	}
	
	void Update() 
    {
        _collider.radius = _light.range;
	}

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        _player._isLit = true;

        _light.color = _activateColour;
    }

    // OnTriggerExit is called when the Collider other has stopped touching the trigger
    void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            return;

        _player._isLit = false;

        _light.color = _normalColour;
    }
}
