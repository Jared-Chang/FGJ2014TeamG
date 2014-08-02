using UnityEngine;
using System.Collections;

public class TestForSkill : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		if( Input.GetKeyDown("w") )
		{
			GameObject.Find("Skill").GetComponent<Skill>().Thunder( new Vector2(10, 10) );
			
		}
		if( Input.GetKeyDown("e") )
		{
			GameObject.Find("Skill").GetComponent<Skill>().Fire( new Vector2(10, 10) );
			
		}
		if( Input.GetKeyDown("r") )
		{
			GameObject.Find("Skill").GetComponent<Skill>().Slay( new Vector2(10, 10) );
			
		}
	}
}
