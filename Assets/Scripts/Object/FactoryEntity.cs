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
	public virtual int ProductionSpeed
	{
		get { return _productionSpeed; }
		protected set { _productionSpeed = value; }
	}

	private void Start()
	{
		Product();
	}

	private async void Product()
	{
		await Task.Delay(ProductionSpeed * 1000);
		var obj = Instantiate(_thing, _releasePosition.position, Quaternion.identity);
		obj.Move(transform.position, _storage.EmptyPosition);
		//Debug.Log(_thing);
	}
}
