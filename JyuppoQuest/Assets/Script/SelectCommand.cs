using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectCommand : MonoBehaviour {

	public GameObject commandArea;
	public GameObject explainArea;

	public GameObject message;

	public GameObject textHpEnemy;
	public GameObject textHpHero;

	public GameObject effectHero;
	public GameObject effectEnemy;

	public GameObject sliderHero;
	public GameObject sliderEnemy;

	public string[] textExplain = new string[4];

	/*
		{
			"herocommand", int 行動 0→攻撃 1→防御 2→回復
			"enemycommand", int 行動 0→攻撃 1→防御 2→回復 3→休憩
		}
	 */
	// Use this for initialization
	void Start () {
		sliderHero.GetComponent<Slider>().maxValue = PlayerPrefs.GetInt("hp");
		sliderEnemy.GetComponent<Slider>().maxValue = PlayerPrefs.GetInt("enemyhp");
		sliderHero.GetComponent<Slider>().value = PlayerPrefs.GetInt("hp");
		sliderEnemy.GetComponent<Slider>().value = PlayerPrefs.GetInt("enemyhp");
		PlayerPrefs.SetInt("enemycommand",0);
		PlayerPrefs.SetInt("herocommand",0);
		selectCommand(0);
	}

	private void Update(){
		if(Input.GetKeyUp(KeyCode.Return)){
			PlayerPrefs.SetInt("isReturn",0);
		}

		if(PlayerPrefs.GetInt("isBattle") == 0)return;

		//右
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
			int num = PlayerPrefs.GetInt("herocommand");
			if(num == 1 || num == 3 || num == 2)return;
			num += 1;
			PlayerPrefs.SetInt("herocommand",num);
			selectCommand(num);
		}
		//左
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
			int num = PlayerPrefs.GetInt("herocommand");
			if(num == 0 || num == 2)return;
			num -= 1;
			PlayerPrefs.SetInt("herocommand",num);
			selectCommand(num);
		}
		//上
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
			int num = PlayerPrefs.GetInt("herocommand");
			if(num == 0 || num == 1 )return;
			num -= 2;
			PlayerPrefs.SetInt("herocommand",num);
			selectCommand(num);
		}
		//下
		if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
			int num = PlayerPrefs.GetInt("herocommand");
			if(num == 2 || num == 3 || num == 1)return;
			num += 2;
			PlayerPrefs.SetInt("herocommand",num);
			selectCommand(num);
		}
		//Enter
		bool isReturn = (PlayerPrefs.GetInt("isReturn",0) == 0)? false : true; 
		if(Input.GetKeyDown(KeyCode.Return) && !isReturn){
			//コマンド
			PlayerPrefs.SetInt("isReturn",1);
			PlayerPrefs.SetInt("isBattle",0);
			Battle();
		}
	}
	
	void selectCommand(int n){
		for(int i=0;i<3;i++){
			GameObject image = commandArea.transform.GetChild(i).GetChild(0).gameObject;
			image.SetActive(false);
			if(i==n)image.SetActive(true);
		}
		explainArea.transform.GetChild(0).GetComponent<Text>().text = textExplain[n];
	}

	/*
	private void Battle(){
		commandArea.SetActive(false);
		explainArea.SetActive(false);
		message.GetComponent<Message>().SetMessagePanel("次");

	}
	*/

	private void Battle(){
		//行動
		int hero = PlayerPrefs.GetInt("herocommand");
		PlayerPrefs.SetInt("enemycommand",UnityEngine.Random.Range(0,4));
		int enemy = PlayerPrefs.GetInt("enemycommand");

		int heroAttack = PlayerPrefs.GetInt("attack");
		int enemyAttack = PlayerPrefs.GetInt("enemyattack");

		commandArea.SetActive(false);
		explainArea.SetActive(false);

		if(hero == 0){
			//攻撃
			if(enemy == 0){
				//相手が攻撃していた時
				HeroAttack((int)heroAttack);
				EnemyAttack(enemyAttack);
				message.GetComponent<Message>().SetMessagePanel(
				"勇者の攻撃！\n"
				+"敵の攻撃！\n"
				+"\n"
				+"お互いダメージを食らった。");
			}else if(enemy == 1){
				//相手が防御していた時
				EnemyAttack((int)(enemyAttack*1.5f));
				message.GetComponent<Message>().SetMessagePanel(
				"勇者の攻撃！\n"
				+"敵のカウンター！\n"
				+"\n"
				+"やばい！\n敵のカウンターを食らってしまった。");
			}else if(enemy == 2){
				//相手が回復していた時
				HeroAttack((int)heroAttack);
				message.GetComponent<Message>().SetMessagePanel(
				"勇者の攻撃！\n"
				+"敵の回復魔法！\n"
				+"\n"
				+"敵が回復準備をしている間を狙って一方的に攻撃！");
			}else if(enemy == 3){
				//相手が休憩していた時
				HeroAttack((int)heroAttack);
				message.GetComponent<Message>().SetMessagePanel(
				"勇者の攻撃！\n"
				+"敵はボーッとしている。\n"
				+"\n"
				+"よし！いいダメージが入った。");
			}
		}else if(hero == 1){
			//防御
			if(enemy == 0){
				//相手が攻撃していた時
				HeroAttack((int)(heroAttack*1.5f));
				message.GetComponent<Message>().SetMessagePanel(
				"勇者のカウンター！\n"
				+"敵の攻撃！\n"
				+"\n"
				+"カウンター成功。\n敵にダメージ与えた！");
			}else if(enemy == 1){
				//相手が防御していた時
				message.GetComponent<Message>().SetMessagePanel(
				"勇者のカウンター！\n"
				+"敵のカウンター！\n"
				+"\n"
				+"お互い身構えている。");
			}else if(enemy == 2){
				//相手が回復していた時
				EnemyHeal(enemyAttack);
				EnemyAttack((int)enemyAttack/2);
				message.GetComponent<Message>().SetMessagePanel(
				"勇者のカウンター！\n"
				+"敵の回復魔法！\n"
				+"\n"
				+"やられた！\n敵の体力が回復してしまった。");
			}else if(enemy == 3){
				//相手が休憩していた時
				message.GetComponent<Message>().SetMessagePanel(
				"勇者のカウンター！\n"
				+"敵はボーッとしている。\n"
				+"\n"
				+"何も起こらない。");
			}
		}else if(hero == 2){
			//回復
			if(enemy == 0){
				//相手が攻撃していた時
				EnemyAttack(enemyAttack);
				message.GetComponent<Message>().SetMessagePanel(
				"勇者の回復魔法！\n"
				+"敵の攻撃！\n"
				+"\n"
				+"ぐっ！\n敵の攻撃を食らってしまった。");
			}else if(enemy == 1){
				//相手が防御していた時
				HeroHeal(heroAttack);
				HeroAttack((int)heroAttack/2);
				message.GetComponent<Message>().SetMessagePanel(
				"勇者の回復魔法！\n"
				+"敵のカウンター！\n"
				+"\n"
				+"決まった！\n体力が回復した。");
			}else if(enemy == 2){
				//相手が回復していた時
				HeroHeal(heroAttack);
				EnemyHeal(enemyAttack);
				message.GetComponent<Message>().SetMessagePanel(
				"勇者の回復魔法！\n"
				+"敵の回復魔法！\n"
				+"\n"
				+"どちらも体力回復！");
			}else if(enemy == 3){
				//相手が休憩していた時
				HeroHeal(heroAttack);
				HeroAttack((int)heroAttack/2);
				message.GetComponent<Message>().SetMessagePanel(
				"勇者の回復魔法！\n"
				+"敵はボーッとしている。\n"
				+"\n"
				+"よし！回復成功！");
			}
		}
	}

	public void HeroAttack(int attack){
		int hp = int.Parse(textHpEnemy.GetComponent<Text>().text);
		hp -= attack;
		if(hp < 0)hp = 0;
		PlayerPrefs.SetInt("isAnimation",1);
		effectEnemy.transform.GetChild(0).gameObject.SetActive(true);

		StartCoroutine(DelayMethod(1.0f, () => {
			iTween.ValueTo(sliderEnemy,iTween.Hash("from",int.Parse(textHpEnemy.GetComponent<Text>().text)
				,"to",hp,"onupdate","UpdateSliderEnemy","onupdatetarget",gameObject));
			textHpEnemy.GetComponent<Text>().text = hp.ToString();
			GameObject textAnim = textHpEnemy.transform.GetChild(0).gameObject;
			textAnim.SetActive(true);
			textAnim.GetComponent<Text>().color = Color.blue;
			textAnim.GetComponent<Text>().text = "-" + attack.ToString() + "↓";
			StartCoroutine(DelayMethod(2.0f, () =>
			{
				effectEnemy.transform.GetChild(0).gameObject.SetActive(false);
				PlayerPrefs.SetInt("isAnimation",0);
				textAnim.SetActive(false);
			}));
		}));
	}

	public void HeroDefense(){
		PlayerPrefs.SetInt("isAnimation",1);
		effectHero.transform.GetChild(1).gameObject.SetActive(true);

		StartCoroutine(DelayMethod(1.0f, () => {
			StartCoroutine(DelayMethod(2.0f, () =>
			{
				effectHero.transform.GetChild(1).gameObject.SetActive(false);
				PlayerPrefs.SetInt("isAnimation",0);
			}));
		}));
	}

	public void HeroHeal(int attack){
		int hp = int.Parse(textHpHero.GetComponent<Text>().text);
		hp += attack;
		if(hp > PlayerPrefs.GetInt("hp"))hp = PlayerPrefs.GetInt("hp");
		if(hp < 0)hp = 0;
		PlayerPrefs.SetInt("isAnimation",1);
		effectHero.transform.GetChild(2).gameObject.SetActive(true);

		StartCoroutine(DelayMethod(1.0f, () => {
			iTween.ValueTo(sliderHero,iTween.Hash("from",int.Parse(textHpHero.GetComponent<Text>().text)
				,"to",hp,"onupdate","UpdateSliderHero","onupdatetarget",gameObject));
			textHpHero.GetComponent<Text>().text = hp.ToString();
			GameObject textAnim = textHpHero.transform.GetChild(0).gameObject;
			textAnim.SetActive(true);
			textAnim.GetComponent<Text>().color = Color.red;
			textAnim.GetComponent<Text>().text = "+" + attack.ToString() + "↑";
			StartCoroutine(DelayMethod(2.0f, () =>
			{
				effectHero.transform.GetChild(2).gameObject.SetActive(false);
				PlayerPrefs.SetInt("isAnimation",0);
				textAnim.SetActive(false);
			}));
		}));
	}
	

	public void EnemyAttack(int attack){
		int hp = int.Parse(textHpHero.GetComponent<Text>().text);
		hp -= attack;
		if(hp < 0)hp = 0;
		PlayerPrefs.SetInt("isAnimation",1);
		effectHero.transform.GetChild(0).gameObject.SetActive(true);

		StartCoroutine(DelayMethod(1.0f, () => {
			iTween.ValueTo(sliderHero,iTween.Hash("from",int.Parse(textHpHero.GetComponent<Text>().text)
				,"to",hp,"onupdate","UpdateSliderHero","onupdatetarget",gameObject));
			textHpHero.GetComponent<Text>().text = hp.ToString();
			GameObject textAnim = textHpHero.transform.GetChild(0).gameObject;
			textAnim.SetActive(true);
			textAnim.GetComponent<Text>().color = Color.blue;
			textAnim.GetComponent<Text>().text = "-" + attack.ToString() + "↓";
			StartCoroutine(DelayMethod(2.0f, () =>
			{
				effectHero.transform.GetChild(0).gameObject.SetActive(false);
				PlayerPrefs.SetInt("isAnimation",0);
				textAnim.SetActive(false);
			}));
		}));
	}

	public void EnemyDefense(){
		PlayerPrefs.SetInt("isAnimation",1);
		effectEnemy.transform.GetChild(1).gameObject.SetActive(true);

		StartCoroutine(DelayMethod(1.0f, () => {
			StartCoroutine(DelayMethod(2.0f, () =>
			{
				effectEnemy.transform.GetChild(1).gameObject.SetActive(false);
				PlayerPrefs.SetInt("isAnimation",0);
			}));
		}));
	}

	public void EnemyHeal(int attack){
		int hp = int.Parse(textHpEnemy.GetComponent<Text>().text);
		hp += attack;
		if(hp > PlayerPrefs.GetInt("enemyhp"))hp = PlayerPrefs.GetInt("enemyhp");
		if(hp < 0)hp = 0;
		PlayerPrefs.SetInt("isAnimation",1);
		effectEnemy.transform.GetChild(2).gameObject.SetActive(true);

		StartCoroutine(DelayMethod(1.0f, () => {
			iTween.ValueTo(sliderEnemy,iTween.Hash("from",int.Parse(textHpEnemy.GetComponent<Text>().text)
				,"to",hp,"onupdate","UpdateSliderEnemy","onupdatetarget",gameObject));
			textHpEnemy.GetComponent<Text>().text = hp.ToString();
			GameObject textAnim = textHpEnemy.transform.GetChild(0).gameObject;
			textAnim.SetActive(true);
			textAnim.GetComponent<Text>().color = Color.red;
			textAnim.GetComponent<Text>().text = "+" + attack.ToString() + "↑";
			StartCoroutine(DelayMethod(2.0f, () =>
			{
				effectEnemy.transform.GetChild(2).gameObject.SetActive(false);
				PlayerPrefs.SetInt("isAnimation",0);
				textAnim.SetActive(false);
			}));
		}));
	}

	private void UpdateSliderHero(float value){
		sliderHero.GetComponent<Slider>().value = value;
	}

	private void UpdateSliderEnemy(float value){
		sliderEnemy.GetComponent<Slider>().value = value;
	}

	private IEnumerator DelayMethod(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}
