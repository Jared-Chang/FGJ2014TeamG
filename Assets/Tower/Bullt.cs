using UnityEngine;
using System.Collections;

public class Bullt : MonoBehaviour {

	public bool intiReady = false;
	public Vector3 targertPosition;
	public Vector3 archerPosition;
	public float speed;
	Vector3 way;
	public float deathTime = 30f;
	float time = 0f;
	public SpriteRenderer mSpriteRenderer;
	public float atk;
	// Update is called once per frame
	void Update () 
	{
		//auto shoot
		time += Time.deltaTime;
		if (time > deathTime) {
			Destroy(gameObject);
		}

		if (intiReady)
		{
			gameObject.transform.position = Vector3.MoveTowards(transform.position, transform.position + way, speed);  
		}
	}

	void OnTriggerEnter2D(Collider2D colli)
	{
		Debug.Log("bullt hit " + colli.name);
		colli.transform.SendMessage("Damage", atk);
		Destroy(gameObject);
	}

	public void SetWay()
	{
		way = (targertPosition - archerPosition).normalized;


		this.transform.FindChild("Arrow").localEulerAngles = new Vector3(0, 0, Mathf.Atan2(way.y, way.x)*Mathf.Rad2Deg);
	}
}
