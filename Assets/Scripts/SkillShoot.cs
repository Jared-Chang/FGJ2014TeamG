using UnityEngine;
using System.Collections;

public class SkillShoot : MonoBehaviour {

	public float skillSpeed;

	private float attack;
	private Vector2 direction;
	private Vector2 target;
	private float radius;
	private const float thoushold = 0.1f;
	private bool isSlay = false;
	private bool isFire = false;
	public GameObject firePart;
	public GameObject thunderPart;
	public GameObject slayPart;

	// Use this for initialization
	void Start () {
		skillSpeed = 10f;
		if( transform.tag == "Slay" )
			isSlay = true;
		if( transform.tag == "Fire" )
			isFire = true;
	}

	void FixedUpdate()
	{
		if( Vector2.Distance( ( Vector2 )( transform.position ), target ) > thoushold )
		{
			transform.position = new Vector2( transform.position.x + direction.x * skillSpeed * Time.deltaTime,
			                                  transform.position.y + direction.y * skillSpeed * Time.deltaTime );
		}
		else
		{
			AttackColculate();
		}

	}
	public void SetSkill ( float _attack, Vector2 _direction, float _radius )
	{
		target = _direction;
		direction = ( _direction - (Vector2)transform.position );
		direction.Normalize();
		attack = _attack;
		radius = _radius;
		CircleCollider2D circle = (CircleCollider2D)gameObject.collider2D;
		circle.radius = 1;
	}
	
	void OnTriggerEnter2D( Collider2D collider )
	{
		if( collider.tag == "Enemies" && !isFire )
		{
			AttackColculate();
		}
	}

	void AttackColculate()
	{
		//These parameter is for slay determine the enemy which will be attacked.
		float minDis = 100, tempDis;
		Collider2D minEnemy = null;
		//
		if( isFire ) Instantiate(firePart, transform.position, Quaternion.identity);
		else if ( isSlay ) Instantiate(slayPart, transform.position, Quaternion.identity);
		else Instantiate(thunderPart, transform.position, Quaternion.identity);

		Collider2D[] allEnemy = Physics2D.OverlapCircleAll( transform.position, radius );
		foreach( Collider2D _collider in allEnemy )
		{
			if( _collider.tag == "Enemies" && !isSlay )
			{
				_collider.gameObject.GetComponent<Enemy>().Damage(attack);
			}
			else if( isSlay && _collider.tag == "Enemies" ) //for slay determine
			{
				tempDis = Vector2.Distance( transform.position, _collider.transform.position );
				if( tempDis < minDis )
				{
					minDis = tempDis;
					minEnemy = _collider;
				}
			}
		}
		if( minEnemy != null ) minEnemy.gameObject.GetComponent<Enemy>().Damage(attack);
		Destroy(gameObject);
	}
}