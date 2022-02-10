using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	[SerializeField] private Transform _inventoryPlace;
	private int _countThings = 0;
	private int _maxThings = 5;

	private List<AbstractThing> _things = new List<AbstractThing>();

	public Vector3 AvailablePosition => _inventoryPlace.position;

	public void AddThing(AbstractThing item)
	{
		_countThings++;
		_things.Add(item);
		Vector3 newPosition = new Vector3(_inventoryPlace.position.x, _inventoryPlace.position.y + item.transform.localScale.y, _inventoryPlace.position.z);
		_inventoryPlace.position = newPosition;
		item.transform.SetParent(this.transform);
	}

	public bool GetAvailableForAdding()
	{
		if (_countThings < _maxThings)
			return true;
		else
			return false;
	}

	public void ReBuild()
	{
		foreach(var el in _things)
		{
			el.transform.SetParent(_inventoryPlace);
		}
	}
}
