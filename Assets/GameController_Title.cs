﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController_Title : MonoBehaviour {

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
		
	}

	//******************************************************************************************
	//	TimeAttackボタンクリック
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	public void TimeAttackButtonDown()
	{
		SceneManager.LoadScene( "Frontline" );
	}

	//******************************************************************************************
	//	Survivalボタンクリック
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	public void SurvivalButtonDown()
	{
		SceneManager.LoadScene( "Stage2" );
	}
}
