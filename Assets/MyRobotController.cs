using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MyRobotController : MonoBehaviour {

	public GameObject weapon1Prefab;	// キャノン砲
	public GameObject weapon2Prefab;	// ミサイル
	public GameObject weapon3Prefab;	// 機関銃

	private float WALK = 5.0f;			// 歩くスピード
	private float RUN = 15.0f;				// 走るスピード
	private float speed;                // 自機の移動速度
	private Rigidbody myRigidbody;
	private float hdgSpeed;
	private bool bulletFlag = false;
	private Vector3 dir;
	private int kindOfWeapon = 0;			// 0:Canon 1:Missile 2:MachineGun 
	private GameObject launcherL;
	private GameObject launcherR;
	private GameObject gunL;
	private GameObject gunR;
	private GameObject myCamera;
	private bool selectFlag = false;
	private int gunSide = 1;
	private float mgCount = 0.0f;
	private string[] weaponName;
	private int weaponEnergy;
	// UIテキスト関連
	private GameObject weaponNameText;
	private GameObject weaponEnergyText;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start () {

		weaponEnergy = 1000;

		weaponName = new []{ "GUN", "MSL", "M/G" };
		myRigidbody = GetComponent<Rigidbody>();
		speed = WALK;
		launcherL = GameObject.Find("LauncherL");
		launcherR = GameObject.Find("LauncherR");
		gunL = GameObject.Find("MG_RIGHT");
		gunR = GameObject.Find("MG_LEFT");
		myCamera = GameObject.Find("Main Camera");
		weaponNameText = GameObject.Find("WeaponNameText");
		weaponEnergyText = GameObject.Find("WeaponEnergyText");

		// 画面の初期化
		weaponNameText.GetComponent<Text>().text = weaponName[kindOfWeapon];    // 兵器名
		weaponEnergyText.GetComponent<Text>().text = "W.E "+ weaponEnergy ;    // 兵器エネルギー

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
		else
			myRigidbody.velocity = new Vector3(0, 0, 0);        // 停止
																// 縦方向の速度を復元する
		myRigidbody.velocity += v;

		// 武器選択（チャタリング防止）
		if (Input.GetKey(KeyCode.C)) {
			if (selectFlag == false)
			{
				selectFlag = true;
				kindOfWeapon++;
				kindOfWeapon %= 3;
				// 兵器名の表示更新
				weaponNameText.GetComponent<Text>().text = weaponName[kindOfWeapon];
			}
		}
		else
			selectFlag = false;

		// 弾の生成・発射
		if (Input.GetKey(KeyCode.Space))
		{
			switch (kindOfWeapon)
			{
				case 0:
					if (weaponEnergy < 10)
						break;
					// キャノン砲（右ランチャーから発射）
					if (bulletFlag == false)
					{
						bulletFlag = true;
						GameObject weapon1 = Instantiate(weapon1Prefab) as GameObject;
						weapon1.transform.position = launcherR.transform.position;
						weapon1.transform.rotation = myCamera.transform.rotation;
						weaponEnergy -= 10;
					}
					break;
				case 1:
					if (weaponEnergy < 100)
						break;
					// ミサイル（左ランチャーから発射）
					if (bulletFlag == false)
					{
						bulletFlag = true;
						GameObject weapon2 = Instantiate(weapon2Prefab) as GameObject;
						weapon2.transform.position = launcherL.transform.position;
						weapon2.transform.rotation = myCamera.transform.rotation;
						weaponEnergy -= 100 ;
					}
					break;
				case 2:
					if (weaponEnergy < 1)
						break;
					// 機関銃（連射OK、胴体中心から、左右交互に発射）
					if (mgCount == 0.0f)
					{
						GameObject weapon3 = Instantiate(weapon3Prefab) as GameObject;
						if (gunSide == 1)
							weapon3.transform.position = gunL.transform.position;
						else if (gunSide == -1)
							weapon3.transform.position = gunR.transform.position;
						weapon3.transform.rotation = myCamera.transform.rotation;
						gunSide *= -1;
						weaponEnergy -= 1;
					}
					// 連射は0.15秒に1発
					mgCount += Time.deltaTime;
					if (mgCount > 0.15f)
						mgCount = 0.0f;
					break;
				default:
					break;
			}
			weaponEnergyText.GetComponent<Text>().text = "W.E " + weaponEnergy;    // 兵器エネルギー

		}
		else {
			bulletFlag = false;
			mgCount = 0.0f ;
		}
	}
}
