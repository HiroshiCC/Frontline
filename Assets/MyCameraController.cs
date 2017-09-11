using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour {

    //Unityちゃんのオブジェクト
    private GameObject myRobot;
	//private GameObject myCamera;

    
    // Use this for initialization
    void Start () {

        // 自分のロボットのオブジェクトを取得
        this.myRobot = GameObject.Find("MyRobot");
		//this.myCamera = GameObject.Find("Main Camera");
		
     }

    // Update is called once per frame
    void Update () {

		// 左に向く
		if (Input.GetKey(KeyCode.LeftArrow))
			//transform.localEulerAngles.Set(0, 2, 0);
			transform.Rotate(new Vector3(0, -2));

		// 右に向く
		if (Input.GetKey(KeyCode.RightArrow))
			//transform.localEulerAngles.Set(0, -2, 0);
			transform.Rotate(new Vector3(0, 2));


		// カメラのみ、上を向く
		if (Input.GetKey(KeyCode.DownArrow))
			//transform.localEulerAngles.Set(-20, 0 , 0);
			transform.Rotate( new Vector3(-2, 0,0));

        // カメラのみ、下を向く
        if (Input.GetKey(KeyCode.UpArrow))
			//transform.localEulerAngles.Set(20, 0, 0);
			transform.Rotate( new Vector3(2, 0,0));

		// 自分のロボットの位置に合わせてカメラの位置を移動
		this.transform.position = new Vector3(this.myRobot.transform.position.x, this.myRobot.transform.position.y+3, this.myRobot.transform.position.z);
	}
}
