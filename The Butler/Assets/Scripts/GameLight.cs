using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GameLight : MonoBehaviour 
{
    protected Color _normalColour;      // Normal Colour of light

    protected Light _light;             // Reference to light component of game object
    protected SphereCollider _collider; // Reference to collider component of game object
    protected Player _player;           // Reference to player within scene

    [Range(0.1f, 1.0f)]
    [Tooltip("Scalar percentage to offset the collider by.")]
    public float _colliderOffset = 1.0f;

    [Tooltip("Whether or not this light is activated and will influence player.")]
    public bool _activated;

	protected virtual void Start() 
    {
        // Get components
        _light      = GetComponent<Light>();
        _collider   = GetComponent<SphereCollider>();

        // Find player within scene
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();

        // Save normal colour of light
        _normalColour = _light.color;
	}
	
	protected virtual void Update() 
    {
        _collider.radius = _light.range * _colliderOffset;

#if !UNITY_GAME
        _light.enabled = _activated;
#endif
	}

#if !UNITY_GAME
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

        // Reset light colour
        _light.color = _normalColour;
    }
#endif
}
