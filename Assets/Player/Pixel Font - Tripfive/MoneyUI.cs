using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour {

	Text text;
	Animator anim;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//if(anim.GetBool("gotMoney") == true ) anim.SetBool("gotMoney", false);
	}

	IEnumerator makeMoneyBigger()
	{
		//text.rectTransform.
	//	Debug.Log("No siemka");
		text.rectTransform.localScale = new Vector3(15.5f, 15.5f);
		yield return new WaitForSeconds(0.1f);
		text.rectTransform.localScale = new Vector3(13f, 13f);
	}

	public void takeMoney(int amount)
	{
		StartCoroutine("makeMoneyBigger");
		//anim.SetBool("gotMoney", true);
		text.text = "" + amount;
	}
}
