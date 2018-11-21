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
		PlayerPrefs.SetInt("enemyhp",50);
		PlayerPrefs.SetInt("enemyattack",5);
		PlayerPrefs.SetInt("enemy1",0);
		PlayerPrefs.SetInt("enemy2",4);
		PlayerPrefs.SetInt("enemy3",0);
		PlayerPrefs.SetInt("enemy4",4);
		PlayerPrefs.SetInt("enemy5",0);
		PlayerPrefs.SetInt("enemy6",4);
		PlayerPrefs.SetInt("enemy7",0);
		PlayerPrefs.SetInt("enemy8",4);
		PlayerPrefs.SetInt("enemy9",0);
		PlayerPrefs.SetInt("enemy10",4);

		textHP.transform.GetChild(0).gameObject.GetComponent<Text>().text 
			= PlayerPrefs.GetInt("enemyhp").ToString();
		textAttack.transform.GetChild(0).gameObject.GetComponent<Text>().text 
			= PlayerPrefs.GetInt("enemyattack").ToString();
	}
}
