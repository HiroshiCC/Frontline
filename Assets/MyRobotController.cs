using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRobotController : MonoBehaviour {

	private float WALK = 5f;			// 歩くスピード
	private float RUN = 15f;				// 走るスピード
	private float speed;                // 自機の移動速度
	//private RaycastHit hit;				// Rayのヒット情報
	private GameObject RobotCamera;
	private GameObject RobotBody;
	private Rigidbody myRigidbody;
	private float hdgSpeed;
	private bool bulletFlag = false;
	public GameObject weapon1Prefab;
	//private Vector3 dir; 

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start () {

        myRigidbody = GetComponent<Rigidbody>();
		RobotCamera = GameObject.Find("Main Camera");
		RobotBody = GameObject.Find("MyRobot");
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

		// 移動スピードによって、旋回速度が変わる
		if (speed == 0)
			hdgSpeed = 1.0f;
		else if (speed == WALK)
			hdgSpeed = 0.6f;
		else if (speed == RUN)
			hdgSpeed = 0.3f;

        // 左に向く
        if (Input.GetKey(KeyCode.LeftArrow))
             transform.Rotate(0, -hdgSpeed, 0);
 
        // 右に向く
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(0, hdgSpeed, 0);

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
		/*
		if (Input.GetKey(KeyCode.Space))
		{
			Debug.Log("shoot!");
			// Rayを放つ。発射位置は自機の中心。方向はカメラの方向。距離300。
			if (Physics.Raycast(transform.position, myCamera.transform.forward, out hit, 300f))
			{
				if (hit.collider.tag == "tagTARGET")
				{
					Debug.Log("Hit");
					//Destroy(hit.rigidbody.gameObject);	// <-- ダメだった。gameObjectには何も入っていないらしい
				}
				else
					Debug.Log("miss1"); // 山など、ターゲット以外の場合
			}
			else
				Debug.Log("miss2");	// 空に向けて発射した場合
		}
		*/
		// 実際に弾を飛ばすバージョン。連射はできないようにする。
		if (Input.GetKey(KeyCode.Space))
		{
			if (bulletFlag == false)
			{
				bulletFlag = true;
				Debug.Log("Fire!");
				//弾丸作成
				//fir = RobotCamera.transform.forward;
				GameObject weapon1 = Instantiate(weapon1Prefab) as GameObject;
				weapon1.transform.position = new Vector3(RobotBody.transform.position.x-3, RobotBody.transform.position.y+2, RobotBody.transform.position.z);
			}
		}
		else
			bulletFlag = false;
			

		// スマホタッチパネル用
		//  ：
		//  ：
		//  ：

	}
}
