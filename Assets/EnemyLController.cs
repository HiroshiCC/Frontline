using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLController : MonoBehaviour {

	public GameObject ExplosionPrefab;  // 爆発
	//public GameObject SparkPrefab;		// 爆発
	public Transform target;
	NavMeshAgent agent;
	GameObject TargetPos;
	int hp;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start ()
	{
		hp = 15;
		agent = GetComponent<NavMeshAgent>();
	}

	//******************************************************************************************
	//	Update
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Update ()
	{
		agent.SetDestination( TargetPos.transform.position );
	}

	//******************************************************************************************
	//	OnCollisionEnter
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void OnTriggerEnter( Collider other )
	{
		if ( (other.gameObject.tag == "tagCANON") || (other.gameObject.tag == "tagMISSILE") || (other.gameObject.tag == "tagMACHINEGUN") )
		{

			if ( other.gameObject.tag == "tagCANON" )
				hp -= 5;
			else if ( other.gameObject.tag == "tagMISSILE" )
				hp -= 15;
			else if ( other.gameObject.tag == "tagMACHINEGUN" )
				hp -= 1;

			if ( hp <= 0 )
			{
				// 破壊された
				Destroy( gameObject );

				GameObject explosion = Instantiate(ExplosionPrefab) as GameObject;
				explosion.transform.position = transform.position;
			}
			else
			{
				// 被弾したが、破壊まで至らない
				//GameObject explosion = Instantiate(SparkPrefab) as GameObject;
				//explosion.transform.position = transform.position;
			}
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
