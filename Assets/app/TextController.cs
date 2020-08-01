using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    private float alpha = 1.0f;
    private float alphaSpeed;
    private bool isFadeout = false;
    private Text text;

    void Start () 
	{
        text = this.GetComponent<Text>();
    }
	
	void Update ()
	{
        ////Imageフェードアウト処理
        if (isFadeout == true)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            alpha -= alphaSpeed;

            if (alpha <= 0)
            {
                text.enabled = false;
				text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f);
                isFadeout = false;
            }
        }
	}

    //Imageを第二引数に入れた数値ずつフェードアウトさせる処理
    public void Fadeout(float num)
    {
        //Update()のImageフェードアウト処理を開始
        isFadeout = true;
        alphaSpeed = num;
    }
}


