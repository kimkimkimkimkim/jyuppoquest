using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TextBlink : MonoBehaviour {

	public float interval = 0.5f;

	// Use this for initialization
	void Start () {
		 StartCoroutine ("Blink");
		RemainAudio.Instance.ChangeBgm(3);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return)){
			FadeManager.Instance.LoadScene("Stage1",2.0f);

			StartCoroutine(DelayMethod(1.9f, () => {
				RemainAudio.Instance.ChangeBgm(0);
			}));
		}
	}

    IEnumerator Blink() {
        while ( true ) {
            var renderComponent = GetComponent<Text>();
            renderComponent.enabled = !renderComponent.enabled;
            yield return new WaitForSeconds(interval);
        }
    }

	private IEnumerator DelayMethod(float waitTime, Action action)
	{
		yield return new WaitForSeconds(waitTime);
		action();
	}
}
