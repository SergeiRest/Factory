using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BigFactory : FactoryEntity
{
	[SerializeField] private MaterialStorage[] _materialStorage;

	private List<MaterialStorage> _emptyStorages = new List<MaterialStorage>();
	private List<MaterialStorage> _storagesWithThings = new List<MaterialStorage>();

	private bool _isWorking = false;
	public override int ProductionSpeed => 5;

	protected override void Start()
	{
		foreach(var storage in _materialStorage)
		{
			_emptyStorages.Add(storage);
			storage.OnCapacityChanged += RemoveFomEmptyList;
		}
		base.Start();
	}

	public override void Product()
	{
		if(_emptyStorages.Count == 0)
		{
			Debug.Log("Можем начать");
			foreach (MaterialStorage materialStorage in _storagesWithThings)
			{
				materialStorage.RemoveThing();
				if(materialStorage.Count == 0)
				{
					_storagesWithThings.Remove(materialStorage);
					_emptyStorages.Add(materialStorage);
				}
			}
			base.Product();
		}
	}

	private void MaterialStorageChanged(MaterialStorage storage)
	{
		_isWorking = true;
		Product();
	}

	private void RemoveFomEmptyList(MaterialStorage storage)
	{
		for(int i = 0; i < _emptyStorages.Count; i++)
		{
			if (storage.NeccesaryType == _emptyStorages[i].NeccesaryType)
			{
				_storagesWithThings.Add(_emptyStorages[i]);
				Debug.Log("Количество элементов в ящике для материалов: " + _emptyStorages[i].Count);
				_emptyStorages.Remove(_emptyStorages[i]);
				Product();
				break;
			}
		}
	}
}
