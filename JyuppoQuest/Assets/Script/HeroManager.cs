using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HeroManager : MonoBehaviour {

	public GameObject textFoot; //残り歩数テキスト
	
	//現在のステイトを保持
	private string state = "stop";
	private bool isMove = false;

	private float timeMove = 1.5f; //移動時間
	private float disMove = 2f; //移動距離

	private void Start(){

	}

	void Update () {
		if(PlayerPrefs.GetInt("foot") == 0) return;
		if(PlayerPrefs.GetInt("canMove") == 0)return;
		if(state == "move")return; //もし移動中だったら入力を受け付けない
		
		Vector3 targetPos = new Vector3(0,0,0);

		//右
		if(Input.GetKey(KeyCode.P) && (PlayerPrefs.GetInt("posx") != 8)){
			isMove = true;
			PlayerPrefs.SetInt("posx",PlayerPrefs.GetInt("posx") + 2);
			//回転
			targetPos = new Vector3(transform.position.x + disMove,transform.position.y, transform.position.z);
		}
		//左
		if(Input.GetKey(KeyCode.L) && (PlayerPrefs.GetInt("posx") != 0)){
			isMove = true;
			PlayerPrefs.SetInt("posx",PlayerPrefs.GetInt("posx") - 2);
			//回転
			targetPos = new Vector3(transform.position.x - disMove,transform.position.y, transform.position.z);
		}
		//上
		if(Input.GetKey(KeyCode.O) && (PlayerPrefs.GetInt("posz") != 8)){
			isMove = true;
			PlayerPrefs.SetInt("posz",PlayerPrefs.GetInt("posz") + 2);
			//回転
			targetPos = new Vector3(transform.position.x,transform.position.y, transform.position.z + disMove);
		}
		//下
		if(Input.GetKey(KeyCode.Semicolon) && (PlayerPrefs.GetInt("posz") != 0)){
			isMove = true;
			PlayerPrefs.SetInt("posz",PlayerPrefs.GetInt("posz") - 2);
			//回転
			targetPos = new Vector3(transform.position.x,transform.position.y, transform.position.z - disMove);
		}

		if(isMove){
			RemainAudio.Instance.PlaySE("foot");
			
			int foot = PlayerPrefs.GetInt("foot") - 1;
			textFoot.GetComponent<Text>().text = foot.ToString();
			PlayerPrefs.SetInt("foot",foot);

			isMove = false;
			GetComponent<Animator> ().SetBool ("isWalk",true);
			state = "move";
			Invoke("Stop",timeMove);

			//回転
			Vector3 relativePos = targetPos - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			transform.rotation = rotation;

			iTween.MoveTo(gameObject, iTween.Hash("x",targetPos.x,"z",targetPos.z,"time",timeMove,
				"EaseType",iTween.EaseType.linear,"oncomplete","CompleteMove","oncompletetarget",gameObject));
			

		}
		
	}

	private void CompleteMove(){
		if(PlayerPrefs.GetInt("isScreenChange") == 0 && PlayerPrefs.GetInt("foot") == 0){
			StartCoroutine(DelayMethod(1.5f, () =>
			{
				//ボス戦
				FadeManager.Instance.LoadScene ("BossBattleScene", 2.0f);
				RemainAudio.Instance.ChangeBgm(2);
			}));
		}
	}

	private void Stop(){
		state = "stop";
		if(PlayerPrefs.GetInt("isScreenChange") == 1)state = "move";
		GetComponent<Animator> ().SetBool ("isWalk",false);
		if(PlayerPrefs.GetInt("foot") == 0){
			//ボス戦突入

		}
	}

	private IEnumerator DelayMethod(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}
