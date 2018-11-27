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
			"turn", int 
		}
	 */

	public GameObject textFoot;
	public GameObject textHealth;
	public GameObject textAttack;

	public GameObject fadeManager;

	private GameObject remainAudio;

	private void Start(){

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
    {	
        if(col.CompareTag("Enemy01")){

			string id = col.GetComponent<ItemID>().id;
			PlayerPrefs.SetInt(id,0);

			int hp = col.GetComponent<ItemStatus>().hp;
			int attack = col.GetComponent<ItemStatus>().attack;
			int uphp = col.GetComponent<ItemStatus>().uphp;
			int upattack = col.GetComponent<ItemStatus>().upattack;

			PlayerPrefs.SetInt("enemyhp",hp);
			PlayerPrefs.SetInt("enemyattack",attack);
			PlayerPrefs.SetInt("uphp",uphp);
			PlayerPrefs.SetInt("upattack",upattack);
			
			PlayerPrefs.SetInt("isScreenChange",1);
			FadeManager.Instance.LoadScene ("BattleScene", 2.0f);

			StartCoroutine(DelayMethod(1.0f, () => {
				//画面遷移
				RemainAudio.Instance.ChangeBgm(1);
			}));
			
		}
		if(col.CompareTag("ItemRed")){

			RemainAudio.Instance.PlaySE("item");

			string id = col.GetComponent<ItemID>().id;
			PlayerPrefs.SetInt(id,0);
			
			Destroy(col.gameObject);
			int plusH = col.GetComponent<ItemStatus>().hp;
			int plusA = col.GetComponent<ItemStatus>().attack;
			int attack = PlayerPrefs.GetInt("attack") + plusA;
			int health = PlayerPrefs.GetInt("hp") + plusH;
			textAttack.GetComponent<Text>().text = attack.ToString();
			textHealth.GetComponent<Text>().text = health.ToString();

			PlayerPrefs.SetInt("attack",attack);
			PlayerPrefs.SetInt("hp",health);

			GameObject textAnim = textAttack.transform.GetChild(0).gameObject;
			textAnim.SetActive(true);
			textAnim.GetComponent<Text>().color = Color.red;
			textAnim.GetComponent<Text>().text = "+" + plusA.ToString() + "↑";

			GameObject textAnimH = textHealth.transform.GetChild(0).gameObject;
			textAnimH.SetActive(true);
			textAnimH.GetComponent<Text>().color = Color.red;
			textAnimH.GetComponent<Text>().text = "+" + plusH.ToString() + "↑";
			StartCoroutine(DelayMethod(1.5f, () =>
			{
				textAnim.SetActive(false);
				textAnimH.SetActive(false);
			}));
		}
		if(col.CompareTag("ItemBlue")){

			RemainAudio.Instance.PlaySE("down");

			string id = col.GetComponent<ItemID>().id;
			PlayerPrefs.SetInt(id,0);
			
			Destroy(col.gameObject);
			int plusH = col.GetComponent<ItemStatus>().hp;
			int plusA = col.GetComponent<ItemStatus>().attack;
			int attack = PlayerPrefs.GetInt("attack") - plusA;
			int health = PlayerPrefs.GetInt("hp") - plusH;
			if(attack < 0)attack = 0;
			if(health < 0)health = 0;
			textAttack.GetComponent<Text>().text = attack.ToString();
			textHealth.GetComponent<Text>().text = health.ToString();

			PlayerPrefs.SetInt("attack",attack);
			PlayerPrefs.SetInt("hp",health);

			GameObject textAnim = textAttack.transform.GetChild(0).gameObject;
			textAnim.SetActive(true);
			textAnim.GetComponent<Text>().color = Color.blue;
			textAnim.GetComponent<Text>().text = "-" + plusA.ToString() + "↓";

			GameObject textAnimH = textHealth.transform.GetChild(0).gameObject;
			textAnimH.SetActive(true);
			textAnimH.GetComponent<Text>().color = Color.blue;
			textAnimH.GetComponent<Text>().text = "-" + plusH.ToString() + "↓";
			StartCoroutine(DelayMethod(1.5f, () =>
			{
				textAnim.SetActive(false);
				textAnimH.SetActive(false);
			}));
		}

		if(col.CompareTag("Warp")){

			RemainAudio.Instance.PlaySE("warp");

			string id = col.GetComponent<ItemID>().id;
			PlayerPrefs.SetInt(id,0);

			string target = "Stage" + col.GetComponent<WarpManager>().target.ToString();
			PlayerPrefs.SetInt("nowStage",col.GetComponent<WarpManager>().target);

			PlayerPrefs.SetInt("isScreenChange",1);
			//画面遷移
			FadeManager.Instance.LoadScene (target, 2.0f);
			
			Destroy(col.gameObject);
		}

		if(col.CompareTag("Chest")){

			RemainAudio.Instance.PlaySE("item");

			string id = col.GetComponent<ItemID>().id;
			PlayerPrefs.SetInt(id,0);
			
			Destroy(col.gameObject);
			int plusH = col.GetComponent<ItemStatus>().hp;
			int plusA = col.GetComponent<ItemStatus>().attack;
			int attack = PlayerPrefs.GetInt("attack") + plusA;
			int health = PlayerPrefs.GetInt("hp") + plusH;
			textAttack.GetComponent<Text>().text = attack.ToString();
			textHealth.GetComponent<Text>().text = health.ToString();

			PlayerPrefs.SetInt("attack",attack);
			PlayerPrefs.SetInt("hp",health);

			GameObject textAnim = textAttack.transform.GetChild(0).gameObject;
			textAnim.SetActive(true);
			textAnim.GetComponent<Text>().color = Color.red;
			textAnim.GetComponent<Text>().text = "+" + plusA.ToString() + "↑";

			GameObject textAnimH = textHealth.transform.GetChild(0).gameObject;
			textAnimH.SetActive(true);
			textAnimH.GetComponent<Text>().color = Color.red;
			textAnimH.GetComponent<Text>().text = "+" + plusH.ToString() + "↑";
			StartCoroutine(DelayMethod(1.5f, () =>
			{
				textAnim.SetActive(false);
				textAnimH.SetActive(false);
			}));
		}

    }

	private IEnumerator DelayMethod(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}
