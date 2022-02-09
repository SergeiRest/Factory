using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractThing : MonoBehaviour
{
    public async void Move(Vector3 startPosition, Vector3 endPosition)
	{
		//Debug.Log((startPosition - endPosition).sqrMagnitude);
		while((transform.position - endPosition).sqrMagnitude > 5)
		{
			transform.position = Vector3.MoveTowards(transform.position, endPosition, 4 * Time.deltaTime);
			Debug.Log("Магнитуда: " + (transform.position - endPosition).sqrMagnitude);
			await System.Threading.Tasks.Task.Delay(50);
		}
		transform.position = endPosition;
	}
}
