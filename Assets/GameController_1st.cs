using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_1st : MonoBehaviour {

	float elipseTime = 0.0f;
	int sec;
	int minute;
	GameObject timePrint;
	bool flag;

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
	}

	//******************************************************************************************
	//	Update
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Update ()
	{
		if ( flag == false )
		{
			// タイムカウント
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
	//******************************************************************************************
	public void Goal()
	{
		Debug.Log( "Goal in!" );

		flag = true;
	}
}
