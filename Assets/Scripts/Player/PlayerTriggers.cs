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
		else if(other.TryGetComponent(out MaterialStorage materialStorage))
		{
			_playerMovement.StopMove();
			ClearingInventory(materialStorage);
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
			_playerMovement.StartMove();
			storage.PositionAppeared?.Invoke();
		}
	}

	private async void ClearingInventory(MaterialStorage storage)
	{
		if (storage.Count < storage.Capacity && _inventory.Count > 0)
		{
			Debug.Log(_inventory.Count);
			for(int i = 0; i < _inventory.Count; i++)
			{
				AbstractThing thing = _inventory.Things[i];
				if (thing.Type == storage.NeccesaryType)
				{
					_inventory.RemoveThing(thing);
					storage.AddItem(thing);
					await System.Threading.Tasks.Task.Delay(2000);
					ClearingInventory(storage);
				}
				else
				{
					_playerMovement.StartMove();
					break;
				}
					
			}
		}
		else
		{
			storage.OnCapacityChanged?.Invoke(storage);
			_playerMovement.StartMove();
		}
	}
}
