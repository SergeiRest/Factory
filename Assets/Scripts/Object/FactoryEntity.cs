using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class FactoryEntity : MonoBehaviour
{
	[SerializeField] private Storage _storage;
	[SerializeField] private AbstractThing _thing;
	[SerializeField] private Transform _releasePosition;

	private int _productionSpeed;
	private bool _isWork = true;

	public delegate void FactoryDelegate(string text);
	public FactoryDelegate OnFactoryStopped;
	public FactoryDelegate OnFactoryReboot;

	public virtual int ProductionSpeed
	{
		get { return _productionSpeed; }
		protected set { _productionSpeed = value; }
	}

	protected virtual void Start()
	{
		Product();
		_storage.PositionAppeared += Reboot;
		_storage.Stop += StopProduction;
	}

	public virtual async void Product()
	{
		await Task.Delay(ProductionSpeed * 1000);
		if (_isWork)
		{
			var obj = Instantiate(_thing, _releasePosition.position, Quaternion.identity);
			_storage.AddThing(obj);
			obj.Move(transform.position, _storage.EmptyPosition);
			if (_storage.CountThings < _storage.Capacity)
			{
				Product();
			}
			else
			{
				OnFactoryStopped?.Invoke("Склад заполнился");
			}
		}
	}

	public void StopProduction()
	{
		Debug.Log("Остановили производство");
		_isWork = false;
	}

	private void Reboot()
	{
		_isWork = true;
		OnFactoryReboot?.Invoke("");
		Product();
	}
}
