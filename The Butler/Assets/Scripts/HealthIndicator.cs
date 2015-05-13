using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthIndicator : MonoBehaviour 
{
    Light _dirLight;            // Reference to directional light
    Player _player;             // Reference to player within scene
    Image _healthGUIOverlay;     // Reference to health GUI overlay texture

    float _normalIntensity;     // Stores original intensity of directional light

	void Start() 
    {
        _dirLight = GetComponent<Light>();
        _player = GameObject.Find("Player").GetComponent<Player>();

        _healthGUIOverlay = GameObject.Find("Health Indicator Overlay").GetComponent<Image>();

        _normalIntensity = _dirLight.intensity;
	}

    void Update()
    {
        float playerHealth = _player.GetHealthPercent();

        // Update directional light intensity to 
        // be in sync with player health percentage
        _dirLight.intensity = playerHealth * _normalIntensity;

        // Update health GUI element with current player percentage
        Color colour    = _healthGUIOverlay.color;
        colour.a = 1.0f - playerHealth;

        _healthGUIOverlay.color = colour;
    }
}
