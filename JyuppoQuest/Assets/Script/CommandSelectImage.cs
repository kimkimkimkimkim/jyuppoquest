using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandSelectImage : MonoBehaviour {

	private float span = 0.5f;
	private float time = 0;
	private bool isActive = true;
	
	// Update is called once per frame
	void FixedUpdate () {
		time += Time.deltaTime;
		if(isActive){
			if(time >= span){
				GetComponent<Image>().enabled = false;
				time = 0;
				isActive = false;
			}
		}
		if(!isActive){
			if(time >= span){
				GetComponent<Image>().enabled = true;
				time = 0;
				isActive = true;
			}
		}

	}
}
