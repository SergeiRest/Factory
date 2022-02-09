using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
	[SerializeField] private Transform _emptyPosition;
	private int _capacity = 10;
	private int _countThings = 0;

	private List<AbstractThing> _things = new List<AbstractThing>();

	public int Capacity => _capacity;
	public int CountThings => _countThings;
	public Vector3 EmptyPosition => _emptyPosition.position;

	public void AddThing(AbstractThing thing)
	{
		_capacity++;
		_things.Add(thing);
	}
}
