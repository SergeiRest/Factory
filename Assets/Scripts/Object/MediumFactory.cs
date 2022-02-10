using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumFactory : FactoryEntity
{
	[SerializeField] private MaterialStorage _materialStorage;

	private bool _isWorking = false;
	public override int ProductionSpeed => 5;

	protected override void Start()
	{
		base.Start();
		_materialStorage.OnCapacityChanged += MaterialStorageChanged;
	}

	public override void Product()
	{
		if((_materialStorage.Count > 0 && _materialStorage.Count <= _materialStorage.Capacity) && _isWorking)
		{
			_materialStorage.RemoveThing();
			base.Product();
		}
		else
		{
			OnFactoryStopped?.Invoke($"Не хватает {_materialStorage.NeccesaryType}");
		}
	}

	private void MaterialStorageChanged(MaterialStorage storage)
	{
		OnFactoryReboot?.Invoke("");
		_isWorking = true;
		Product();
	}
}
