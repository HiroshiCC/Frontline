using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject[] startPos = new GameObject[8];
	public GameObject enemySPrefab;
	public GameObject enemyMPrefab;
	public GameObject enemyLPrefab;

	private GameObject[] enemyData = new GameObject[15];	// 最大15の敵が存在できる
	private GameObject myRobot;

	float deltaT = 0.0f;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start ()
	{
		int i;

		for ( i = 0 ;i< 15;i++)
		{
			enemyData[i] = null;
		}

		myRobot = GameObject.Find( "MyRobot" );
	}

	//******************************************************************************************
	//	Update
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Update ()
	{
		int posNum;
		int kindNum;
		int ptr = 0;
		bool flag;
		int i;
		int j;

		deltaT += Time.deltaTime;
		if ( deltaT < 15.0f )
			return;	// １５秒経過していない間は、出現させない
		deltaT = 0.0f;

		for ( i = 0 ; i < 2 ; i++ )
		{
			// 敵データの格納先を探す
			flag = false;
			for ( j = 0 ; j < 15 ; j++ )
			{
				if ( enemyData[j] == null )
				{
					ptr = j;
					flag = true;
					break;
				}

			}
			if ( flag == false )
				return;     // 空きがないので、敵を出現させない

			// 発生する敵の種類
			kindNum = Random.Range( 1, 4 );

			switch ( kindNum )
			{
				case 1:
					enemyData[ptr] = Instantiate(enemySPrefab) as GameObject;
					// 目的地セット
					enemyData[ptr].GetComponent<EnemySController>().SetTargetPos( myRobot );
					break;
				case 2:
					enemyData[ptr] = Instantiate( enemyMPrefab ) as GameObject;
					// 目的地セット
					enemyData[ptr].GetComponent<EnemyMController>().SetTargetPos( myRobot );
					break;
				case 3:
					enemyData[ptr] = Instantiate( enemyLPrefab ) as GameObject;
					// 目的地セット
					enemyData[ptr].GetComponent<EnemyLController>().SetTargetPos( myRobot );
					break;
				default:
					break;
			}
			// 敵発生位置
			posNum = Random.Range( 0, 8 );
			enemyData[ptr].transform.position = startPos[posNum].transform.position;
			enemyData[ptr].transform.rotation = startPos[posNum].transform.rotation;
		}
	}
}
