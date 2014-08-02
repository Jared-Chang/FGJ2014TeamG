using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
	public int buildingHurtRange = 1;
	public GameObject bulletPrefab;
	public GameObject[] archerPrefab;
	public GameObject[] toperPreafab;
	public int towerNumber;
	public int archerAttackRange = 5;
	public float bulltSpeed=1;

	public GameObject refTower;
	public GameObject refArcherPrefab;


	// Use this for initialization
	void Start () {
		archerPrefab = new GameObject[towerNumber];
		toperPreafab = new GameObject[towerNumber];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F1)) 
		{
			//ShootBullt();
			GameObject newBullt = GameObject.Instantiate(bulletPrefab) as GameObject;
			newBullt.transform.localPosition = refArcherPrefab.transform.localPosition;
			Bullt mBullt = newBullt.GetComponent<Bullt>();
			mBullt.targertPosition = new Vector3(refArcherPrefab.transform.position.x -1000, refArcherPrefab.transform.position.y, refArcherPrefab.transform.position.z);
			mBullt.speed = bulltSpeed;
			mBullt.intiReady = true;
		}

		
		float time = 0f;
		
		time += Time.deltaTime;
		if (time > 0.5f) {
			GameObject newBullt = GameObject.Instantiate(bulletPrefab) as GameObject;
			newBullt.transform.localPosition = refArcherPrefab.transform.localPosition;
			Bullt mBullt = newBullt.GetComponent<Bullt>();
			mBullt.targertPosition = new Vector3(refArcherPrefab.transform.position.x -1000, refArcherPrefab.transform.position.y, refArcherPrefab.transform.position.z);
			mBullt.speed = bulltSpeed;
			mBullt.intiReady = true;
			time = 0f;
		}
	}

	void OnTriggerEnter2D(Collider2D colli)
	{
		if(colli.tag == "Enemies")
		{
			//when building get enemy hit
			SendMessage("Damager", buildingHurtRange);
			Debug.Log("building get Enemies hit");
			Destroy(colli.gameObject);
		}
	}
	

	void ShootBullt()
	{
		for (int i = 0; i < towerNumber; i++) 
		{
			/*Collider2D[] mCollers = Physics2D.OverlapCircleAll(archerPrefab[i], archerAttackRange);
			int enemiesCount = 0;
			float[] distance;
			for( int j = 0; j < mCollers.Length; j++)
			{
				if(mCollers[j].tag == "Enemies")
				{
					enemiesCount++;

				}
			}
			if(enemiesCount > 0)
			{
				distance = new int[enemiesCount];
			}
			float maxDistance = 0;
			for(int k = 0; k < enemiesCount; k++)
			{
				distance[k] = Vector3.Distance(archerPrefab[i], mCollers[k].transform.position);
				if(k >0)
				{
					if (distance[k] < distance[k-1])
					{
						maxDistance = distance[k-1];
					}
				}
			}*/
			GameObject newBullt = GameObject.Instantiate(bulletPrefab) as GameObject;
			newBullt.transform.localPosition = archerPrefab[i].transform.localPosition;
			Bullt mBullt = newBullt.GetComponent<Bullt>();
			//if(maxDistance != 0)
			//{
				//Bullt mBullt = newBullt.GetComponent<Bullt>();
			//}
			//else
			//{
				mBullt.targertPosition = new Vector3(archerPrefab[i].transform.position.x -1000, archerPrefab[i].transform.position.y, archerPrefab[i].transform.position.z);
				mBullt.speed = bulltSpeed;
				mBullt.intiReady = true;
			//}


		}

	}

}
