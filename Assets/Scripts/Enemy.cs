using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, this.transform.position + Vector3.left, 2.0f * Time.deltaTime);
	}

	void OnMouseDown()
	{
		Destroy(this.gameObject);
	}
}
