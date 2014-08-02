using UnityEngine;
using System.Collections;

public class cloudAI : MonoBehaviour {

	public float maxOffset;
	public float offset;

	private Transform cloud;
	private float timer;
	private float nowOffset;
	private const float timerInterval = 0.01f;


	// Use this for initialization
	void Start () 
	{
		cloud = transform;
		maxOffset = 5.0f;
		offset = 0f;
		nowOffset = 0f;
		timer = 0f;

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void FixedUpdate ()
	{
		timer += Time.fixedDeltaTime;

		if (timer > timerInterval) 
		{
			timer = 0f;
			cloud.position = new Vector2(cloud.position.x + offset,cloud.position.y);
			nowOffset += offset;
		}
		/*if (Mathf.Abs( nowOffset ) > maxOffset)
		{
			offset *= -1;
		}*/
	}
}
