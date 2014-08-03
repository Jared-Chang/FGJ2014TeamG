using UnityEngine;
using System.Collections;

public class CloudAI : MonoBehaviour {

	private Transform cloud;

	public float maxOffsetRandMin;
	public float maxOffsetRandMax;
	private float maxOffset;
	public float offsetRandMin;
	public float offsetRandMax;
	private float offset;
	private float timer;
	private float timerForMove;
	private float nowOffset;
	private float nowOffsetForMove;
	public float limit;
	public float timerInterval;
	public float timerIntervalForMove;
	public float quitSpeed;
	private float rand;


	public int quitDirection = 1;
	private int animationDirection = 1; 

	// Use this for initialization
	void Start () 
	{
		cloud = transform;
		/*maxOffsetRandMin = 0.5f;
		maxOffsetRandMax = 1.0f;
		offsetRandMin = 0.01f;
		offsetRandMax = 0.02f;
		limit = 10f;
		timerInterval = 0.01f;
		timerIntervalForMove = 0.05f;
		quitSpeed = 0;*/
		nowOffset = 0;
		timer = 0;
		timerForMove = 0;
		maxOffset = Random.Range(maxOffsetRandMin,maxOffsetRandMax);
		offset = Random.Range(offsetRandMin,offsetRandMax);
		//shm = Mathf.Cos (offset) + Mathf.Sin (offset);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public void MoveCloud()
	{	
		StartCoroutine ("CloudAnimation");
	}
	
	IEnumerator CloudAnimation()
	{
		for (;;) 
		{
			timerForMove += Time.fixedDeltaTime;
			timerForMove = 0f;
			cloud.position = new Vector2 (cloud.position.x + offset*quitSpeed*quitDirection, cloud.position.y);
			nowOffsetForMove += offset;

			if (nowOffsetForMove > limit)
			{
				Destroy(gameObject);
			}
			yield return null;
		}
	}
	
	void FixedUpdate ()
	{
		timer += Time.fixedDeltaTime;
		
		if (timer > timerInterval) 
		{
			timer = 0f;
			cloud.position = new Vector2(cloud.position.x,cloud.position.y + offset*animationDirection);
			nowOffset += offset*animationDirection;
		}
		
		if (Mathf.Abs( nowOffset ) > maxOffset)
		{	
			animationDirection *= -1;
		}
	}
}
