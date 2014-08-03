using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour 
{
	
	//Defined Skill Parameter
	
	public float[] thunderCDTime = new float[3];
	public float[] fireCDTime = new float[3];
	public float[] slayCDTime = new float[3];
	
	
	public float[] thunderAttack = new float[3];
	public float[] fireAttack = new float[3];
	public float[] slayAttack = new float[3];
	
	public float[] thunderSlowdownSpeed = new float[3];

	public float thunderExplosionScope;
	public float fireExplosionScope;
	public float slayExplosionScope;

	public int thunderLevel;
	public int fireLevel;
	public int slayLevel;

	public Transform mainTowerTransform;

	[HideInInspector]
	public bool canUseThunder;
	[HideInInspector]
	public bool canUseFire;
	[HideInInspector]
	public bool canUseSlay;
	
	public GameObject thunderPrefeb;
	public GameObject firePrefeb;
	public GameObject slayPrefeb;

	//Defined Timer
	
	private float thunderCDTimer;
	private float fireCDTimer;
	private float slayCDTimer;

	private Transform fireCDAni, slayCDAni, thunderCDAni;

	// Use this for initialization
	void Start () 
	{
		canUseThunder = true;
		canUseFire = true;
		canUseSlay = true;
		fireCDAni = transform.FindChild("FireCD");
		slayCDAni = transform.FindChild("SlayCD");
		thunderCDAni = transform.FindChild("ThunderCD");

	}

	//To use the skill Thunder
	//recive a Vector2 with target's position.
	public void Thunder( Vector2 targetDirect )
	{ 
		if( canUseThunder )
		{
			StartCoroutine("ThunderCD");
			thunderCDAni.GetComponent<ThunderCDAni>().SetCD(thunderCDTime[thunderLevel]);
			GameObject thunder = (GameObject)Instantiate( thunderPrefeb, mainTowerTransform.position, Quaternion.identity );
			thunder.GetComponent<SkillShoot>().SetSkill( thunderAttack[thunderLevel],  targetDirect, thunderExplosionScope );
		}
	}
	//To use the skill Fire
	//recive a Vector2 with target's position.
	public void Fire( Vector2 targetDirect )
	{
		if( canUseFire )
		{
			StartCoroutine("FireCD");
			fireCDAni.GetComponent<FireCDAni>().SetCD(fireCDTime[fireLevel]);
			GameObject fire = (GameObject)Instantiate( firePrefeb, mainTowerTransform.position, Quaternion.identity );
			fire.GetComponent<SkillShoot>().SetSkill( fireAttack[fireLevel],  targetDirect, fireExplosionScope );
		}
	}
	//To use the skill Slay
	//recive a Vector2 with target's position.
	public void Slay ( Vector2 targetDirect )
	{
		if( canUseSlay )
		{
			StartCoroutine("SlayCD");
			slayCDAni.GetComponent<SlayCDAni>().SetCD(slayCDTime[slayLevel]);
			GameObject slay = (GameObject)Instantiate( slayPrefeb, mainTowerTransform.position, Quaternion.identity );
			slay.GetComponent<SkillShoot>().SetSkill( slayAttack[slayLevel],  targetDirect, slayExplosionScope );
		}
	}

	IEnumerator ThunderCD() 
	{
		canUseThunder = false;
		while ( thunderCDTimer < thunderCDTime[thunderLevel] )
		{
			thunderCDTimer += Time.deltaTime;
			yield return null;
		}
		thunderCDTimer = 0.0f;
		canUseThunder = true;
	}
	IEnumerator FireCD() 
	{
		canUseFire = false;
		while ( fireCDTimer < fireCDTime[fireLevel] )
		{
			fireCDTimer += Time.deltaTime;
			yield return null;
		}
		fireCDTimer = 0.0f;
		canUseFire = true; 
	}
	IEnumerator SlayCD() 
	{
		canUseSlay = false;
		while ( slayCDTimer < slayCDTime[slayLevel] )
		{
			slayCDTimer += Time.deltaTime;
			yield return null;
		}
		slayCDTimer = 0.0f;	
		canUseSlay = true;
	}
}
