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
	// [コメント]
	//******************************************************************************************
	public void StartGame()
	{
		startFlag = true;
	}

	//******************************************************************************************
	//	ピッチ処理
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	public void SetPitch( float mouseY )
	{
		float mY = 50.0f * mouseY * Time.deltaTime;

		//Debug.Log( pitchAngle + "  :  " + mouseY + " : " + mY );

		if ( mY > 0.0f )
		{
			if ( (pitchAngle + mY) > 30.0f )
				mY = 0.0f;
		}
		else
		{
			if ( (pitchAngle + mY) < -30.0f )
				mY = 0.0f;
		}

		pitchAngle += mY;
		transform.Rotate( new Vector3( mY, 0.0f, 0.0f ) );
	}
}

