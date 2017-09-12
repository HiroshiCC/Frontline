using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour {


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
		if (Input.GetKey(KeyCode.DownArrow))
			transform.Rotate( new Vector3(-2, 0,0));

        // カメラのみ、下を向く
        if (Input.GetKey(KeyCode.UpArrow))
			transform.Rotate( new Vector3(2, 0,0));
	}
}
