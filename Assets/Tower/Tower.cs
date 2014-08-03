using UnityEngine;
using System.Collections;

public enum ArcherEquipmentType
{
	UseRndSuitsEquipment,
	UseRndEquipment
}

public class Tower : MonoBehaviour {
	public int buildingHurtRange = 1;
	public GameObject bulletPrefab;
	public GameObject shootPart;
	public GameObject[] archerPrefab;
	public GameObject[] shootPoint;


	public Sprite[] arrowSprite;
	public Sprite[] bowSprite;
	public Sprite[] hatSprite;
	public Sprite[] bodySprite;
	public Sprite[] handSprite;
	public Sprite mArrowSprite;
	public SpriteRenderer[] mBowSprite;
	public SpriteRenderer[] mHatSprite;
	public SpriteRenderer[] mBodySprite;
	public SpriteRenderer[] mHandSpriteF;
	public SpriteRenderer[] mHandSpriteB;
	public int RndSuitsEquipmentId;
	public ArcherEquipmentType equipmentType = ArcherEquipmentType.UseRndSuitsEquipment;

	public GameObject[] toperPreafab;
	public int towerNumber;
	public int archerAttackRange = 50;
	public float bulltSpeed = 1;
	public float shootSpeed = 1;
	
	public GameObject refArcherPrefab;
	float time = 0f;

	// Use this for initialization
	void Start () {

		//chang archer image
		int archerImageId = Random.Range(0, bodySprite.Length);
		for (int i = 0; i < towerNumber; i++) 
		{
			mBodySprite[i].sprite = bodySprite[archerImageId];
			mHandSpriteF[i].sprite = handSprite[archerImageId];
			mHandSpriteB[i].sprite = handSprite[archerImageId];
		}

		//chang equipment image
		if (equipmentType == ArcherEquipmentType.UseRndEquipment)
		{
			RndeEquipmnet();
		}
		else if (equipmentType == ArcherEquipmentType.UseRndSuitsEquipment)
		{
			RndSuitsEquipmentId = Random.Range(0, arrowSprite.Length);
			for(int i = 0; i < towerNumber; i++)
			{
				mArrowSprite = arrowSprite[RndSuitsEquipmentId];
				mBowSprite[i].sprite = bowSprite [RndSuitsEquipmentId];
				mHatSprite[i].sprite = hatSprite [RndSuitsEquipmentId];
			}
		}

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
			//find neer target
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

			// creat bullt
			GameObject newBullt = GameObject.Instantiate(bulletPrefab) as GameObject;
			newBullt.transform.localPosition = shootPoint[i].transform.position;
			Bullt mBullt = newBullt.GetComponent<Bullt>();
			mBullt.mSpriteRenderer.sprite = mArrowSprite;
			mBullt.speed = bulltSpeed;
			Debug.Log("miniDistance: " + miniDistance);
			if(miniDistance != 0)
			{
				// find tagater
				Debug.Log("go to " +mCollers[miniDistanceId].name);
				mBullt.archerPosition = shootPoint[i].transform.position;
				mBullt.targertPosition = mCollers[miniDistanceId].transform.position;
				mBullt.SetWay();
			}
			else
			{
				//not find tagater
				//mBullt.targertPosition = new Vector3(shootPoint[i].transform.position.x -1000, shootPoint[i].transform.position.y, shootPoint[i].transform.position.z);
			}
			newBullt.transform.LookAt(mBullt.targertPosition);
			shootPart.transform.LookAt(mBullt.targertPosition);

			mBullt.intiReady = true;
		}

	}

	void RndeEquipmnet()
	{
		if (towerNumber == 0)return;
		for (int i = 0; i < towerNumber; i++) 
		{
			mArrowSprite = arrowSprite[Random.Range (0, arrowSprite.Length)];
			mBowSprite[i].sprite = bowSprite [Random.Range (0, bowSprite.Length)];
			mHatSprite[i].sprite = hatSprite [Random.Range (0, hatSprite.Length)];
		}
	}
}
