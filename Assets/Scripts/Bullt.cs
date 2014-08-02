using UnityEngine;
using System.Collections;

public class Bullt : MonoBehaviour {

	public bool intiReady = false;
	public Vector3 targertPosition;
	public float speed;

	// Update is called once per frame
	void Update () 
	{

		if (intiReady)
		{
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targertPosition, speed);  
		}
	}

	void OnTriggerEnter2D(Collider2D colli)
	{
		Destroy(colli.gameObject);
		Debug.Log("bullt hit " + colli.name);
	}
}
