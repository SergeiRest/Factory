using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialStorage : MonoBehaviour
{
	[SerializeField] private ThingType _neccesaryType;
	[SerializeField] private Transform _availablePlace;
	private int _capacity = 5;

	private List<AbstractThing> _things = new List<AbstractThing>();

	public int Capacity => _capacity;
	public int Count => _things.Count;

	public ThingType NeccesaryType => _neccesaryType;

	public delegate void CapacityChanged(MaterialStorage storage);
	public CapacityChanged OnCapacityChanged;

	public async void AddItem(AbstractThing thing)
	{
		_things.Add(thing);
		thing.Move(thing.transform.position, _availablePlace.position);
		_availablePlace.position = new Vector3(_availablePlace.position.x, _availablePlace.position.y + thing.transform.localScale.y, _availablePlace.position.z);
		thing.transform.eulerAngles = new Vector3(0, 90, 0);
		await System.Threading.Tasks.Task.Delay(2000);
	}

	public void RemoveThing()
	{
		AbstractThing removingThing = _things[Count - 1];
		_things.Remove(removingThing);
		Destroy(removingThing.gameObject);
	}
}
