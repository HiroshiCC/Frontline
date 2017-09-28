﻿using System.Collections;
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
	void Update () {
		agent.SetDestination( target.position );
	}

	//******************************************************************************************
	//	OnCollisionEnter
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void OnCollisionEnter( Collision other )
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