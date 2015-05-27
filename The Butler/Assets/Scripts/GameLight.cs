using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class GameLight : MonoBehaviour 
{
    public ParticleSystem _particleSystem;  // Reference to particle system of light

    protected Light _light;                 // Reference to light component of game object
    protected SphereCollider _collider;     // Reference to collider component of game object
    protected Player _player;               // Reference to player within scene

    [Range(0.1f, 1.0f)]
    [Tooltip("Scalar percentage to offset the collider by.")]
    public float _colliderOffset = 1.0f;

    float _lightChangeRate = 8.0f;          // Rate at which the light will slowly dim/brighten
                                            // when the light activates or deactivates

    float _normalLightIntensity;

    [Tooltip("Whether or not this light is activated and will influence player.")]
    public bool _activated;
    bool _lastActivated_state = false;      // Used to denote when the light is either 
                                            // activating or deactivating

	protected virtual void Start() 
    {
        // Get components
        _light      = GetComponent<Light>();
        _collider   = GetComponent<SphereCollider>();

        // Find player within scene
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();

        // Update last activated state, for activation and deactivation
        _lastActivated_state = _activated;

        // Assign variables used for light state changing
        _normalLightIntensity = _light.intensity;
	}

	protected virtual void Update() 
    {
        // Update collider
        _collider.radius = (_light.range * _colliderOffset) * GetIntensityPercent();

        // Update particle system
        _particleSystem.enableEmission = _activated;

        if (_lastActivated_state != _activated)
        {
            HandleLightFade();
        }
	}

    float GetIntensityPercent()
    {
        if (_light.intensity == 0.0f)
        {
            return 0.0f;
        }

        return _light.intensity / _normalLightIntensity;
    }

    void HandleLightFade()
    {
        if (_activated)
        {
            if (_light.intensity >= _normalLightIntensity)
            {
                _light.intensity        = _normalLightIntensity;
                _lastActivated_state    = _activated;
            }

            _light.intensity += Time.deltaTime * _lightChangeRate;
        }
        else
        {
            if (_light.intensity <= 0)
            {
                _light.intensity        = 0.0f;
                _lastActivated_state    = _activated;
            }

            _light.intensity -= Time.deltaTime * _lightChangeRate;
        }
    }

    public void OnTriggerStay(Collider a_other)
    {
        // Ignore if not player or light isn't active
        if (a_other.tag != "Player")
        {
            return;
        }

        if (_activated)
        {
            // Player is under this light
            _player._isLit = true;

            // Increase player health
            _player.IncreaseHealth(Time.deltaTime);
        }
    }

    public void OnTriggerExit(Collider a_other)
    {
        // Ignore if not player or light isn't active
        if (a_other.tag != "Player")
        {
            return;
        }

        if (_activated)
        {
            // Player should now be outside of light
            _player._isLit = false;
        }
    }
}
