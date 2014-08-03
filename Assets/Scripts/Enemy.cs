using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float speed;
	public float hp;
	public ParticleSystem fx;
	public EnemyCreater creater;

	void Start () {

	}

	void Update ()  {
		transform.position = Vector3.MoveTowards(transform.position, this.transform.position + Vector3.right, speed * 0.2f * Time.deltaTime);
	}

	public void Damage(float damage)
	{
		hp -= damage;
		if(hp < 0)
		{
			fx.Stop();
			fx.transform.position = this.transform.position;
			fx.Play();

			creater.count--;
			Destroy(this.gameObject);
		}
	}
}
