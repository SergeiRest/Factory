using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _speed;
	private Vector3 _startPos;
	private Vector3 _delta;
	private bool _canMove = true;

	private void Update()
	{
		GetDelta();
	}

	private void GetDelta()
	{
		if (!_canMove)
			return;

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
		Quaternion look = Quaternion.LookRotation(new Vector3((Input.mousePosition.x - _startPos.x), 0, (Input.mousePosition.y - _startPos.y)));
		transform.rotation = look;
	}

	public void StartMove()
	{
		_canMove = true;
	}

	public void StopMove()
	{
		_canMove = false;
	}
}
