using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectController : MonoBehaviour
{
    private float highScore;
    private int breakCount = 0;
    private GameObject scoreObject;
    private GameObject startObject;
    public GameObject particleDestroy;
    private GameObject billBoard;
    private GameMethods gameMethods;
    private SEController sE;
    private OpCounter opCounter;
    private Material objectColor;
    private Animator myAnimator;
    private Text clearTimeText;
    private PlayableController gameTitleTimeline;
    private PlayableController gameTitleTimelineChara;
    private PlayableController gameSceneTimeline;
    private PlayableController gameTitleTimelineAfterScore;
    private PlayableController gameScoreTimeline;
    private PlayableController gameScoreTimelineChara;

    void Start()
    {
        //オブジェクトの格納
        scoreObject = GameObject.Find("ScoreObject");
        startObject = GameObject.Find("StartObject");
        billBoard = GameObject.Find("BillBoard");
        //コンポーネントの格納
        sE = GameObject.Find("SE").GetComponent<SEController>();
        this.myAnimator = GetComponent<Animator>();
        gameMethods = GameObject.Find("GameMethods").GetComponent<GameMethods>();
        opCounter = GameObject.Find("CountImage").GetComponent<OpCounter>();
        clearTimeText = GameObject.Find("ClearTimeText").GetComponent<Text>();
        gameTitleTimeline = GameObject.Find("GameTitleTimeline").GetComponent<PlayableController>();
        gameTitleTimelineChara = GameObject.Find("GameTitleTimeline(Chara)").GetComponent<PlayableController>();
        gameSceneTimeline = GameObject.Find("GameSceneTimeline").GetComponent<PlayableController>();
        gameTitleTimelineAfterScore = GameObject.Find("GameTitleTimeline(AfterScore)").GetComponent<PlayableController>();
        gameScoreTimeline = GameObject.Find("GameScoreTimeline").GetComponent<PlayableController>();
        gameScoreTimelineChara = GameObject.Find("GameScoreTimeline(Chara)").GetComponent<PlayableController>();

    }

    void OnMouseDown()
    {
        if (this.gameObject.tag == "StartObjectTag")
        {
            //GameMethods.cs記述のItemGenerate()を呼び出す
            gameMethods.ItemGenerate();
            //Playableのオンオフ
            gameTitleTimeline.StopTimeline();
            gameTitleTimelineChara.StopTimeline();
            gameSceneTimeline.PlayTimeline();
            //オブジェクトをオフ
            scoreObject.SetActive(false);
            startObject.SetActive(false);
            billBoard.SetActive(false);
            //OpCounter.cs内記述のOPカウントダウンをするコルーチンを呼び出す
            opCounter.CallcoRoutine();
            //右上のタイムカウントしてるテキストのリセット
            clearTimeText.text = "0.00秒";
        }
        else if (this.gameObject.tag == "ScoreObjectTag")
        {
            Billboard_ReflectScore();
            //Playableのオンオフ
            gameTitleTimeline.StopTimeline();
            gameTitleTimelineAfterScore.StopTimeline();
            gameTitleTimelineChara.StopTimeline();
            gameScoreTimeline.PlayTimeline();
            gameScoreTimelineChara.PlayTimeline();
            //オブジェクトをオフ
            scoreObject.SetActive(false);
            startObject.SetActive(false);
        }
        else if (this.gameObject.tag == "HomeButton_BillBoardViewTag")
        {
            gameScoreTimeline.StopTimeline();
            gameScoreTimelineChara.StopTimeline();
            gameTitleTimelineAfterScore.PlayTimeline();
            gameTitleTimelineChara.PlayTimeline();
            scoreObject.SetActive(true);
            startObject.SetActive(true);
        }
        else
        {
            //震えるアニメーション再生
            this.myAnimator.SetTrigger("VIB");
            sE.TouchSound();
            breakCount++;
            //クリックされたオブジェクトの子のマテリアルカラーを少し赤くする処理
            //各オブジェクトが破壊される直前で一番赤い状態になるよう後で変更
            for (int i = 0; i < this.gameObject.transform.childCount; i++)
            {
                foreach (Renderer targetRenderer in this.gameObject.transform.GetChild(i).GetComponents<Renderer>())
                {
                    foreach (Material material in targetRenderer.materials)
                    {
                        material.color = new Color(material.color.a, material.color.g * 0.8f, material.color.b * 0.8f);
                    }
                }
            }

            if (this.gameObject.tag == "RoadBlockerTag" && breakCount == 8)
            {
                ObjectDestroy();
            }
            else if (this.gameObject.tag == "PianoTag" && breakCount == 14)
            {
                ObjectDestroy();
            }
            else if (this.gameObject.tag == "TankTag" && breakCount == 24)
            {
                ObjectDestroy();
            }
        }
    }

    void ObjectDestroy()
    {
        //パーティクルの再生
        Instantiate(particleDestroy, this.transform.position, Quaternion.identity);
        sE.DestroySound();
        Destroy(this.gameObject);
    }


    void Billboard_ReflectScore()
    {
        //保存しておいたハイスコアを呼び出し取得。保存されていなければ9999にする
        highScore = PlayerPrefs.GetFloat("HIGHSCORE", 9999);

        GameObject.Find("Score_BillBoardView").GetComponent<TextMeshPro>().text = highScore.ToString("f2") + "s";
    }
}
