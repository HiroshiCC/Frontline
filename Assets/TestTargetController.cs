using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTargetController : MonoBehaviour
{
	public GameObject ExplosionPrefab;

	//private AudioSource sound;

	//******************************************************************************************
	//	Start
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Start()
	{

	}

	//******************************************************************************************
	//	Update
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void Update()
	{

	}

	//******************************************************************************************
	//	OnCollisionEnter
	// [引数]
	// [戻り値]
	// [コメント]
	//******************************************************************************************
	void OnCollisionEnter(Collision other)
	{
		if ( (other.gameObject.tag == "tagCANON") || (other.gameObject.tag == "tagMISSILE") || (other.gameObject.tag == "tagMACHINEGUN") )
		{
			Destroy(gameObject);

			// 爆発のassetを実行
			GameObject explosion = Instantiate(ExplosionPrefab) as GameObject;
			explosion.transform.position = transform.position;

			// 爆発音を設定・再生
			//sound = GetComponent<AudioSource>();
			//sound.Play();

		}
	}
}
