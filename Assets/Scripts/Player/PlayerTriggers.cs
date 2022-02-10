using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInventory))]
public class PlayerTriggers : MonoBehaviour
{
	[SerializeField] private PlayerMovement _playerMovement;
	[SerializeField] private PlayerInventory _inventory;

	private void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent(out Storage storage))
		{
			_playerMovement.StopMove();
			FillingInventory(storage);
		}
	}

	private async void FillingInventory(Storage storage)
	{
		if (_inventory.GetAvailableForAdding() && storage.CountThings > 0)
		{
			storage.Stop?.Invoke();
			AbstractThing thing = storage.RemoveThing();
			_inventory.AddThing(thing);
			thing.Move(thing.transform.position, _inventory.AvailablePosition);
			await System.Threading.Tasks.Task.Delay(2000);
			FillingInventory(storage);
		}
		else
		{
			Debug.Log("Отпустили");
			_playerMovement.StartMove();
			storage.PositionAppeared?.Invoke();
		}
			

		
	}
}
