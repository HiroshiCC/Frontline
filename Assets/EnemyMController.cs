using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMController : MonoBehaviour {

	public GameObject ExplosionPrefab;
	public Transform target;
	NavMeshAgent agent;
	GameObject TargetPos;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start ()
	{
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
		agent.SetDestination( TargetPos.transform.position );

		//agent.SetDestination( new Vector3( 100.0f, 0.0f, 100.0f ) );
		// 姿勢が、地面の角度に沿うようにしようと思った。
		//RaycastHit hit;
		//if ( Physics.Raycast( transform.position, Vector3.down, out hit, 300f ) )
		//{
		//	gameObject.transform.rotation = Quaternion.Euler( hit.normal );
		//}
	}

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

	//******************************************************************************************
	//	目的地をセットする
	// [引数]
	//	GameObject Obj		: 目的値となるオブジェクト	
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	public void SetTargetPos( GameObject Obj )
	{
		TargetPos = Obj;
	}
}
