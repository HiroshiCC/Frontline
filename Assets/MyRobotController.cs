using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRobotController : MonoBehaviour {

	private float WALK = 5.0f;			// 歩くスピード
	private float RUN = 15.0f;				// 走るスピード
	private float speed;                // 自機の移動速度
	//private RaycastHit hit;				// Rayのヒット情報
	//private GameObject RobotCamera;
	//private GameObject RobotBody;
	private Rigidbody myRigidbody;
	private float hdgSpeed;
	private bool bulletFlag = false;
	public GameObject weapon1Prefab;
	private Vector3 dir;
	private float sideOfFire = 1.0f;        // 1:右 2:左

	private GameObject launcherL;
	private GameObject launcherR;


	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start () {

        myRigidbody = GetComponent<Rigidbody>();
		//RobotCamera = GameObject.Find("Main Camera");
		//RobotBody = GameObject.Find("MyRobot");
		speed = WALK;

		launcherL = GameObject.Find("LauncherL");
		launcherR = GameObject.Find("LauncherR");
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
		if (speed == 0.0f)
			hdgSpeed = 1.0f;
		else if (speed == WALK)
			hdgSpeed = 0.6f;
		else if (speed == RUN)
			hdgSpeed = 0.3f;

		// for debug
		hdgSpeed = 1.0f;

		// 左に向く
		if (Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(0, -hdgSpeed, 0);

		// 右に向く
		if (Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(0, hdgSpeed, 0);

		// 速度の設定
		if (Input.GetKey(KeyCode.S))
			speed = RUN;                    // 走る
		else if (Input.GetKey(KeyCode.X))
			speed = WALK;                   // 歩く

		// 縦方向の速度を保持する
		Vector3 v = myRigidbody.velocity;
		v.x = 0.0f;
		v.z = 0.0f;

		// 前進・後退（キーを離したら停止）走って後退はできない
		if (Input.GetKey(KeyCode.A))
			myRigidbody.velocity = transform.forward * speed;   // 前進
		else if (Input.GetKey(KeyCode.Z))
			myRigidbody.velocity = transform.forward * (-WALK); // 後退
		else {
			myRigidbody.velocity = new Vector3(0, 0, 0);        // 停止
		}
		// 縦方向の速度を復元する
		myRigidbody.velocity += v;

		// 発射
		// 実際に弾を飛ばすバージョン。連射はできないようにする。
		if (Input.GetKey(KeyCode.Space))
		{
			if (bulletFlag == false)
			{
				bulletFlag = true;
				Debug.Log("Fire!");
				GameObject weapon1 = Instantiate(weapon1Prefab) as GameObject;

				//弾丸作成（左右のランチャから交互に発射する）
				if (sideOfFire == 1)
					weapon1.transform.position = launcherL.transform.position;
				else if (sideOfFire == -1 )
					weapon1.transform.position = launcherR.transform.position;
				sideOfFire *= -1.0f;
			}
		}
		else
			bulletFlag = false;


		// スマホタッチパネル用
		//  ：
		//  ：
		//  ：
		
		//dir = RobotCamera.transform.forward;
		//Debug.Log("Hdg=" + dir);
	}
}
