using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	[SerializeField] private Transform _inventoryPlace;
	private int _countThings = 0;
	private int _maxThings = 5;

	private List<AbstractThing> _things = new List<AbstractThing>();

	public int Count => _countThings;

	public Vector3 AvailablePosition => _inventoryPlace.position;
	public List<AbstractThing> Things => _things;

	public void AddThing(AbstractThing item)
	{
		_countThings++;
		_things.Add(item);
		Vector3 newPosition = new Vector3(_inventoryPlace.localPosition.x, _inventoryPlace.localPosition.y + item.transform.localScale.y, _inventoryPlace.localPosition.z);
		_inventoryPlace.localPosition = newPosition;
		item.transform.SetParent(this.transform);
		item.transform.localRotation = Quaternion.Euler(0, 180, 0);
	}

	public AbstractThing GetThing()
	{
		return _things[_things.Count - 1];
	}

	public void RemoveThing(AbstractThing thing)
	{
		_things.Remove(thing);
		thing.transform.SetParent(null);
		_countThings--;
		Vector3 newPosition = new Vector3(_inventoryPlace.localPosition.x, _inventoryPlace.localPosition.y - thing.transform.localScale.y, _inventoryPlace.localPosition.z);
		_inventoryPlace.localPosition = newPosition;
	}

	public bool GetAvailableForAdding()
	{
		if (_countThings < _maxThings)
			return true;
		else
			return false;
	}
}
