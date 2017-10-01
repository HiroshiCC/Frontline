using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMController : MonoBehaviour {

	public GameObject ExplosionPrefab;
	public Transform target;
	NavMeshAgent agent;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}

	//******************************************************************************************
	//	Update
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Update()
	{
		agent.SetDestination( target.position );

		//agent.SetDestination( new Vector3( 100.0f, 0.0f, 100.0f ) );
		// 姿勢が、地面の角度に沿うようにしようと思った。
		//RaycastHit hit;
		//if ( Physics.Raycast( transform.position, Vector3.down, out hit, 300f ) )
		//{
		//	gameObject.transform.rotation = Quaternion.Euler( hit.normal );
		//}
	}

	//******************************************************************************************
	//	OnCollisionEnter
	// [引数]
	// [戻り値]
	// [コメント]
	//	Stage1では、このキャラは出てこない
	//******************************************************************************************
	//void OnCollisionEnter( Collision other )
		//{
		//	if ( (other.gameObject.tag == "tagCANON") || (other.gameObject.tag == "tagMISSILE") || (other.gameObject.tag == "tagMACHINEGUN") )
		//	{
		//		Destroy( gameObject );
		//		// 爆発のassetを実行
		//		GameObject explosion = Instantiate(ExplosionPrefab) as GameObject;
		//		explosion.transform.position = transform.position;
		//	}
		//}
	
		//******************************************************************************************
		//	OnTriggerEnter
		// [引数]
		// [戻り値]
		// [コメント]
		//******************************************************************************************
		void OnTriggerEnter( Collider other )
	{
		if ( (other.gameObject.tag == "tagCANON") || (other.gameObject.tag == "tagMISSILE") || (other.gameObject.tag == "tagMACHINEGUN") )
		{
			Destroy( gameObject );
			// 爆発のassetを実行
			GameObject explosion = Instantiate(ExplosionPrefab) as GameObject;
			explosion.transform.position = transform.position;
		}
	}
}
