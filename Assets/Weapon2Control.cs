using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2Control : MonoBehaviour {

	public GameObject ExplosionPrefab;

	private Rigidbody myRigidbody;
	private AudioSource sound;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start ()
	{

		// 速度を設定
		myRigidbody = GetComponent<Rigidbody>();
		myRigidbody.velocity = transform.forward * 40.0f;

		// 発射音を設定・再生
		sound = GetComponent<AudioSource>();
		sound.Play();

		// 発射後 4秒で消す
		Destroy( gameObject, 6.0f);
	}

	//******************************************************************************************
	//	Update
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Update ()
	{
		
	}

	//******************************************************************************************
	//	OnCollisionEnter
	// [引数]
	// [戻り値]
	// [コメント]
	//	ステージ１では、こちらを使う
	//******************************************************************************************
	void OnCollisionEnter(Collision other)
	{

		if (other.gameObject.tag == "tagTARGET")
		{
			Destroy(gameObject);
		}
		else if (other.gameObject.tag == "tagGROUND")
		{
			GameObject explosion = Instantiate(ExplosionPrefab) as GameObject;
			explosion.transform.position = transform.position;

			Destroy( gameObject );
		}
	}

	//******************************************************************************************
	//	OnTriggerEnter
	// [引数]
	// [戻り値]
	//	ステージ２では、こちらを使う
	//******************************************************************************************
	void OnTriggerEnter( Collider other )
	{
		if ( other.gameObject.tag == "tagEnemyS" )
		{
			Destroy( gameObject );
		}
		else if ( other.gameObject.tag == "tagEnemyM" )
		{
			Destroy( gameObject );
		}
		else if ( other.gameObject.tag == "tagEnemyL" )
		{
			Destroy( gameObject );
		}
	}
}
