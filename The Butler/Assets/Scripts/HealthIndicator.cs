using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthIndicator : MonoBehaviour 
{
    Light _dirLight;                // Reference to directional light
    Player _player;                 // Reference to player within scene
    Image _blackoutGUI;             // Reference to health GUI overlay texture

    float _normalIntensity;         // Stores original intensity of directional light

    RectTransform _backoutRect;     // Rectangle transform of blackout GUI element

	void Start()
    {
        // Get reference of player in scene
        GameObject gameObject   = GameObject.Find("Player");
        _player                 = gameObject.GetComponent<Player>();

        // Get blackout GUI references and initialise it
        gameObject              = GameObject.Find("Health Indicator Overlay");
        _blackoutGUI            = gameObject.GetComponent<Image>();
        _backoutRect            = gameObject.GetComponent<Image>().rectTransform;
        _backoutRect.position   = new Vector3(0.0f, 0.0f, 0.0f);

        // Initiailise directional light
        _dirLight               = GetComponent<Light>();
        _normalIntensity        = _dirLight.intensity;
	}

    void Update()
    {
        float playerHealth      = _player.GetHealthPercent();

        // Update directional light intensity to 
        // be in sync with player health percentage
        _dirLight.intensity     = playerHealth * _normalIntensity;

        // Update health GUI element with current player percentage
        Color colour            = _blackoutGUI.color;
        colour.a                = (1.0f - playerHealth);

        // Update blackout GUI element
        _backoutRect.sizeDelta  = new Vector2(Screen.width, Screen.height);
        _blackoutGUI.color      = colour;
    }
}
