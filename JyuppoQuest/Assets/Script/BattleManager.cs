using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BattleManager : MonoBehaviour {

	public GameObject attack;
	public GameObject attackenemy;
	public GameObject commandArea;
	public GameObject attackArea;
	public GameObject textWinLose;

	public GameObject hpHero;
	public GameObject attackHero;
	public GameObject hpEnemy;
	public GameObject attackEnemy;

	private int turn = 0;

	private void Start(){
		hpHero.GetComponent<Text>().text = PlayerPrefs.GetInt("hp").ToString();
		attackHero.GetComponent<Text>().text = PlayerPrefs.GetInt("attack").ToString();
	}

	public void BattleStart(){
		commandArea.SetActive(false);
		attackenemy.SetActive(true);

		Invoke("Action",1.0f);
	}

	private void Action(){
		attackenemy.transform.GetChild(turn * 2).gameObject.GetComponent<Image>().sprite
			= attackArea.GetComponent<CommandImage>()
			.commandSprite[PlayerPrefs.GetInt("enemy" + (turn+1).ToString())];
		turn++;

		Battle();

		if(PlayerPrefs.GetInt("hp") <= 0){
			//heroの負け
		}else if(PlayerPrefs.GetInt("enemyhp") <= 0){
			//heroの勝ち
			textWinLose.SetActive(true);
			iTween.MoveFrom(textWinLose,iTween.Hash("x",-100));
		}else if(turn < 10){
			Invoke("Action",1.5f);
		}
	}

	private void HeroAttack(){
		int attack = PlayerPrefs.GetInt("attack");
		int hp = PlayerPrefs.GetInt("enemyhp");
		hp -= attack;
		if(hp < 0)hp = 0;
		hpEnemy.GetComponent<Text>().text = hp.ToString();
		PlayerPrefs.SetInt("enemyhp",hp);

		GameObject textAnim = hpEnemy.transform.GetChild(0).gameObject;
		textAnim.SetActive(true);
		textAnim.GetComponent<Text>().color = Color.blue;
		textAnim.GetComponent<Text>().text = "-" + attack.ToString() + "↓";
		StartCoroutine(DelayMethod(1.0f, () =>
		{
			textAnim.SetActive(false);
		}));
	}

	private void EnemyAttack(){
		int attack = PlayerPrefs.GetInt("enemyattack");
		int hp = PlayerPrefs.GetInt("hp");
		hp -= attack;
		if(hp < 0)hp = 0;
		hpHero.GetComponent<Text>().text = hp.ToString();
		PlayerPrefs.SetInt("hp",hp);

		GameObject textAnim = hpHero.transform.GetChild(0).gameObject;
		textAnim.SetActive(true);
		textAnim.GetComponent<Text>().color = Color.blue;
		textAnim.GetComponent<Text>().text = "-" + attack.ToString() + "↓";
		StartCoroutine(DelayMethod(1.0f, () =>
		{
			textAnim.SetActive(false);
		}));
	}

	private void Battle(){
		//行動
		int hero = PlayerPrefs.GetInt("hero"+turn.ToString());
		int enemy = PlayerPrefs.GetInt("enemy"+turn.ToString());

		if(hero == 0){
			if(enemy == 0){
				//攻撃攻撃
				HeroAttack();
				EnemyAttack();
			}else if(enemy == 1){
				//攻撃防御
			}else if(enemy == 2){
				//攻撃回復
			}else if(enemy == 4){
				//攻撃休憩
				HeroAttack();
			}
		}else if(hero == 1){
			if(enemy == 0){
				//防御攻撃
			}else if(enemy == 1){
				//防御防御
			}else if(enemy == 2){
				//防御回復
			}else if(enemy == 4){
				//防御休憩
			}
		}else if(hero == 2){
			if(enemy == 0){
				//回復攻撃
			}else if(enemy == 1){
				//回復防御
			}else if(enemy == 2){
				//回復回復	
			}	else if(enemy == 4){
				//回復休憩
			}
		}
	}

	private IEnumerator DelayMethod(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}
