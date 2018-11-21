using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandScript : MonoBehaviour {

	public int num;

	private GameObject attack;
	private GameObject battleManager;

	private void Start(){
		PlayerPrefs.SetInt("turn",0);
		battleManager = GameObject.Find("BattleManager");
		attack = transform.parent.parent.Find("Attack").gameObject;
	}
	/*
		{
			"hero1", int 行動　0→攻撃　１→防御 2→回復
		}
	 */
	public void OnClick(){
		if(num <= 2){
			int turn = PlayerPrefs.GetInt("turn",0);
			attack.transform.GetChild(turn * 2).gameObject.GetComponent<Image>().sprite 
				= transform.parent.GetComponent<CommandImage>().commandSprite[num];
			PlayerPrefs.SetInt("turn",turn + 1);
			PlayerPrefs.SetInt("hero" + (turn+1).ToString(),num);
		}
		if(num == 3){
			int turn = PlayerPrefs.GetInt("turn",0) - 1;
			attack.transform.GetChild(turn * 2).gameObject.GetComponent<Image>().sprite 
				= transform.parent.GetComponent<CommandImage>().commandSprite[num];
			PlayerPrefs.SetInt("turn",turn);
		}
		if(num == 4){
			int turn = PlayerPrefs.GetInt("turn",0);
			if(turn != 10)return;
			//攻撃
			Debug.Log("攻撃");
			battleManager.GetComponent<BattleManager>().BattleStart();
		}

	}
}
