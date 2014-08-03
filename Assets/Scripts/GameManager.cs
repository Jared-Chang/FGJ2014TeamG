using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public const int HPMAX = 10;

	public int hp;
	public EnemyCreater creater;
	public GameObject[] enemyType;

	public SpriteRenderer logo;
	public SpriteRenderer logoBackground;
	public GameObject startLabel;
	public GameObject titleLabel;
	public GameObject selfDestroySmoke;
	public GameObject finishLight;

	public GameObject[] building;
	public GameObject ruin;
	public Vector3[] ruinPos;

	public GameObject[] cloud;
	public Vector3[] cloudPos;
	public GameObject sky;
	public Vector3 skyPos;
	public Light dirLight;
	public int currentBuildingLevel;

	public Vector3[] buildingOriginalPos;

	//not sure
	private enum Stat { LOGO, TITLE, GAME }
	private Stat stat = Stat.LOGO;

	void Start () {
		currentBuildingLevel = 0;
		buildingOriginalPos = new Vector3[building.Length];
		for(int i = 0; i < building.Length; ++i)
		{
			buildingOriginalPos[i] = building[i].transform.localPosition;
		}

		StartCoroutine("InitLogo");
	}

	void Update () {
		if(stat == Stat.GAME)
		{
			if(Input.GetMouseButtonDown(0))
			{
				Vector2 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
				SendMessage("Thunder", target);
			}
		}
		if(Input.GetKeyDown(KeyCode.Space))
			Application.LoadLevel(0);
	}

	IEnumerator InitLogo()
	{
		logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, 0f);
		Color targetColor = new Color(logo.color.r, logo.color.g, logo.color.b, 1f);
		while(logo.color.a < 0.9f)
		{
			logo.color = Color.Lerp(logo.color, targetColor, 5.0f * Time.deltaTime);
			yield return null;
		}
		
		yield return new WaitForSeconds(1.0f);
		
		targetColor = new Color(logo.color.r, logo.color.g, logo.color.b, 0f);
		while(logo.color.a > 0.1f)
		{
			logo.color = Color.Lerp(logo.color, targetColor, 6.0f * Time.deltaTime);
			yield return null;
		}
		
		logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, 0f);
		yield return new WaitForSeconds(0.5f);
		
		//Logo background fade out
		targetColor = new Color(logoBackground.color.r, logoBackground.color.g, logoBackground.color.b, 0f);
		while(logoBackground.color.a > 0.1f)
		{
			logoBackground.color = Color.Lerp(logoBackground.color, targetColor, 2.0f * Time.deltaTime);
			yield return null;
		}
		logoBackground.gameObject.SetActive(false);

		StartCoroutine("InitTitle");
	}

	IEnumerator InitTitle() {
		//Reset all

		//Display start label
		stat = Stat.TITLE;
		startLabel.SetActive(true);
		titleLabel.SetActive(true);
		while(true)
		{
			if(Input.GetMouseButton(0))
				break;
			yield return null;
		}
		startLabel.SetActive(false);
		titleLabel.SetActive(false);
		StartCoroutine("Init");
	}

	IEnumerator Init() {
		//Create smoke
		float time = 2.0f;
		while(time > 0)
		{
			time -= Time.deltaTime;

			Vector3 pos = building[Random.Range(0, building.Length)].transform.position + new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-0.5f, 0.5f), 0);
			Instantiate(selfDestroySmoke, pos, Quaternion.identity);
			yield return null;
		}

		//Cloud move in
		for(int i = 0; i < cloud.Length; ++i)
		{
			//cloud[i].transform.FindChild("Sprite").GetComponent<Animator>().speed = Random.Range(0.3f, 1.0f);
			StartCoroutine(CoroutineMove(cloud[i], cloudPos[i]));
		}

		//Sky move down
		StartCoroutine(CoroutineMove(sky, skyPos));

		//Make building collapse
		for(int i = building.Length-1; i >= 0; --i)
		{
			StartCoroutine("BuildingCollapse", building[i]);

			//Ruin rise
			if(i == 1) StartCoroutine(CoroutineMove(ruin, ruinPos[0]));
			yield return new WaitForSeconds(0.3f);
		}

		//Game start
		stat = Stat.GAME;
		StartCoroutine("InitLevelOne");
	}

	public Construct cc;
	private IEnumerator InitLevelOne()
	{
		cc.Cons();

		creater = GetComponent(typeof(EnemyCreater)) as EnemyCreater;
		creater.count = 8;
		creater.StartEnemyWave(enemyType[0], 4, 0);
		yield return new WaitForSeconds(2.0f);
		creater.StartEnemyWave(enemyType[0], 4, 0);


		while(true)
		{
			if(creater.count <= 0)
				break;
			yield return null;
		}

		StartCoroutine("InitLevelTwo");
	}

	private IEnumerator InitLevelTwo()
	{
		cc.Cons();

		creater.count = 15;
		creater = GetComponent(typeof(EnemyCreater)) as EnemyCreater;
		creater.StartEnemyWave(enemyType[0], 5, 0);
		yield return new WaitForSeconds(2.0f);
		creater.StartEnemyWave(enemyType[0], 5, 0);
		yield return new WaitForSeconds(8.0f);

		creater.StartEnemyWave(enemyType[0], 5, 0);
		creater.StartEnemyWave(enemyType[1], 5, 1);

		while(true)
		{
			if(creater.count <= 0)
				break;
			yield return null;
		}

		StartCoroutine("InitLevelThree");
	}

	private IEnumerator InitLevelThree()
	{
		cc.Cons();

		creater = GetComponent(typeof(EnemyCreater)) as EnemyCreater;
		creater.StartEnemyWave(enemyType[0], 5, 0);
		yield return new WaitForSeconds(2.0f);
		creater.StartEnemyWave(enemyType[1], 5, 1);
		yield return new WaitForSeconds(8.0f);
		
		creater.StartEnemyWave(enemyType[0], 5, 2);
		creater.StartEnemyWave(enemyType[1], 5, 1);
		
		creater.count = 15;
		while(true)
		{
			if(creater.count <= 0)
				break;
			yield return null;
		}

		StartCoroutine("InitLevelFour");
	}

	private IEnumerator InitLevelFour()
	{
		cc.Cons();
		creater.count = 15;

		creater = GetComponent(typeof(EnemyCreater)) as EnemyCreater;
		creater.StartEnemyWave(enemyType[0], 5, 0);
		yield return new WaitForSeconds(2.0f);
		creater.StartEnemyWave(enemyType[1], 5, 1);
		yield return new WaitForSeconds(8.0f);
		
		creater.StartEnemyWave(enemyType[0], 5, 2);
		creater.StartEnemyWave(enemyType[1], 5, 3);
		

		while(true)
		{
			if(creater.count <= 0)
				break;
			yield return null;
		}

		StartCoroutine("InitLevelFive");
	}

	private IEnumerator InitLevelFive()
	{
		cc.Cons();
		creater.count = 15;

		creater = GetComponent(typeof(EnemyCreater)) as EnemyCreater;
		creater.StartEnemyWave(enemyType[0], 5, 0);
		yield return new WaitForSeconds(2.0f);
		creater.StartEnemyWave(enemyType[1], 5, 1);
		yield return new WaitForSeconds(8.0f);
		
		creater.StartEnemyWave(enemyType[0], 5, 2);
		creater.StartEnemyWave(enemyType[1], 5, 3);

		while(true)
		{
			if(creater.count <= 0)
				break;
			yield return null;
		}

		StartCoroutine("InitLevelSix");
	}

	private IEnumerator InitLevelSix()
	{
		//clear
		cc.Cons();
		finishLight.SetActive(true);

		while(true)
		{
			finishLight.transform.localEulerAngles += new Vector3(0, 0, 1);
			finishLight.transform.FindChild("lightB").localEulerAngles += new Vector3(0, 0, -2);
			yield return null;
		}
	}

	private IEnumerator BuildingCollapse(GameObject build)
	{
		Vector3 targetPos = new Vector3(build.transform.position.x, -7, build.transform.position.z);
		float time = 0.05f;
		while(true)
		{
			build.transform.position = Vector3.MoveTowards(build.transform.position, targetPos, 4.0f*Time.deltaTime);

			time-=Time.deltaTime;
			if(time < 0)
			{
				time = 0.05f;
				Vector3 pos = build.transform.position + new Vector3(Random.Range(-2.0f, 2.0f), 0, 0);
				Instantiate(selfDestroySmoke, pos, Quaternion.identity);
			}

			if(Vector3.Distance(build.transform.position, targetPos) < 0.1f)
				break;
			yield return null;
		}
	}

	private IEnumerator CoroutineMove(GameObject build, Vector3 pos)
	{
		while(true)
		{
			build.transform.localPosition = Vector3.MoveTowards(build.transform.localPosition, pos, 4.0f*Time.deltaTime);

			if(Vector3.Distance(build.transform.localPosition, pos) < 0.1f)
				break;
			yield return null;
		}
		build.transform.localPosition = pos;
	}

	public GameObject[] buildingChild;

	public void StartBuildingChild(int index)
	{
		if(index == 5)
			return;

		buildingChild[index*4].SetActive(true);
		buildingChild[index*4+1].SetActive(true);
		buildingChild[index*4+2].SetActive(true);
		buildingChild[index*4+3].SetActive(true);
	}
}
