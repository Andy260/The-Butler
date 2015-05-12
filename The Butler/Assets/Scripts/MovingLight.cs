using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingLight : GameLight 
{
    [Tooltip("Should the light loop movement list, or stop after last node?")]
    public bool _loopMovement;
    [Tooltip("How fast this light will move, in units per second")]
    public float _speed;
    [Tooltip("List of transforms this light will move to (doesn't update transform position when moving to next node)")]
    public List<Transform> _moveToPositions;
    
    int _positionItr = 0;   // Which node we are at within the tranforms list

    // Timing variables
    float _startTime;
    float _journeyLength;
    
    // Movement markers
    Vector3 _startMarker;
    Vector3 _endMarker;

	protected override void Start() 
    {
#if UNITY_EDITOR
        bool errorsCaught = false;

        if (_speed <= 0)
        {
            Debug.LogError("Moving Light (" + transform.name + ") speed cannot be less than 1");
            errorsCaught = true;
        }

        if (!(_moveToPositions.Count > 0))
        {
            Debug.LogError("Moving Light (" + transform.name + 
                ") must have at least one movement node attached to it!");
            errorsCaught = true;
        }

        if (errorsCaught)
        {
            Debug.Break();
        }
#endif

        _startTime      = Time.time;
        _startMarker    = transform.position;
        _endMarker      = _moveToPositions[_positionItr].position;
        _journeyLength  = Vector3.Distance(_startMarker, _endMarker);

        base.Start();
	}
	
	protected override void Update() 
    {
        if (Vector3.Distance(transform.position, _moveToPositions[_positionItr].position) <= 0.0f)
        {
            if (_positionItr >= _moveToPositions.Count - 1)
            {
                if (_loopMovement)
                {
                    _positionItr = 0;
                }
            }
            else
            {
                _positionItr++;
            }

            _startTime      = Time.time;
            _startMarker    = transform.position;
            _endMarker      = _moveToPositions[_positionItr].position;
            _journeyLength  = Vector3.Distance(_startMarker, _endMarker);
        }

        if (_startMarker != _endMarker)
        {
            MoveToNextNode();
        }

        base.Update();
	}

    void MoveToNextNode()
    {
        // Move to new position
        float distCovered = (Time.time - _startTime) * _speed;
        float fracJourney = distCovered / _journeyLength;

        Vector3 lerp = Vector3.Lerp(_startMarker, _endMarker, fracJourney);
        transform.position = lerp;
    }
}
