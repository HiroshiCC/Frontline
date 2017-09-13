using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRobotController : MonoBehaviour {

	private float WALK = 5f;			// 歩くスピード
	private float RUN = 15;				// 走るスピード
	private float speed;				// 自機の移動速度
	private RaycastHit hit;				// Rayのヒット情報
	private Rigidbody myRigidbody;
	private GameObject myCamera;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start () {

        myRigidbody = GetComponent<Rigidbody>();
		myCamera = GameObject.Find("Main Camera");
		speed = WALK;
    }

	//******************************************************************************************
	//	Update
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Update()
    {
		// PCでのデバッグ用

        // 左に向く
        if (Input.GetKey(KeyCode.LeftArrow))
             transform.Rotate(0, -2, 0);
 
        // 右に向く
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(0, 2, 0);

		// 速度の設定
		if (Input.GetKey(KeyCode.S))
			speed = RUN;					// 走る
		else if (Input.GetKey(KeyCode.X))
			speed = WALK;					// 歩く

		// 前進・後退（キーを離したら停止）走って後退はできない
		if (Input.GetKey(KeyCode.A))
			myRigidbody.velocity = transform.forward * speed;	// 前進
		else if (Input.GetKey(KeyCode.Z))
			myRigidbody.velocity = transform.forward * (-WALK);	// 後退
		else
			myRigidbody.velocity = new Vector3(0, 0, 0);        // 停止
		
		// 発射
		if (Input.GetKey(KeyCode.Space))
		{
			// Rayを放つ。発射位置は自機の中心。方向はカメラの方向。距離300。
			if (Physics.Raycast(transform.position, myCamera.transform.forward, out hit, 300f))
			{
				if (hit.collider.tag == "tagTARGET")
				{
					Debug.Log("Hit");
				}
				else
					Debug.Log("miss1"); // 山など、ターゲット以外の場合
			}
			else
				Debug.Log("miss2");	// 空に向けて発射した場合
		}

		// スマホタッチパネル用
		//  ：
		//  ：
		//  ：

	}
}
