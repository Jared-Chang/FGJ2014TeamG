using UnityEngine;
using System.Collections;

public class CallCloud : MonoBehaviour {
	
	private CloudAI[] clouds;
	// Use this for initialization

	void Start () 
	{
		clouds = gameObject.GetComponentsInChildren<CloudAI> ();
	}

	/*
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			MoveCloud();	
		}
		//Debug.Log(conter);
	}
*/
	public void MoveCloud()
	{
		/*
		GameObject.Find("Cloud1").GetComponent <CloudAI> ().MoveCloud();
		GameObject.Find("Cloud2").GetComponent <CloudAI> ().MoveCloud();
		GameObject.Find("Cloud3").GetComponent <CloudAI> ().MoveCloud();
		GameObject.Find("Cloud4").GetComponent <CloudAI> ().MoveCloud();
		GameObject.Find("Cloud5").GetComponent <CloudAI> ().MoveCloud();
		GameObject.Find("Cloud6").GetComponent <CloudAI> ().MoveCloud();
		*/
		int[] cloudRand = new int[3];
		for (int i = 0; i < 3; ++i) 
		{
			cloudRand[i] = Random.Range(0,5);
			if( i == 1 )
			{
				while( cloudRand[ i ] == cloudRand[ i-1 ] )
				{
					cloudRand[i] = Random.Range(0,5);
				}
			}
			if( i == 2 )
			{
				while( cloudRand[ i ] == cloudRand[ i-1 ] || cloudRand[ i ] == cloudRand[ i-2 ]  )
				{
					cloudRand[i] = Random.Range(0,5);
				}
			}
			clouds[ cloudRand[ i ] ].quitDirection *= -1;	
		}
		/*
		foreach (CloudAI cloud in clouds) {
				
			cloud.MoveCloud();

		}
*/
		for( int i = 0; i < 6; ++i )clouds[i].MoveCloud();
	}
}
