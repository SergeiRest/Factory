using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _speed;
	private Vector3 _startPos;
	private Vector3 _delta;

	private void Update()
	{
		GetDelta();
	}

	private void GetDelta()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_startPos = Input.mousePosition;
		}
		else if(Input.GetMouseButton(0))
		{
			_delta = (Input.mousePosition - _startPos).normalized;
			Move(_delta);
		}
	}

	private void Move(Vector3 delta)
	{
		transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(delta.x, 0, delta.y), _speed * Time.deltaTime);
	}
}
