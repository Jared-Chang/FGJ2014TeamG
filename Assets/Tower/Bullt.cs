using UnityEngine;
using System.Collections;

public class Bullt : MonoBehaviour {

	public bool intiReady = false;
	public Vector3 targertPosition;
	public Vector3 archerPosition;
	public float speed;
	Vector3 way;

	// Update is called once per frame
	void Update () 
	{
		if (intiReady)
		{
			gameObject.transform.position = Vector3.MoveTowards(transform.position, transform.position + way, speed);  
		}
	}

	void OnTriggerEnter2D(Collider2D colli)
	{
		Debug.Log("bullt hit " + colli.name);
		Destroy(gameObject);
	}

	public void SetWay()
	{
		way = (targertPosition - archerPosition).normalized;
	}
}
