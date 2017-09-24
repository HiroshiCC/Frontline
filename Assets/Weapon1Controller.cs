﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1Controller : MonoBehaviour {

	private Rigidbody myRigidbody;
	private float speed;			// 弾の速さ
	private float periodOfLive;		// 弾の最大生存期間

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start () {
		myRigidbody = GetComponent<Rigidbody>();

		/*
		// 弾の速さをと生存期間の設定
		if (myRigidbody.tag == "tagCANON")
		{
			speed = 150.0f;
			periodOfLive = 2.5f;
		}
		else if (myRigidbody.tag == "tagMISSILE")
		{
			speed = 40.0f;
			periodOfLive = 3.0f;
		}
		else if (myRigidbody.tag == "tagMACHINEGUN")
		{
			speed = 120.0f;
			periodOfLive = 2.0f;
		}
		*/
		speed = 140.0f;
		periodOfLive = 0.75f;

		myRigidbody.velocity = transform.forward * speed;

		// 発射後 3秒で消す
		Destroy(gameObject, periodOfLive);
	}

	//******************************************************************************************
	//	Update
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Update () {
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
			// 標的に命中
			Destroy(gameObject);
		}
		else if (other.gameObject.tag == "tagGROUND")
		{
			// 地面に外れた
			Destroy(gameObject);
		}
	}
}
