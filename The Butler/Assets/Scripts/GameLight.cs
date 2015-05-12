using UnityEngine;
using System.Collections;

public class GameLight : MonoBehaviour 
{
    [Tooltip("Light's colour will change to this colour when within the influence of this light.")]
    public Color _activateColour;
    protected Color _normalColour;

    protected Light _light;
    protected SphereCollider _collider;
    protected Player _player;

	protected virtual void Start() 
    {
#if UNITY_EDITOR
        if (_activateColour.r == 0.0f &&
            _activateColour.g == 0.0f &&
            _activateColour.b == 0.0f)
        {
            Debug.LogError("Light (" + transform.name + 
                ") activation colour will be black with current settings");
        }
#endif

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
        _collider.radius = _light.range;
	}

    public void OnTriggerStay(Collider a_other)
    {
        // Ignore if not player
        if (a_other.tag != "Player")
        {
            return;
        }

        // Player is under this light
        _player._isLit = true;

        // Increase player health
        _player.IncreaseHealth(Time.deltaTime);

        // Change colour to activation colour
        _light.color = _activateColour;
    }

    public void OnTriggerExit(Collider a_other)
    {
        // Ignore if not player
        if (a_other.tag != "Player")
        {
            return;
        }

        // Player should now be outside of light
        _player._isLit = false;

        // Reset light colour
        _light.color = _normalColour;
    }
}
