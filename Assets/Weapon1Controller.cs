using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1Controller : MonoBehaviour {

	private GameObject RobotCamera;
	private Vector3 dir;
	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start () {
		RobotCamera = GameObject.Find("Main Camera");
		//gameObject.transform.Rotate( new Vector3( RobotCamera.transform.eulerAngles.y, RobotCamera.transform.eulerAngles.z ) );
		dir = RobotCamera.transform.forward;
	}

	//******************************************************************************************
	//	Update
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Update () {
		//transform.Translate( -60.0f*Time.deltaTime,0f,0f);
		transform.Translate( dir * 60.0f * Time.deltaTime );
		// 射程距離を計算し、Destroy()する。
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
		}
		else
			Debug.Log("missA");

		Destroy(gameObject);
	}
}
