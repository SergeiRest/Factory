using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractThing : MonoBehaviour
{
    public async void Move(Vector3 startPosition, Vector3 endPosition)
	{
		while((transform.position - endPosition).sqrMagnitude > 0.1f)
		{
			transform.position = Vector3.MoveTowards(transform.position, endPosition, 7 * Time.deltaTime);
			await System.Threading.Tasks.Task.Delay(5);
		}
		transform.position = endPosition;
	}
}
