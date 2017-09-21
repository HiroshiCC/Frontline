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
		dir = RobotCamera.transform.forward;	// <--ここにこれがないと、ホーミング弾になってしまう。

		// 発射後 3秒で消す
		Destroy(gameObject, 3.0f);
	}

	//******************************************************************************************
	//	Update
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Update () {
		transform.Translate(dir * 30.0f * Time.deltaTime);	// <-- これでは遅い。100.0fにしたいが、素通りしてしまう。
	}

	//******************************************************************************************
	//	OnCollisionEnter
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void OnCollisionEnter(Collision other)
	{
		Debug.Log(other.gameObject.tag);  // <=== なぜ「Untagged」なのか！

		// 発射直後、ランチャーに触れてしまうので、無視する。
		//if (other.gameObject.tag == "tagLAUNCHER")
		//{
		//	Debug.Log("Launcher");
		//	return;
		//}

		if (other.gameObject.tag == "tagTARGET")
		{
			Debug.Log("Hit!");
			Destroy(gameObject);
		}
		else
		{
			Debug.Log("missA");
			//Destroy(gameObject);
		}
		// どこかに衝突したらすぐに消す。

	}
}
