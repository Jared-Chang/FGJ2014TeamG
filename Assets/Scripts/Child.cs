using UnityEngine;
using System.Collections;

public class Child : MonoBehaviour {

	public GameObject leftPoint, rightPoint;
	private bool turn;
	private Vector3 v;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject target;
		if(turn)
		{
			target = leftPoint;
			transform.position = Vector3.SmoothDamp(transform.position, target.transform.position, ref v, 2.0f * Time.deltaTime, 3.0f);
			if(Vector3.Distance(transform.position, target.transform.position) < 0.1f)
				turn = !turn;
		}
		else
		{
			target = rightPoint;
			transform.position = Vector3.SmoothDamp(transform.position, target.transform.position, ref v, 2.0f * Time.deltaTime, 3.0f);
			if(Vector3.Distance(transform.position, target.transform.position) < 0.1f)
				turn = !turn;
		}

	}
}
