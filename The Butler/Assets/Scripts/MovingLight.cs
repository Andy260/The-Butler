using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingLight : GameLight 
{
    List<Vector3> _moveToPositions;
    public bool _loopMovement;

    int _positionItr    = 0;

    float _startTime;
    float _journeyLength;
    public float _speed = 1.0f;
    Vector3 _startMarker;
    Vector3 _endMarker;

	protected override void Start() 
    {
        _moveToPositions = new List<Vector3>(transform.childCount);

        for (int i = 0; i < transform.childCount; ++i)
        {
            Vector3 position = transform.GetChild(i).position;
            _moveToPositions.Add(position);
        }

        _startTime = Time.time;
        _startMarker = transform.position;
        _endMarker = _moveToPositions[_positionItr];
        _journeyLength = Vector3.Distance(_startMarker, _endMarker);

        base.Start();
	}
	
	protected override void Update() 
    {
        if (Vector3.Distance(transform.position, _moveToPositions[_positionItr]) <= 0.0f)
        {
            if (_positionItr >= _moveToPositions.Count)
                _positionItr = 0;
            else
                _positionItr++;

            _startTime      = Time.time;
            _startMarker    = transform.position;
            _endMarker      = _moveToPositions[_positionItr];
            _journeyLength  = Vector3.Distance(_startMarker, _endMarker);
        }

        // Move to new position
        float distCovered   = (Time.time - _startTime) * _speed;
        float fracJourney   = distCovered / _journeyLength;

        Vector3 lerp = Vector3.Lerp(_startMarker, _endMarker, fracJourney);
        transform.position  = lerp;

        base.Update();
	}

    void MoveToPosition()
    {

    }
}
