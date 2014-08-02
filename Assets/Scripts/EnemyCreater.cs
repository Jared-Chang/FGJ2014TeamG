using UnityEngine;
using System.Collections;

public class EnemyCreater : MonoBehaviour {

	public GameObject enemy;

	// Use this for initialization
	void Start () {
	
	}

	private float time = 1.0f;
	private int count = 10;
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if(time < 0)
		{
			time = Random.Range(1,3);
			count--;

			GameObject obj = (GameObject)Instantiate(enemy);
			obj.transform.position = this.transform.position + new Vector3(30, 0, 0);
		}
	}
}
