using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3Control : MonoBehaviour {

	private Rigidbody myRigidbody;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start () {
		myRigidbody = GetComponent<Rigidbody>();
		myRigidbody.velocity = transform.forward * 100.0f;

		// 発射後 0.5秒で消す
		Destroy(gameObject, 0.5f);
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
