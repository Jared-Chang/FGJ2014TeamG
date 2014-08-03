using UnityEngine;
using System.Collections;

public class Construct : MonoBehaviour {

	public GameManager game;
	private bool isBuinding;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Cons()
	{
		//if(!isBuinding)
			StartCoroutine("StartBuild");
	}

	private IEnumerator StartBuild()
	{
		isBuinding = true;
		int index = game.currentBuildingLevel;

		SpriteRenderer sprite = game.ruin.GetComponent<SpriteRenderer>();
		Vector3 skyTarget = game.sky.transform.localPosition + new Vector3(0, 1.5f*(index+1), 0);

		game.StartBuildingChild(index);
		game.cloud[index].SendMessage("MoveCloud");

		while(true)
		{
			game.building[index].transform.localPosition = Vector3.MoveTowards(game.building[index].transform.localPosition, game.buildingOriginalPos[index], 4.0f*Time.deltaTime);
			game.dirLight.intensity = Mathf.MoveTowards(game.dirLight.intensity, 0.1f + (index + 1)*0.2f, 0.2f*Time.deltaTime);

			if(index < 5)
			{
				if(game.building[index].transform.localPosition.y + game.building[index].transform.localScale.y/2 >
				   game.ruin.transform.localPosition.y - game.ruin.transform.localScale.y/2)
				{
					game.ruin.transform.localPosition = new Vector3(game.ruin.transform.localPosition.x, 
					                                                game.building[index].transform.localPosition.y + game.building[index].transform.localScale.y/2 + game.ruin.transform.localScale.y/2, 
					                                                game.ruin.transform.localPosition.z);

					game.sky.transform.localPosition = Vector3.MoveTowards(game.sky.transform.localPosition, 
					                                                       skyTarget, 4.0f*Time.deltaTime);
				}
			}
			else
			{
				Color targetColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
				sprite.color = Color.Lerp(sprite.color, targetColor, 5.0f * Time.deltaTime);

				//Enter game clear stat
			}

			if(Vector3.Distance(game.building[index].transform.localPosition, game.buildingOriginalPos[index]) < 0.1f)
				break;

			yield return null;
		}

		game.currentBuildingLevel++;
		if(index >= 5) game.ruin.SetActive(false);
		isBuinding = false;
	}
}
