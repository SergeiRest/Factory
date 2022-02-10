using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
	[SerializeField] private Transform _emptyPosition;
	private int _capacity = 5;
	private int _countThings = 0;

	private List<AbstractThing> _things = new List<AbstractThing>();

	public int Capacity => _capacity;
	public int CountThings => _countThings;
	public Vector3 EmptyPosition => _emptyPosition.position;

	public delegate void OnPositionAppeared();
	public OnPositionAppeared PositionAppeared;
	public delegate void StopFilling();
	public StopFilling Stop;

	private void Start()
	{
		_emptyPosition.localPosition = new Vector3(0, 1.5f, 0);
	}

	public void AddThing(AbstractThing thing)
	{
		_countThings++;
		_things.Add(thing);

		Vector3 newPosition = new Vector3(_emptyPosition.position.x, _emptyPosition.position.y + thing.transform.localScale.y, _emptyPosition.position.z);
		_emptyPosition.position = newPosition;
	}

	public AbstractThing RemoveThing()
	{
		AbstractThing current = _things[_countThings - 1];
		_countThings--;
		_things.Remove(current);
		Vector3 newPosition = new Vector3(_emptyPosition.position.x, _emptyPosition.position.y - current.transform.localScale.y, _emptyPosition.position.z);
		_emptyPosition.position = newPosition;
		return current;
	}
}
