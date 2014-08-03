using UnityEngine;
using System.Collections;

public class Child : MonoBehaviour {

	public GameObject leftPoint, rightPoint;
	public float speed;
	private bool left;
	private Vector3 v;

	// Use this for initialization
	void Start () {
		left = Random.Range(0,2) == 1 ? left : !left;
		speed = Random.Range(1.0f, 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(left)
		{
			transform.position = Vector3.SmoothDamp(transform.position, new Vector3(leftPoint.transform.position.x, transform.position.y, transform.position.z), ref v, speed * Time.deltaTime, speed/2);
			if(Mathf.Abs (transform.position.x - leftPoint.transform.position.x) < 0.1f)
				left = !left;
		}
		else
		{
			transform.position = Vector3.SmoothDamp(transform.position, new Vector3(rightPoint.transform.position.x, transform.position.y, transform.position.z), ref v, speed * Time.deltaTime, speed/2);
			if(Mathf.Abs (transform.position.x - rightPoint.transform.position.x) < 0.1f)
				left = !left;
		}



	}


}
