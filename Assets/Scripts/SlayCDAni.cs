using UnityEngine;
using System.Collections;

public class SlayCDAni : MonoBehaviour {

	private const float underBound = -0.8f;
	private const float oringin = 0.0f;
	private float speed;
	
	public void SetCD( float second )
	{
		transform.localPosition = new Vector2( transform.localPosition.x, oringin );
		StartCoroutine("CDAnimation", second );
		speed = underBound / second;
	}
	
	IEnumerator CDAnimation( float second )
	{
		while( transform.localPosition.y > underBound )
		{
			transform.localPosition = new Vector2( transform.localPosition.x, transform.localPosition.y + speed * Time.fixedDeltaTime );
			yield return null;
		}
	}
}
