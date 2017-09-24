using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTargetController : MonoBehaviour
{
	public GameObject ExplosionPrefab;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnCollisionEnter(Collision other)
	{
		if ( (other.gameObject.tag == "tagCANON") || (other.gameObject.tag == "tagMISSILE") || (other.gameObject.tag == "tagMACHINEGUN") )
		{
			Destroy(gameObject);
			GameObject explosion = Instantiate(ExplosionPrefab) as GameObject;
			explosion.transform.position = transform.position;
		}
	}
}
