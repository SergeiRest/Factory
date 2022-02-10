using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BigFactory : FactoryEntity
{
	[SerializeField] private MaterialStorage[] _materialStorage;

	private List<MaterialStorage> _emptyStorages = new List<MaterialStorage>();
	private List<MaterialStorage> _storagesWithThings = new List<MaterialStorage>();
	private string neccessaryThings = "Необходимо:";

	private bool _isWorking = false;
	public override int ProductionSpeed => 5;

	protected override void Start()
	{
		foreach(var storage in _materialStorage)
		{
			_emptyStorages.Add(storage);
			neccessaryThings += $" {storage.NeccesaryType}";
			storage.OnCapacityChanged += RemoveFomEmptyList;
		}
		foreach (var storage in _materialStorage)
		{
			_storagesWithThings.Add(storage);
		}
		OnFactoryStopped?.Invoke(neccessaryThings);
		base.Start();
		neccessaryThings = "Необходимо:";
	}

	public override void Product()
	{
		if (_emptyStorages.Count == 0)
		{
			OnFactoryReboot?.Invoke("");
			for (int i = 0; i < _storagesWithThings.Count; i++)
			{
				_storagesWithThings[i].RemoveThing();
				if (_storagesWithThings[i].Count <= 0)
				{
					_emptyStorages.Add(_storagesWithThings[i]);
					neccessaryThings += $" {_storagesWithThings[i].NeccesaryType}";
				}
			}
			base.Product();
		}
		else
			OnFactoryStopped?.Invoke(neccessaryThings);
	}

	private void MaterialStorageChanged(MaterialStorage storage)
	{
		_isWorking = true;
		Product();
	}

	private void RemoveFomEmptyList(MaterialStorage storage)
	{
		MaterialStorage current = null;
		for(int i = 0; i < _emptyStorages.Count; i++)
		{
			if(i == 0)
			{
				neccessaryThings = "Необходимо";
			}
			if (storage.NeccesaryType != _emptyStorages[i].NeccesaryType)
			{
				neccessaryThings += $" {_emptyStorages[i].NeccesaryType}";
				if(i+ 1 != _emptyStorages.Count)
					continue;
			}
			current = _emptyStorages[i];
				
			if (i + 1 == _emptyStorages.Count)
			{
				_emptyStorages.Remove(current);
				Product();
			}
		}
	}
}
