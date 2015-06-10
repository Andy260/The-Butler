using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
    [Tooltip("Defines whether the player is being lit by light or not.")]
#if UNITY_EDITOR
    [ReadOnly]
#endif
    public bool _isLit;

    [Tooltip("Speed in units per second.")]
    public float _speed;

    [Tooltip("Health which the player will spawn with. Represents seconds the player can surivive outside of light.")]
    public float _maxHealth;

#if UNITY_EDITOR
    [ReadOnly]
#endif
    [Tooltip("Current Health. Represents seconds the player can surivive outside of light.")]
    public float _currentHealth;
    [Tooltip("Maximum speed the player will rotate towards movement direction, in degrees per-second.")]
    public float _maxTurnSpeed;

    [Tooltip("Key used for controlling forward movement.")]
    public KeyCode _moveForwardKey  = KeyCode.UpArrow;
    [Tooltip("Key used for controlling backward movement.")]
    public KeyCode _moveBackwardKey = KeyCode.DownArrow;
    [Tooltip("Key used for controlling left movement.")]
    public KeyCode _moveLeftKey     = KeyCode.LeftArrow;
    [Tooltip("Key used for controlling right movement.")]
    public KeyCode _moveRightKey    = KeyCode.RightArrow;
    [Tooltip("Key used for activating objects.")]
    public KeyCode _activateKey     = KeyCode.Space;
    [Tooltip("Key used for pausing and unpausing the game.")]
    public KeyCode _pauseKey        = KeyCode.Escape;

    GUISystem _guiSystem;   // Reference to GUI System game object
    Vector3 _moveDir;        // Current movement direction

    public bool isAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
        }
    }
    bool _isAlive;

	void Start() 
    {
#if UNITY_EDITOR
        bool errorsCaught = false;

        if (_speed <= 0.0f)
        {
            Debug.LogError("Player speed cannot be less than 1");
            errorsCaught = true;
        }

        if (_maxHealth <= 0.0f)
        {
            Debug.LogError("Player maximum health cannot be less than 1");
            errorsCaught = true;
        }

        if (errorsCaught)
        {
            Debug.Break();
        }
#endif

        _isLit          = false;
        _isAlive        = true;
        _currentHealth  = _maxHealth;
        _moveDir        = new Vector3();

        _guiSystem = GameObject.Find("GUI System").GetComponent<GUISystem>();
	}

    void Update()
    {
        // Deduct health if not within light
        if (!_isLit)
        {
            _currentHealth -= Time.deltaTime;
        }

        // End level if player health too low
        if (_currentHealth <= 0.0f)
        {
            _guiSystem.EndLevel(false);
        }

        // Update player movement
        if (isAlive)
        {
            HandleMovement();
        }

        // Placeholder for testing...
        _isAlive    = true;
        _isLit      = true;
    }

    void HandleMovement()
    {
        _moveDir        = new Vector3();
        bool isMoveing  = false;

        // Get movement direction
        if (Input.GetKey(_moveLeftKey))
        {
            _moveDir.x -= 1.0f;
            isMoveing   = true;
        }
        if (Input.GetKey(_moveRightKey))
        {
            _moveDir.x += 1.0f;
            isMoveing   = true;
        }
        if (Input.GetKey(_moveBackwardKey))
        {
            _moveDir.z -= 1.0f;
            isMoveing   = true;
        }
        if (Input.GetKey(_moveForwardKey))
        {
            _moveDir.z += 1.0f;
            isMoveing   = true;
        }

        // Apply translation
        transform.position += _moveDir * (_speed * Time.deltaTime);

        // Only rotate player if moving
        if (isMoveing)
        {
            Quaternion rotationQuat = Quaternion.LookRotation(_moveDir);

            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                rotationQuat, _maxTurnSpeed * Time.deltaTime);
        }
    }

    public void IncreaseHealth(float a_value)
    {
        // Clamp health increase if needed, and increase
        if (_currentHealth + a_value > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        else
        {
            _currentHealth += a_value;
        }
    }

    public float GetHealthPercent()
    {
        return _currentHealth / _maxHealth;
    }
}