using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
	public int buildingHurtRange = 1;
	public GameObject bulletPrefab;
	public GameObject[] archerPrefab;
	public GameObject[] toperPreafab;
	public int towerNumber;
	public int archerAttackRange = 50;
	public float bulltSpeed = 1;
	public float shootSpeed = 1;
	
	public GameObject refArcherPrefab;
	float time = 0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F1)) 
		{
			//ShootBullt();
			GameObject newBullt = GameObject.Instantiate(bulletPrefab) as GameObject;
			newBullt.transform.parent = refArcherPrefab.transform;
			newBullt.transform.localPosition = refArcherPrefab.transform.position;
			Bullt mBullt = newBullt.GetComponent<Bullt>();
			mBullt.targertPosition = new Vector3(refArcherPrefab.transform.position.x -1000, refArcherPrefab.transform.position.y, refArcherPrefab.transform.position.z);
			mBullt.speed = bulltSpeed;
			mBullt.intiReady = true;
		}

		//auto shoot
		time += Time.deltaTime;
		if (time > shootSpeed) {
			ShootBullt();
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
			Collider2D[] mCollers = Physics2D.OverlapCircleAll(archerPrefab[i].transform.position, archerAttackRange);
			Debug.Log("OverlapCircleAll Collers.Length" + mCollers.Length);
			float[] distance = new float[mCollers.Length];
			float miniDistance = 0;
			int miniDistanceId = 0;
			for(int k = 0; k < mCollers.Length; k++)
			{
				if(mCollers[k].tag == "Enemies")
				{
					distance[k] = Vector3.Distance(archerPrefab[i].transform.position, mCollers[k].transform.position);
					Debug.Log("OverlapCircleAll Collers[" + mCollers[k].name + "] distance: " + distance[k].ToString());
					if(k == 0)
					{
						miniDistance = distance[k];
					}
					else
					{
						if (distance[k] < miniDistance)
						{
							miniDistanceId = k;
							miniDistance = distance[k];
						}
					}
				}
			}
			GameObject newBullt = GameObject.Instantiate(bulletPrefab) as GameObject;
			newBullt.transform.localPosition = archerPrefab[i].transform.position;
			Bullt mBullt = newBullt.GetComponent<Bullt>();
			mBullt.speed = bulltSpeed;
			Debug.Log("miniDistance: " + miniDistance);
			if(miniDistance != 0)
			{
				Debug.Log("go to " +mCollers[miniDistanceId].name);
				mBullt.archerPosition = archerPrefab[i].transform.position;
				mBullt.targertPosition = mCollers[miniDistanceId].transform.position;
				mBullt.SetWay();
			}
			else
			{
				mBullt.targertPosition = new Vector3(archerPrefab[i].transform.position.x -1000, archerPrefab[i].transform.position.y, archerPrefab[i].transform.position.z);
			}
			mBullt.intiReady = true;

		}

	}

}
