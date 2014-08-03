using UnityEngine;
using System.Collections;

public class SortUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ParticleSystem ps = this.GetComponent(typeof(ParticleSystem)) as ParticleSystem;
		ps.renderer.sortingOrder = 150;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
