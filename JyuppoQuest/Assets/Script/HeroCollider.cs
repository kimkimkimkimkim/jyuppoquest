using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HeroCollider : MonoBehaviour {

	/*
		{
			"foot", int 残りの歩数
			"hp", int HP
			"attack", int 攻撃力
		}
	 */

	public GameObject textFoot;
	public GameObject textHealth;
	public GameObject textAttack;

	public GameObject fadeManager;
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
    {	
        if(col.CompareTag("Enemy01")){
			//Destroy(col.gameObject);
			//int health = PlayerPrefs.GetInt("hp") - 20;
			//textHealth.GetComponent<Text>().text = health.ToString();
			//PlayerPrefs.SetInt("hp",health);

			//画面遷移
			FadeManager.Instance.LoadScene ("BattleScene", 2.0f);
		}
		if(col.CompareTag("ItemRed")){
			Destroy(col.gameObject);
			int plus = 5;
			int attack = PlayerPrefs.GetInt("attack") + plus;
			textAttack.GetComponent<Text>().text = attack.ToString();
			PlayerPrefs.SetInt("attack",attack);

			GameObject textAnim = textAttack.transform.GetChild(0).gameObject;
			textAnim.SetActive(true);
			textAnim.GetComponent<Text>().color = Color.red;
			textAnim.GetComponent<Text>().text = "+" + plus.ToString() + "↑";
			StartCoroutine(DelayMethod(1.5f, () =>
			{
				textAnim.SetActive(false);
			}));
		}
		if(col.CompareTag("ItemBlue")){
			Destroy(col.gameObject);
			int plus = 5;
			int attack = PlayerPrefs.GetInt("attack") - plus;
			textAttack.GetComponent<Text>().text = attack.ToString();
			PlayerPrefs.SetInt("attack",attack);

			GameObject textAnim = textAttack.transform.GetChild(0).gameObject;
			textAnim.SetActive(true);
			textAnim.GetComponent<Text>().color = Color.blue;
			textAnim.GetComponent<Text>().text = "-" + plus.ToString() + "↓";
			StartCoroutine(DelayMethod(1.5f, () =>
			{
				textAnim.SetActive(false);
			}));
		}
    }

	private IEnumerator DelayMethod(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}
