using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class FactoryEntity : MonoBehaviour
{
	[SerializeField] private Storage storage;
	[SerializeField] private AbstractThing thing;

	private int _productionSpeed;
	public virtual int ProductionSpeed
	{
		get { return _productionSpeed; }
		protected set { _productionSpeed = value; }
	}

	private void Start()
	{
		Debug.Log("Начали");
		Product();
	}

	private async void Product()
	{
		await Task.Delay(ProductionSpeed * 1000);
		Debug.Log(thing);
	}
}
