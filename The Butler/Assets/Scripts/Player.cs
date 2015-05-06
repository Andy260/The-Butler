using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
    public bool _isLit;
    public float _speed;

	void Start() 
    {
        
	}

    void Update()
    {
        Vector3 direction = new Vector3();

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

        transform.Translate(direction * (_speed * Time.deltaTime));
    }
}