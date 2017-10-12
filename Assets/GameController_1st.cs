using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController_1st : MonoBehaviour {

	float elipseTime = 0.0f;
	int sec;
	int minute;
	GameObject timePrint;
	GameObject msgText;
	bool flag;
	float timer;
	bool countdownFlag=true;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start ()
	{
		flag = false;
		sec = 0;
		minute = 0;
		timePrint = GameObject.Find( "TimeText" );
		msgText = GameObject.Find( "MsgText" );
	}

	//******************************************************************************************
	//	Update
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Update ()
	{
		// ゲーム終了後の時間の確保
		if ( flag == true )
		{
			timer += Time.deltaTime;
			if ( timer > 5.0f )
			{
				// タイトル画面に戻る
				SceneManager.LoadScene( "Title" );
			}
			return;
		}

		// スタートのカウントダウン
		if ( countdownFlag == true )
		{
			timer += Time.deltaTime;

			if ( timer < 1.0f )
			{
				timePrint.GetComponent<Text>().text = "5";
				return;
			}
			else if ( timer < 2.0f )
			{
				timePrint.GetComponent<Text>().text = "4";
				return;
			}
			else if ( timer < 3.0f )
			{
				timePrint.GetComponent<Text>().text = "3";
				return;
			}
			else if ( timer < 4.0f )
			{
				timePrint.GetComponent<Text>().text = "2";
				return;
			}
			else if ( timer < 5.0f )
			{
				timePrint.GetComponent<Text>().text = "1";
				return;
			}
			else if ( timer < 6.0f )
			{
				msgText.GetComponent<Text>().text = "START !!!";
				countdownFlag = false;
				GameObject.Find( "MyRobot" ).GetComponent<MyRobotController>().StartGame();
				GameObject.Find( "Main Camera" ).GetComponent<MyCameraController>().StartGame();
				GameObject.Find( "GameControl2" ).GetComponent<GameController>().StartGame();
				return;
			}
			else
				return;
		}

		// ゲーム中のタイムカウント
		if ( flag == false )
		{
			elipseTime += Time.deltaTime;
			if ( elipseTime > 1.0f )
			{
				elipseTime -= 1.0f;
				sec++;
				if ( sec == 60 )
				{
					sec = 0;
					minute++;
				}
			}
			// タイムの表示更新
			timePrint.GetComponent<Text>().text = minute + ":" + sec + "." + (int)(elipseTime * 100.0f);
		}
	}

	//******************************************************************************************
	//	ゴールした時の処理
	// [引数]
	// [戻り値]
	// [コメント]
	//	1stステージのみ。MyRobotControllerのOnTriggerEnter()から、呼ばれる。
	//******************************************************************************************
	public void Goal()
	{
		// 時間カウントの停止
		flag = true;
		timer = 0;
		
	}
}
