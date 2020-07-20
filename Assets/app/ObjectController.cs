using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectController : MonoBehaviour
{


    private float highScore; //ハイスコア用変数

    private int breakCount = 0; //破壊されるオブジェクトが壊れるまでのタップ回数カウント

    //
    private GameObject scoreObject;
    private GameObject startObject;



    // Use this for initialization
    void Start()
    {
        scoreObject = GameObject.Find("ScoreObject"); //Unityちゃんの手についてるオブジェクト
        startObject = GameObject.Find("StartObject"); //Unityちゃんの手についてるオブジェクト
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if(this.gameObject.tag == "StartObjectTag")
        {
            Debug.Log("スタートするよ");
            //アイテム生成
            GameObject.Find("GameMethods").GetComponent<GameMethods>().ItemGenerate();
            GameObject.Find("GameTitleTimeline").GetComponent<PlayableController>().StopTimeline(); //Title演出のtimelineのplayableをオフ
            GameObject.Find("GameTitleTimeline (Chara)").GetComponent<PlayableController>().StopTimeline(); //Title演出(キャラ)のtimelineのplayableをオフ
            GameObject.Find("GameSceneTimeline").GetComponent<PlayableController>().PlayTimeline(); //ゲーム開始演出のtimelineのplayableをオン
            scoreObject.SetActive(false); //Unityちゃんの手についてるオブジェクトをオフ
            startObject.SetActive(false); //Unityちゃんの手についてるオブジェクトをオフ
            GameObject.Find("BillBoard").SetActive(false); //Billboardオブジェクトをオフ
            GameObject.Find("CountImage").GetComponent<OpCounter>().CallcoRoutine();
        }
        else if(this.gameObject.tag == "ScoreObjectTag")
        {
            Billboard_ReflectScore();
            GameObject.Find("GameTitleTimeline").GetComponent<PlayableController>().StopTimeline(); //Score演出のtimelineのplayableをオフ
            GameObject.Find("GameTitleTimeline (AfterScore)").GetComponent<PlayableController>().StopTimeline(); //Score演出(キャラ)のtimelineのplayableをオフ
            GameObject.Find("GameTitleTimeline (Chara)").GetComponent<PlayableController>().StopTimeline(); //Score演出(キャラ)のtimelineのplayableをオフ
            GameObject.Find("GameScoreTimeline").GetComponent<PlayableController>().PlayTimeline(); //スコア演出のtimelineのplayableをオン
            GameObject.Find("GameScoreTimeline (Chara)").GetComponent<PlayableController>().PlayTimeline(); //スコア演出(キャラ)のtimelineのplayableをオン
            scoreObject.SetActive(false); //Unityちゃんの手についてるオブジェクトをオフ
            startObject.SetActive(false); //Unityちゃんの手についてるオブジェクトをオフ
        }
        else if(this.gameObject.tag == "HomeButton_BillBoardViewTag")
        {
            Debug.Log("できてるよ");
            GameObject.Find("GameScoreTimeline").GetComponent<PlayableController>().StopTimeline(); //Score演出のtimelineのplayableをオフ
            GameObject.Find("GameScoreTimeline (Chara)").GetComponent<PlayableController>().StopTimeline(); //Score演出(キャラ)のtimelineのplayableをオフ
            GameObject.Find("GameTitleTimeline (AfterScore)").GetComponent<PlayableController>().PlayTimeline(); //Title演出のtimelineのplayableをオン
            GameObject.Find("GameTitleTimeline (Chara)").GetComponent<PlayableController>().PlayTimeline(); //Title演出(キャラ)のtimelineのplayableをオン
            scoreObject.SetActive(true); //Unityちゃんの手についてるオブジェクトをオン
            startObject.SetActive(true); //Unityちゃんの手についてるオブジェクトをオン
        }
        else
        {
            breakCount++;
            Debug.Log(breakCount);

            if (this.gameObject.tag == "RoadBlockerTag" && breakCount == 2)
            {
                Destroy(this.gameObject);
            }
            else if (this.gameObject.tag == "PianoTag" && breakCount == 4)
            {
                Destroy(this.gameObject);
            }
            else if (this.gameObject.tag == "TankTag" && breakCount == 10)
            {
                Destroy(this.gameObject);
            }
        }

        
    }

    


    void Billboard_ReflectScore()
    {
        //保存しておいたハイスコアを呼び出し取得し保存されていなければ9999になる
        highScore = PlayerPrefs.GetFloat("HIGHSCORE", 9999);

        GameObject.Find("Score_BillBoardView").GetComponent<TextMeshPro>().text = highScore.ToString("f2") + "s";
    }

}
