using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour {

	float maxAngle = 30;    // 最大回転角度
	float minAngle = -45;   // 最小回転角度
	float speed = 1.0f;     // 回転スピード
	int pithCount;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start () {
		
     }

	//******************************************************************************************
	//	Update
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Update () {

		// カメラのみ、上を向く
		if ( Input.GetKey( KeyCode.DownArrow ) )
		{
			transform.Rotate( new Vector3( -50.0f * Time.deltaTime, 0.0f, 0.0f ) );
			//PitchLimit();

		}

		// カメラのみ、下を向く
		if ( Input.GetKey( KeyCode.UpArrow ) )
		{
			transform.Rotate( new Vector3( 50.0f * Time.deltaTime, 0.0f, 0.0f ) );
			//PitchLimit();
		}
	}

	//******************************************************************************************
	//	カメラのピッチ角を制限する
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void PitchLimit()
	{
		// ピッチ角を制限
		float turn = Input.GetAxis("Horizontal");

		// 現在の回転角度を0～360から-180～180に変換
		float rotateX = (transform.eulerAngles.x > 180) ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;

		// 現在の回転角度に入力(turn)を加味した回転角度をMathf.Clamp()を使いminAngleからMaxAngle内に収まるようにする
		float angleX = Mathf.Clamp(rotateX + turn * speed, minAngle, maxAngle);

		// 回転角度を-180～180から0～360に変換
		angleX = (angleX < 0) ? angleX + 360 : angleX;

		// 回転角度をオブジェクトに適用
		transform.rotation = Quaternion.Euler( angleX, 0.0f, 0.0f );
	}
}





