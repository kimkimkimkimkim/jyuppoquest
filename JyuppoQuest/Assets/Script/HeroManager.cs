using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroManager : MonoBehaviour {

	public GameObject textFoot; //残り歩数テキスト
	
	//現在のステイトを保持
	private string state = "stop";
	private bool isMove = false;

	private float timeMove = 1.5f; //移動時間
	private float disMove = 2f; //移動距離

	private void Start(){
		PlayerPrefs.SetInt("foot",10);
		PlayerPrefs.SetInt("hp",100);
		PlayerPrefs.SetInt("attack",10);
	}

	void Update () {
		if(state == "move")return; //もし移動中だったら入力を受け付けない
		
		Vector3 targetPos = new Vector3(0,0,0);

		//右
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
			isMove = true;
			//回転
			targetPos = new Vector3(transform.position.x + disMove,transform.position.y, transform.position.z);
		}
		//左
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
			isMove = true;
			//回転
			targetPos = new Vector3(transform.position.x - disMove,transform.position.y, transform.position.z);
		}
		//右
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
			isMove = true;
			//回転
			targetPos = new Vector3(transform.position.x,transform.position.y, transform.position.z + disMove);
		}
		//右
		if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
			isMove = true;
			//回転
			targetPos = new Vector3(transform.position.x,transform.position.y, transform.position.z - disMove);
		}

		if(isMove){
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

			//移動
			iTween.MoveTo(gameObject, iTween.Hash("x",targetPos.x,"z",targetPos.z,"time",timeMove,
				"EaseType",iTween.EaseType.linear));

		}
		
	}

	private void UpdatePos(){

	}

	private void Stop(){
		state = "stop";
		GetComponent<Animator> ().SetBool ("isWalk",false);
		if(PlayerPrefs.GetInt("foot") == 0){
			//ボス戦突入

		}
	}
}
