using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyCreater : MonoBehaviour {
	
	//public int level;
	public GameObject[] respawnPoints;
	public ParticleSystem deathPuff;
	public int count;

	void Start () {
	
	}
	
	void Update () {

	}

	public void StartEnemyWave(GameObject enemy, int amount, int level)
	{
		StartCoroutine(Create(enemy, amount, level));
	}

	private IEnumerator Create(GameObject enemy, int amount, int level)
	{
		int count = amount;
		while(count > 0)
		{
			yield return new WaitForSeconds(Random.Range(1.0f,2.5f));

			//Time's up, create enemy
			GameObject obj = (GameObject)Instantiate(enemy);
			Enemy enemyScript = obj.GetComponent(typeof(Enemy)) as Enemy;
			enemyScript.fx = deathPuff;
			enemyScript.creater = this;
			count--;

			//Create enemy at random respawn point
			GameObject target = respawnPoints[level];
			obj.transform.position = target.transform.position;
		}
		//Create finished
	}
}
