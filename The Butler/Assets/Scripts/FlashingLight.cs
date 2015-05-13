using UnityEngine;
using System.Collections;

public class FlashingLight : GameLight 
{
    [Tooltip("How long the light will remain in its current state before turning off/on, in seconds. (Affects on and off period)")]
    public float _flashRate;
    float _currentFlashTime;

	protected override void Start()
    {
        // Initialise timer
        _currentFlashTime = _flashRate; 

        base.Start();
	}

    protected override void Update()
    {
        if (_currentFlashTime <= 0.0f)
        {
            // Toggle light state and reset timer
            _activated = !_activated;
            _currentFlashTime = _flashRate;

            Debug.Log("Light state toggled...");
        }
        else
        {
            // Tick timer
            _currentFlashTime -= Time.deltaTime;
        }

        base.Update();
	}
}
