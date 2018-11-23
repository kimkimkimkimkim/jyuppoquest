using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

	public GameObject textHP;
	public GameObject textAttack;

	/*
		{
			"enemyhp",int
			"enemyattack",int
			"enemy1",int 行動 0→攻撃　１→防御 2→回復 4→休憩
		}
	 */
	
	void Start () {
		PlayerPrefs.SetInt("enemyhp",100);
		PlayerPrefs.SetInt("enemyattack",20);

		textHP.transform.GetChild(0).gameObject.GetComponent<Text>().text 
			= PlayerPrefs.GetInt("enemyhp").ToString();
		textAttack.transform.GetChild(0).gameObject.GetComponent<Text>().text 
			= PlayerPrefs.GetInt("enemyattack").ToString();
	}
}
