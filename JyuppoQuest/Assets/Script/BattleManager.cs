using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BattleManager : MonoBehaviour {

	public GameObject hpHero;
	public GameObject attackHero;
	public GameObject hpEnemy;
	public GameObject attackEnemy;

	private int turn = 0;

	private void Start(){
		hpHero.GetComponent<Text>().text = PlayerPrefs.GetInt("hp").ToString();
		attackHero.GetComponent<Text>().text = PlayerPrefs.GetInt("attack").ToString();
	}

}
