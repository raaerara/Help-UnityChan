using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class OpCounter : MonoBehaviour {


    ////オブジェクトの取得用変数
    private UnityChanController uCC; //UnityChanControllerスクリプトを入れる変数
	////スクリプトの取得用変数
    private Image opImg; //Imageコンポーネントを入れる変数
    ////Playbleの取得用変数
    private PlayableDirector sceneAnimation; //クリア時のアニメーションを再生するPlayableDirectorのオンオフを制御する変数

    private bool isFadeout = false; //Imageフェードアウト開始の判定

    //////数値・係数を扱う変数
    private float alpha = 1.0f;
    ////係数
    private float alphaSpeed = 0.01f; //OPカウント画像フェードアウトのスピード


    void Start () 
	{
        opImg = this.GetComponent<Image>();
        uCC = GameObject.Find("unitychan").GetComponent<UnityChanController>(); //UnityChanControllerを取得
        //Playbleの取得
        this.sceneAnimation = GameObject.Find("GameSceneTimeline").GetComponent<PlayableDirector>(); //GameClearTimelineのPlayableDirectorを取得
    }
	
	void Update () 
	{
        ////Imageフェードアウト処理
        if (isFadeout == true)
        {
            opImg.color = new Color(opImg.color.r, opImg.color.g, opImg.color.b, alpha);
            alpha -= alphaSpeed;

            if (alpha <= 0)
            {
                Debug.Log("0だよ");
                opImg.enabled = false;
                opImg.color = new Color(opImg.color.r, opImg.color.g, opImg.color.b, 1.0f);
                isFadeout = false;
                }
        }
	}


    public void CallcoRoutine() //ボタンから呼び出すため記述
    {
        StartCoroutine("OpcoRoutine"); //ゲーム開始時コルーチンでカウントダウン
    }

    //OPカウントダウンをするコルーチン
    public Sprite[] opCountImages = new Sprite[4];
    public IEnumerator OpcoRoutine()
    {
        yield return new WaitForSeconds(2); //2秒待機
        //opImg.enabled = true;
        for (int i = 3; i >= 0; i--)
        {
            if (i == 0)
            {
                uCC.enabled = true; //UnityChanControllerスクリプトをオン
                isFadeout = true;
                alpha = 1.0f;
            }
            opImg.sprite = opCountImages[i]; //カウント画像を表示
            opImg.enabled = true;
            yield return new WaitForSeconds(1); //1秒待機
        }
    }
}
