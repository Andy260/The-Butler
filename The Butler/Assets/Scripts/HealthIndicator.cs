using UnityEngine;
using System.Collections;

public class HealthIndicator : MonoBehaviour 
{
    Light _dirLight;    // Reference to directional light
    Player _player;     // Reference to player within scene

    float _normalIntensity;

	void Start() 
    {
        _dirLight = GetComponent<Light>();
        _player = GameObject.Find("Player").GetComponent<Player>();

        _normalIntensity = _dirLight.intensity;
	}

    void Update()
    {
        // Update directional light intensity to 
        // be in sync with player health percentage
        _dirLight.intensity = _player.GetHealthPercent() * _normalIntensity;
    }
}
