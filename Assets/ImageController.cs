using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class ImageController : MonoBehaviour
{



    ////オブジェクトの取得用変数
    private GameObject unityChan; //Unityちゃんオブジェクトを入れる変数
    ////スクリプトの取得用変数
    private GameMethods gameMethods; //GameMethodスクリプトを入れる変数
    private Image image; //Imageコンポーネントを入れる変数
    private UnityChanController uCC; //UnityChanControllerスクリプトを入れる変数
   
    private PlayableDirector clearAnimation; //クリア時のアニメーションを再生するPlayableDirectorのオンオフを制御する変数


    //////数値・係数を扱う変数
    private float alpha = 1.0f;
    ////係数
    private float alphaSpeed; //フェードアウトのスピード


    private bool isCountEnd = false; //カウント終了の判定
    private bool isFadeout = false; //Imageフェードアウト開始の判定

    // Use this for initialization
    void Start()
    {
        ////オブジェクトの取得
        unityChan = GameObject.Find("unitychan"); //Unityちゃんオブジェクトを取得
        ////スクリプトの取得
        gameMethods = GameObject.Find("GameMethods").GetComponent<GameMethods>(); //GameMethodsを取得
        uCC = gameMethods.GetComponent<UnityChanController>(); //UnityChanControllerを取得
        image = this.GetComponent<Image>(); //アタッチしているオブジェクトからImageControllerを取得
        //Playbleの取得
        this.clearAnimation = GameObject.Find("GameClearTimeline").GetComponent<PlayableDirector>(); //GameClearTimelineのPlayableDirectorを取得
    }

    // Update is called once per frame
    void Update()
    {
        ////Imageフェードアウト処理
        if (isFadeout == true)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            alpha -= alphaSpeed;

            if (alpha <= 0)
            {
                Debug.Log("0だよ");
                image.enabled = false;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
                isFadeout = false;
            }
        }
    }



    void Fadeout(float num) //Imageを第二引数に入れた数値ずつフェードアウトさせる
    {
        isFadeout = true; //Update()のImageフェードアウト処理を開始
        alphaSpeed = num;
    }

    ////リトライボタンを押したときに各オブジェクトをゲーム開始位置に戻す
    public void OnRetry()
    {
        if(this.gameObject.tag == "Button_Retry")
        {
            unityChan.transform.position = new Vector3(0, 0, 0); //Unityちゃんの座標を戻す
            this.clearAnimation.enabled = false; //クリアアニメーションのPlaybleをオフ
            //gameMethods.ItemGenerate(); //GameMethodsスクリプト内のItemGenerate()を起動
            //StartCoroutine("CallOpcoRoutine"); //OPカウントをするコルーチンを呼び出す
        }
        Fadeout(0.1f); //このスクリプトをアタッチしているRetryボタンをフェードアウト
    }
    //OpCounter内に記述してあるOpcoRoutine()を起動するコルーチン
    // private IEnumerator CallOpcoRoutine()
    // {
    //     OpCounter opc = new OpCounter();
    //     yield return StartCoroutine(opc.OpcoRoutine());
    //     Debug.Log("OpcoRoutineコルーチン使えてるよ");
    // }




}
