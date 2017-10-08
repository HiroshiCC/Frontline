using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1Controller : MonoBehaviour
{
	public GameObject ExplosionPrefab;

	private Rigidbody myRigidbody;
	private float speed;            // 弾の速さ
	private float periodOfLive;     // 弾の最大生存期間
	private AudioSource sound;
	//private GameObject myCamera;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start()
	{
		myRigidbody = GetComponent<Rigidbody>();
		//myCamera = GameObject.Find( "Main Camera" );
		//RaycastHit hit;

		// 発射音を設定・再生
		sound = GetComponent<AudioSource>();
		sound.Play();

		//// 照準の中央に向かって弾を発射するようにする
		//// カメラの方向にRayを放つ
		//if ( Physics.Raycast( transform.position, myCamera.transform.forward, out hit, 300f ) )
		//{
		//	//発射地点から、カメラのRay方向に向かって、弾を進める
		//}
		

		myRigidbody.velocity = transform.forward * 140.0f;

		// 発射後 1秒で消す
		Destroy( gameObject, 1f );
	}

	//******************************************************************************************
	//	Update
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Update()
	{
	}

	//******************************************************************************************
	//	OnCollisionEnter
	// [引数]
	// [戻り値]
	// [コメント]
	//	ステージ１では、こちらを使う
	//******************************************************************************************
	void OnCollisionEnter( Collision other )
	{
		if ( other.gameObject.tag == "tagTARGET" )
		{
			// 標的に命中
			Destroy( gameObject );
		}
		else if ( other.gameObject.tag == "tagGROUND" )
		{
			// 地面に外れた
			Destroy( gameObject );
			GameObject explosion = Instantiate(ExplosionPrefab) as GameObject;
			explosion.transform.position = transform.position;
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
