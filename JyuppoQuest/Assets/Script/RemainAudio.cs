using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RemainAudio : MonoBehaviour {

	#region Singleton

	private static RemainAudio instance;

	public static RemainAudio Instance {
		get {
			if (instance == null) {
				instance = (RemainAudio)FindObjectOfType (typeof(RemainAudio));

				if (instance == null) {
					Debug.LogError (typeof(RemainAudio) + "is nothing");
				}
			}

			return instance;
		}
	}

	#endregion Singleton

	public bool DontDestroyEnabled = true;

	public AudioClip itemSE;
	public AudioClip footSE;
	public AudioClip warpSE;
	public AudioClip attackSE;
	public AudioClip defenseSE;
	public AudioClip healSE;
	public AudioClip winSE;
	public AudioClip loseSE;
	public AudioClip bosswinSE;
	public AudioClip bossloseSE;
	public AudioClip downSE;

    public void Awake ()
	{
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}

		DontDestroyOnLoad (this.gameObject);
	}

	public void ChangeBgm(int num){
		AudioSource[] audio;
		audio = this.GetComponents<AudioSource>();

		int len = audio.Length;
		for(int i=0;i<len;i++){
			audio[i].enabled = false;
		}
		audio[num].enabled = true;

	}

	public void Play(){
		Debug.Log("Play");
		AudioSource[] audio = GetComponents<AudioSource>();
		int n = audio.Length;
		int target = 0;
		for(int i=0;i<n;i++){
			if(audio[i].enabled)target = i;
		}
		audio[target].Play();	
	}

	public void Pause(){
		Debug.Log("Pause");
		AudioSource[] audio = GetComponents<AudioSource>();
		int n = audio.Length;
		int target = 0;
		for(int i=0;i<n;i++){
			if(audio[i].enabled)target = i;
		}
		audio[target].Pause();	
	}

	public void Stop(){
		Debug.Log("Stop");
		AudioSource[] audio = GetComponents<AudioSource>();
		int n = audio.Length;
		int target = 0;
		for(int i=0;i<n;i++){
			if(audio[i].enabled)target = i;
		}
		audio[target].Stop();	
	}

	public void PlaySE(string str){
		AudioSource[] audio = GetComponents<AudioSource>();
		int n = audio.Length;
		int target = 0;
		for(int i=0;i<n;i++){
			if(audio[i].enabled)target = i;
		}
		switch(str){
		case "item":
			audio[target].PlayOneShot(itemSE,2);
			break;
		case "foot":
			StartCoroutine(DelayMethod(0.2f, () => {
				audio[target].PlayOneShot(footSE,1.5f);
			}));
			StartCoroutine(DelayMethod(1.0f, () => {
				audio[target].PlayOneShot(footSE,1.5f);
			}));
			break;
		case "warp":
			audio[target].PlayOneShot(warpSE,1.5f);
			break;
		case "attack":
			audio[target].PlayOneShot(attackSE,1.0f);
			break;
		case "defense":
			audio[target].PlayOneShot(defenseSE,1.0f);
			break;
		case "heal":
			audio[target].PlayOneShot(healSE,1.0f);
			break;
		case "win":
			audio[target].PlayOneShot(winSE,2.0f);
			break;
		case "lose":
			audio[target].PlayOneShot(loseSE,2.0f);
			break;
		case "bosswin":
			audio[target].PlayOneShot(bosswinSE,2.5f);
			break;
		case "bosslose":
			audio[target].PlayOneShot(bossloseSE,0.8f);
			break;
		case "down":
			audio[target].PlayOneShot(downSE);
			break;
		default:
			Debug.Log("そんなSEありません");
			break;
		}
	}

	private IEnumerator DelayMethod(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}
