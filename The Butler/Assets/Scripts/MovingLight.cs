using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingLight : GameLight 
{
    [Tooltip("Should the light loop movement list, or stop after last node?")]
    public bool _loopMovement;
    [Tooltip("How fast this light will move, in units per second")]
    public float _speed;
	
	[Tooltip("Draws lines between the light and each of it's movement nodes, to visualise the projected movement path during gameplay.")]
	public bool _drawPath = false;

    [Tooltip("List of transforms this light will move to (doesn't update transform position when moving to next node)")]
    public List<Transform> _moveToPositions = new List<Transform>();

    int _positionItr;   // Which node we are at within the tranforms list

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

		if (Application.isPlaying) 
		{
			_startTime 		= Time.time;
			_startMarker 	= transform.position;
			_endMarker 		= _moveToPositions [_positionItr].position;
			_journeyLength 	= Vector3.Distance (_startMarker, _endMarker);
		}

        base.Start();
	}
	
	protected override void Update() 
    {
		if (Application.isPlaying) 
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
		}
		
#if UNITY_EDITOR
		if (_drawPath &&
		    _moveToPositions != null && 
		    _moveToPositions.Count > 0)
		{
			for (int i = 0; i < _moveToPositions.Count; ++i)
			{
				Vector3 startPos;
				Vector3 endPos = _moveToPositions[i].position;

				if (i == 0)
				{
					startPos = _moveToPositions[_moveToPositions.Count - 1].position;
				}
				else
				{
					startPos = _moveToPositions[i -1].position;
				}

				Debug.DrawLine(startPos, endPos);
			}
		}
#endif

        base.Update();
	}

    void MoveToNextNode()
    {
        // Move to new position
        float distCovered 	= (Time.time - _startTime) * _speed;
        float fracJourney 	= distCovered / _journeyLength;

        Vector3 lerp 		= Vector3.Lerp(_startMarker, _endMarker, fracJourney);
        transform.position 	= lerp;
    }
}
