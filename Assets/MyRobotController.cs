using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRobotController : MonoBehaviour {

	private float WALK = 5f;	// 歩くスピード
	private float RUN = 15;		// 走るスピード
	private float speed;
    private Rigidbody myRigidbody;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start () {
        myRigidbody = GetComponent<Rigidbody>();
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
			myRigidbody.velocity = transform.forward * speed;  // 前進
		else if (Input.GetKey(KeyCode.Z))
			myRigidbody.velocity = transform.forward * (-WALK);  // 後退
		else
			myRigidbody.velocity = new Vector3(0, 0, 0);        // 停止

		// スマホタッチパネル用
		//  ：
		//  ：
		//  ：

	}
}
