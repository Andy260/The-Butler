using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
    [Tooltip("Defines whether the player is being lit by light or not.")]
    [ReadOnly]
    public bool _isLit;

    [Tooltip("Speed in units per second.")]
    public float _speed;

    [Tooltip("Health which the player will spawn with. Represents seconds the player can surivive outside of light.")]
    public float _maxHealth;

    [ReadOnly]
    [Tooltip("Current Health. Represents seconds the player can surivive outside of light.")]
    public float _currentHealth;

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
        _currentHealth  = _maxHealth;
	}

    void Update()
    {
        // Deduct health if not within light
        if (!_isLit)
        {
            _currentHealth -= Time.deltaTime;
        }

        // Reset level if player deemed dead
        if (_currentHealth <= 0.0f)
        {
            Application.LoadLevel(0);
        }

        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 direction = new Vector3();

        // Get movement direction
        if (Input.GetKey(KeyCode.A))
        {
            direction.x -= 1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x += 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.z -= 1.0f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            direction.z += 1.0f;
        }

        // Apply translation
        transform.Translate(direction * (_speed * Time.deltaTime));
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
}