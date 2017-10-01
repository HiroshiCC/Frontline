using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3Control : MonoBehaviour {

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
		myRigidbody = GetComponent<Rigidbody>();
		myRigidbody.velocity = transform.forward * 100.0f;

		// 発射音を設定・再生
		sound = GetComponent<AudioSource>();
		sound.Play();
		
		// 発射後 0.5秒で消す
		Destroy(gameObject, 0.5f);
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
	//******************************************************************************************
	void OnCollisionEnter(Collision other)
	{

		if (other.gameObject.tag == "tagTARGET")
		{
			Destroy(gameObject);
		}
		else if (other.gameObject.tag == "tagGROUND")
		{
			Destroy(gameObject);
			// 地面に外れた時のパーティクルオブジェクトを生成する
			//GameObject explosion = Instantiate(ExplosionPrefab) as GameObject;
			//explosion.transform.position = transform.position;
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
			// ここで、相手を爆発させる
		}
		else if ( other.gameObject.tag == "tagEnemyL" )
		{
			Destroy( gameObject );
		}
	}
}
