using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRobotController : MonoBehaviour {

    private GameObject myCamera;
    private Rigidbody myRigidbody;

    // Use this for initialization
    void Start () {
        myCamera = GameObject.Find("Main Camera");
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        // 左に向く
        if (Input.GetKey(KeyCode.LeftArrow))
             transform.Rotate(0, -2, 0);
 
        // 右に向く
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(0, 2, 0);

		// 前進
		if (Input.GetKey(KeyCode.A))
			myRigidbody.AddForce(transform.forward * 100f );


		// 後退
		if (Input.GetKey(KeyCode.Z))
            myRigidbody.AddForce(-transform.forward * 100f );


		// カメラの位置は、MyCameraController.csで処理
	}
}
