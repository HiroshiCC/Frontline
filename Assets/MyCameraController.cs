using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour
{
	float pitchAngle;       // カメラのピッチ角管理用
	bool startFlag;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start()
	{
		startFlag = false;
	}

	//******************************************************************************************
	//	Update
	// [引数]
	// [戻り値]
	// [コメント]
	//	可動範囲を制限。Rotate()した角度の積算をpitchAngleで管理する。
	//******************************************************************************************
	void Update()
	{
		if ( startFlag == false )
			return;

		// カメラのみ、上を向く
		if ( Input.GetKey( KeyCode.DownArrow ) )
		{
			if ( pitchAngle > -30.0f )
			{
				transform.Rotate( new Vector3( -50.0f * Time.deltaTime, 0.0f, 0.0f ) );
				pitchAngle += (-50.0f) * Time.deltaTime;
			}
		}

		// カメラのみ、下を向く
		if ( Input.GetKey( KeyCode.UpArrow ) )
		{
			if ( pitchAngle < 30.0f )
			{
				transform.Rotate( new Vector3( 50.0f * Time.deltaTime, 0.0f, 0.0f ) );
				pitchAngle += (50.0f * Time.deltaTime);
			}
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
}

