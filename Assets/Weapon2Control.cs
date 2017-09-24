﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2Control : MonoBehaviour {

	private Rigidbody myRigidbody;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start () {
		myRigidbody = GetComponent<Rigidbody>();
		myRigidbody.velocity = transform.forward * 40.0f;

		// 発射後 3秒で消す
		Destroy(gameObject, 4.0f);
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
			Debug.Log("Hit!");
			Destroy(gameObject);
		}
		else if (other.gameObject.tag == "tagGROUND")
		{
			Debug.Log("Ground");
			Destroy(gameObject);
		}
	}
}
