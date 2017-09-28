using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : MonoBehaviour {

	private AudioSource sound;
	
	// Use this for initialization
	void Start () {

		// 爆発音を再生
		sound = GetComponent<AudioSource>();
		sound.Play();

		// 5秒後にオブジェクトを削除
		Destroy( gameObject , 5.0f );	// <---  消えない！
	}

	// Update is called once per frame
	void Update () {
		
	}
}
