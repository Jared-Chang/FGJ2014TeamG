using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

	public float hp = 10;
	public SpriteRenderer hpBar;
	private Vector3 maxScale;
	// Use this for initialization
	void Start () {
		maxScale = hpBar.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D colli)
	{
		if(colli.tag == "Enemies")
		{
			hp = Mathf.Clamp(hp--, 0, 10);
			hpBar.transform.localScale = new Vector3((hp/10) * maxScale.x, maxScale.y, maxScale.z);
			Destroy(colli.gameObject);
		}
	}
}
