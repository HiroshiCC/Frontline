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
	float waitTime;
	int DriveMode=0;
	bool fire = false;
	bool dragFlag = false;

	// UIテキスト関連
	GameObject weaponNameText;
	GameObject weaponEnergyText;
	GameObject messageText;
	float elipseTime = 0.0f;

	// スティック操作計算用
	float offX = 1.0f;
	float offY = 1.0f;
	float sizeX = 360.0f;
	float sizeY = 360.0f;
	float dX = 30.0f;
	float dY = 30.0f;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start ()
	{
		weaponEnergy = 500 ;

		waitTime = 0.0f;
		weaponName = new []{ "GUN", "MSL", "M/G" };
		myRigidbody = GetComponent<Rigidbody>();
		speed = 0.0f;

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
		messageText.GetComponent<Text>().text = "";
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
		if ( Input.GetKey( KeyCode.Alpha1 ) )
			DriveMode = 2;		// 走る

		if ( Input.GetKey( KeyCode.Q ) )
			DriveMode = 1;		// 歩く
		
		if ( Input.GetKey( KeyCode.A ) )
			DriveMode = 0;		// 停止する

		if ( Input.GetKey( KeyCode.Z ) )
			DriveMode = -1;     // バックする

		//if ( Input.GetKey( KeyCode.Space ) )
		//	FireOn();
		//else
		//{
		//	if ( fire == true)
		//		FireOff();
		//}

		if ( Input.GetKey( KeyCode.C ) )
			InputSEL();

		// 縦方向の速度を保持する
		Vector3 v = myRigidbody.velocity;
		v.x = 0.0f;
		v.z = 0.0f;

		Vector3 spd = new Vector3( 0, 0, 0 );   // 警告を出さないために、とりあえず0をセットした。
		switch ( DriveMode )
		{
			case 0:
				spd = new Vector3( 0, 0, 0 );      // 停止
				speed = 0.0f;
				break;
			case 1:
				spd = transform.forward * WALK;
				speed = WALK;
				break;
			case 2:
				spd = transform.forward * RUN;
				speed = RUN;
				break;
			case -1:
				spd = transform.forward * (-WALK); // 後退
				speed = WALK;
				break;
			default:
				break;
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
			rect.x = 20.0f * Mathf.Sin( 3.141592f / 180.0f * elipseTime * 180.0f * 1.5f ) - 210.0f;
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
			rect.x = 20.0f * Mathf.Sin( 3.141592f / 180.0f * elipseTime * 180.0f * 2.5f ) - 210.0f;
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

		// 残りの弾が0になったら、５秒後にオープニング画面に戻る(2nd Stage)
		if ( weaponEnergy == 0 )
		{
			waitTime += Time.deltaTime;
			if ( waitTime > 5.0f )
				SceneManager.LoadScene( "Title" );
		}
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


	//******************************************************************************************
	//	武器を切り替える
	// [引数]
	// [戻り値]
	//******************************************************************************************
	public void InputSEL()
	{
		if ( startFlag == false )
			return;

		if ( selectFlag == false )
		{
			selectFlag = true;
			kindOfWeapon++;
			kindOfWeapon %= 3;
			// 兵器名の表示更新
			weaponNameText.GetComponent<Text>().text = weaponName[kindOfWeapon];
		}
	}

	//******************************************************************************************
	//	走る
	// [引数]
	// [戻り値]
	//******************************************************************************************
	public void InputRUN()
	{
		if ( startFlag == false )
			return;

		DriveMode = 2;
	}

	//******************************************************************************************
	//	歩く
	// [引数]
	// [戻り値]
	//******************************************************************************************
	public void InputWALK()
	{
		if ( startFlag == false )
			return;

		DriveMode = 1;
	}

	//******************************************************************************************
	//	停止する
	// [引数]
	// [戻り値]
	//******************************************************************************************
	public void InputSTOP()
	{
		if ( startFlag == false )
			return;

		DriveMode = 0;
	}

	//******************************************************************************************
	//	バックする
	// [引数]
	// [戻り値]
	//******************************************************************************************
	public void InputBACK()
	{
		if ( startFlag == false )
			return;

		DriveMode = -1;
	}

	//******************************************************************************************
	//	スティック操作　開始
	// [引数]
	// [戻り値]
	//******************************************************************************************
	public void InputSTICK_DrugBegin()
	{
			dragFlag = true;
	}

	//******************************************************************************************
	//	スティック操作　横方向
	// [引数]
	//	float mouseX		: 横の操作量
	// [戻り値]
	//	最大値に対する割合(0.0～1.0)
	//******************************************************************************************
	private float CalcX( float mouseX )
	{
		if ( mouseX > (sizeX / 2.0f + dX) )
			mouseX = mouseX - (sizeX / 2.0f + dX);
		else if ( mouseX > (sizeX / 2.0f - dX) )
			mouseX = 0.0f;
		else if ( mouseX <= sizeX / 2.0f - dX )
			mouseX = mouseX - (sizeX / 2.0f - dX);

		mouseX = mouseX / (sizeX / 2.0f - dX);

		return mouseX;
	}

	//******************************************************************************************
	//	スティック操作　縦方向
	// [引数]
	//	float mouseX		: 縦の操作量
	// [戻り値]
	//	最大値に対する割合(0.0～1.0)
	//******************************************************************************************
	private float CalcY( float mouseY )
	{
		if ( mouseY > (sizeY / 2.0f + dY) )
			mouseY = mouseY - (sizeY / 2.0f + dY);
		else if ( mouseY > (sizeY / 2.0f - dY) )
			mouseY = 0.0f;
		else if ( mouseY <= sizeY / 2.0f - dY )
			mouseY = mouseY - (sizeY / 2.0f - dY);

		mouseY = mouseY / (sizeY / 2.0f - dY);

		return mouseY;
	}

	//******************************************************************************************
	//	スティック操作　操作終了
	// [引数]
	// [戻り値]
	//******************************************************************************************
	public void InputSTICK_DrugEnd()
	{
		dragFlag = false;
	}

	//******************************************************************************************
	//	スティック操作　スティックエリアから外れた
	// [引数]
	// [戻り値]
	//******************************************************************************************
	public void InputSTICK_Exit()
	{
		//dragFlag = false;
	}

	//******************************************************************************************
	//	スティック操作　スティックエリアに再侵入
	// [引数]
	// [戻り値]
	//******************************************************************************************
	public void InputSTICK_Enter()
	{
		//dragFlag = true;
	}

	//******************************************************************************************
	//	OnGUI
	// [引数]
	// [戻り値]
	//******************************************************************************************
	private void OnGUI()
	{
		if ( startFlag == false )
			return;

		// 発砲処理
		if ( fire == true )
		{
			Debug.Log( "Fire!!!" );

			switch ( kindOfWeapon )
			{
				case 0:
					// キャノン砲（右ランチャーから発射）
					if ( weaponEnergy < 5 )
						break;
					if ( bulletFlag == false )
					{
						bulletFlag = true;
						GameObject weapon1 = Instantiate(weapon1Prefab) as GameObject;
						weapon1.transform.position = launcherR.transform.position;
						weapon1.transform.rotation = myCamera.transform.rotation;
						weaponEnergy -= 5;      // 発射にW.Eが5を消費
					}
					break;
				case 1:
					// ミサイル（左ランチャーから発射）
					if ( weaponEnergy < 15 )
						break;
					if ( bulletFlag == false )
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
					if ( weaponEnergy < 1 )
						break;
					if ( mgCount == 0.0f )
					{
						GameObject weapon3 = Instantiate(weapon3Prefab) as GameObject;
						if ( gunSide == 1 )
							weapon3.transform.position = gunL.transform.position;
						else if ( gunSide == -1 )
							weapon3.transform.position = gunR.transform.position;
						weapon3.transform.rotation = myCamera.transform.rotation;
						gunSide *= -1;
						weaponEnergy -= 1;  // 発射にW.Eが1を消費
					}
					// 連射は0.15秒に1発
					mgCount += Time.deltaTime;
					if ( mgCount > 0.25f )
						mgCount = 0.0f;
					break;
				default:
					break;
			}
			weaponEnergyText.GetComponent<Text>().text = "W.E " + weaponEnergy;    // 兵器エネルギー
		}
		else
		{
			bulletFlag = false;
			mgCount = 0.0f;
		}

		// スティック処理
		if ( dragFlag == true )
		{
			float mouseX = Input.mousePosition.x - offX;
			float mouseY = Input.mousePosition.y - offY;

			// 移動スピードによって、旋回速度が変わる
			if ( speed == 0.0f )
				hdgSpeed = 80.0f;
			else if ( speed == WALK )
				hdgSpeed = 50.0f;
			else if ( speed == RUN )
				hdgSpeed = 30.0f;

			mouseY = CalcY( mouseY );
			GameObject.Find( "Main Camera" ).GetComponent<MyCameraController>().SetPitch( mouseY );

			mouseX = CalcX( mouseX );
			transform.Rotate( 0, hdgSpeed * mouseX * Time.deltaTime, 0 );

		}
	}

	//******************************************************************************************
	//	FIREボタン　押下
	// [引数]
	// [戻り値]
	//******************************************************************************************
	public void FireOn()
	{
		fire = true;

		Debug.Log( "FireON" );
	}

	//******************************************************************************************
	//	FIREボタン　解放
	// [引数]
	// [戻り値]
	//******************************************************************************************
	public void FireOff()
	{
		fire = false;

		Debug.Log( "FireOFF" );
	}
}

