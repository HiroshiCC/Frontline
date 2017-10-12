using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MyRobotController : MonoBehaviour {

	public GameObject weapon1Prefab;	// キャノン砲
	public GameObject weapon2Prefab;	// ミサイル
	public GameObject weapon3Prefab;	// 機関銃

	float WALK = 5.0f;			// 歩くスピード
	float RUN = 15.0f;				// 走るスピード
	float speed =0.0f;                // 自機の移動速度
	int speedGear ;				// 0:---  1:walk 2:run
	Rigidbody myRigidbody;
	float hdgSpeed;
	bool bulletFlag = false;
	Vector3 dir;
	int kindOfWeapon = 0;			// 0:Canon 1:Missile 2:MachineGun 
	GameObject launcherL;
	GameObject launcherR;
	GameObject gunL;
	GameObject gunR;
	GameObject myCamera;
	bool selectFlag = false;
	int gunSide = 1;
	float mgCount = 0.0f;
	string[] weaponName;
	int weaponEnergy;
	GameObject cockpitPanel;
	bool startFlag = false;

	// UIテキスト関連
	GameObject weaponNameText;
	GameObject weaponEnergyText;
	GameObject messageText;
	float elipseTime = 0.0f;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start ()
	{
		weaponEnergy = 500 ;

		weaponName = new []{ "GUN", "MSL", "M/G" };
		myRigidbody = GetComponent<Rigidbody>();
		speed = 0.0f;
		//speedMode = 0;
		speedGear = 0;
		launcherL = GameObject.Find("LauncherL");
		launcherR = GameObject.Find("LauncherR");
		gunL = GameObject.Find("MG_RIGHT");
		gunR = GameObject.Find("MG_LEFT");
		myCamera = GameObject.Find("Main Camera");
		weaponNameText = GameObject.Find("WeaponNameText");
		weaponEnergyText = GameObject.Find("WeaponEnergyText");
		cockpitPanel = GameObject.Find( "Cockpit" );
		messageText = GameObject.Find( "MsgText" );

		// 画面の初期化
		weaponNameText.GetComponent<Text>().text = weaponName[kindOfWeapon];    // 兵器名
		weaponEnergyText.GetComponent<Text>().text = "W.E "+ weaponEnergy ;    // 兵器エネルギー
		messageText.GetComponent<Text>().text = "Start !!!";
	}


	//******************************************************************************************
	//	Update
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Update()
	{
		if ( startFlag == false )
			return;

		float ry;
		elipseTime += Time.deltaTime;

		// 移動スピードによって、旋回速度が変わる
		if ( speed == 0.0f )
			hdgSpeed = 80.0f;
		else if ( speed == WALK )
			hdgSpeed = 50.0f;
		else if ( speed == RUN )
			hdgSpeed = 30.0f;

			// 左に向く
		if ( Input.GetKey( KeyCode.LeftArrow ) )
			transform.Rotate( 0, -hdgSpeed * Time.deltaTime, 0 );

		// 右に向く
		if ( Input.GetKey( KeyCode.RightArrow ) )
			transform.Rotate( 0, hdgSpeed * Time.deltaTime, 0 );

		// 速度の設定
		if ( Input.GetKey( KeyCode.S ) )
			speedGear = 2;                    // 走る
		else if ( Input.GetKey( KeyCode.X ) )
			speedGear = 1;                   // 歩く


		// 縦方向の速度を保持する
		Vector3 v = myRigidbody.velocity;
		v.x = 0.0f;
		v.z = 0.0f;

		// 前進・後退（キーを離したら停止）走って後退はできない
		Vector3 spd = new Vector3( 0, 0, 0 );   // 警告を出さないために、とりあえず0をセットした。
		if ( Input.GetKey( KeyCode.A ) )
		{
			// ゲーム開始直後、前進させるため
			if ( speedGear == 0 )
				speedGear = 1;

			if ( speedGear == 1 )
			{
				spd = transform.forward * WALK;
				speed = WALK;
			}
			else if ( speedGear == 2 )
			{
				spd = transform.forward * RUN;
				speed = RUN;
			}
		}
		else if ( Input.GetKey( KeyCode.Z ) )
		{
			spd = transform.forward * (-WALK); // 後退
			speed = WALK;
		}
		else
		{
			spd = new Vector3( 0, 0, 0 );      // 停止
			speed = 0.0f;
		}
		// 縦方向の速度を復元する
		myRigidbody.velocity = spd;
		myRigidbody.velocity += v;              

		//コックピットを揺らす
		if ( speed == 0.0f )
		{
			elipseTime = 0.0f;
		}
		else if ( speed == WALK )
		{
			Vector3 rect;
			rect.x = 20.0f * Mathf.Sin( 3.141592f / 180.0f * elipseTime * 180.0f * 1.5f ) - 140.0f;
			ry = 20.0f * Mathf.Cos( 3.141592f / 180.0f * elipseTime * 180.0f * 1.5f );
			if ( ry < 0.0f )
				ry *= -1.0f;
			rect.y = ry;
			rect.z = 0.0f;
			cockpitPanel.GetComponent<Transform>().position = rect;
		}
		else if ( speed == RUN )
		{
			Vector3 rect;
			rect.x = 20.0f * Mathf.Sin( 3.141592f / 180.0f * elipseTime * 180.0f * 2.5f ) - 140.0f;
			ry = 20.0f * Mathf.Cos( 3.141592f / 180.0f * elipseTime * 180.0f * 2.5f );
			if ( ry < 0.0f )
				ry *= -1.0f;
			rect.y = ry;
			rect.z = 0.0f;
			cockpitPanel.GetComponent<Transform>().position = rect;
		}

		// 武器選択（チャタリング防止）
		if ( Input.GetKey(KeyCode.C)) {
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
					// キャノン砲（右ランチャーから発射）
					if ( weaponEnergy < 5)
						break;
					if (bulletFlag == false)
					{
						bulletFlag = true;
						GameObject weapon1 = Instantiate(weapon1Prefab) as GameObject;
						weapon1.transform.position = launcherR.transform.position;
						weapon1.transform.rotation = myCamera.transform.rotation;
						weaponEnergy -= 5;		// 発射にW.Eが5を消費
					}
					break;
				case 1:
					// ミサイル（左ランチャーから発射）
					if ( weaponEnergy < 15)
						break;
					if (bulletFlag == false)
					{
						bulletFlag = true;
						GameObject weapon2 = Instantiate(weapon2Prefab) as GameObject;
						weapon2.transform.position = launcherL.transform.position;
						weapon2.transform.rotation = myCamera.transform.rotation;
						weaponEnergy -= 15;    // 発射にW.Eが15を消費
					}
					break;
				case 2:
					// 機関銃（連射OK、胴体中心から、左右交互に発射）
					if ( weaponEnergy < 1)
						break;
					if (mgCount == 0.0f)
					{
						GameObject weapon3 = Instantiate(weapon3Prefab) as GameObject;
						if (gunSide == 1)
							weapon3.transform.position = gunL.transform.position;
						else if (gunSide == -1)
							weapon3.transform.position = gunR.transform.position;
						weapon3.transform.rotation = myCamera.transform.rotation;
						gunSide *= -1;
						weaponEnergy -= 1;  // 発射にW.Eが1を消費
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

		// 残りの弾が0になったらオープニング画面に戻る(2nd Stage)
		if ( weaponEnergy  == 0 )
			SceneManager.LoadScene( "Title" );
	}

	//******************************************************************************************
	//	OnTriggerEnter
	// [引数]
	// [戻り値]
	//	1stステージのみ
	//******************************************************************************************
	void OnTriggerEnter( Collider other )
	{
		// ゴールラインを通過した(1st Stage)
		if ( other.gameObject.tag == "tagGoal" )
		{
			messageText.GetComponent<Text>().text = "Goal !!!";
			GameObject.Find( "GameControl" ).GetComponent<GameController_1st>().Goal();
		}
	}

	//******************************************************************************************
	//	ゲーム開始を許可する
	// [引数]
	// [戻り値]
	//	1stステージのみ
	//******************************************************************************************
	public void StartGame()
	{
		startFlag = true;
	}

	public void InputLeftButtonDown()
	{
		Debug.Log( "LEFT" );
	}

	public void InputRightButtonDown()
	{
		Debug.Log( "RIGHT" );

	}
}
