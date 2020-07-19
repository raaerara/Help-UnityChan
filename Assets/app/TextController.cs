using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {


    ////スクリプトの取得用変数
    private Text text; //Textコンポーネントを入れる変数
    //////数値・係数を扱う変数
    private float alpha = 1.0f;
    ////係数
    private float alphaSpeed; //フェードアウトのスピード
    private bool isFadeout = false; //Imageフェードアウト開始の判定


    // Use this for initialization
    void Start () 
	{
        ////スクリプトの取得
        text = this.GetComponent<Text>(); //アタッチしているオブジェクトからTextを取得
    }
	
	// Update is called once per frame
	void Update ()
	{
        ////Imageフェードアウト処理
        if (isFadeout == true)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            alpha -= alphaSpeed;

            if (alpha <= 0)
            {
                Debug.Log("0だよ");
                text.enabled = false;
				text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f);
                isFadeout = false;
            }
        }
	}
    public void Fadeout(float num) //Imageを第二引数に入れた数値ずつフェードアウトさせる
    {
        isFadeout = true; //Update()のImageフェードアウト処理を開始
        alphaSpeed = num;
    }
}


