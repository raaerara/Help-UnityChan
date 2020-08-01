using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Playables;

public class UnityChanController : MonoBehaviour
{
    //前進するための力
    private float forwardForce = 300.0f;
    //動きを減速させる係数
    private float decelerateCoefficient = 0.95f;
    private float animatorSpeed = 1f;
    private float countTime = 0.0f;
    private float countClear = 0.0f;
    private float highScore;
    private GameObject obstacle = null;
    private bool isStop = false;
    private bool isGoal = false;
    private bool isHighScore = false;
    private GameObject clearTimeText;
    private Animator myAnimator;
    private Rigidbody myRigidbody;
    private Text scoreTime;
    private PlayableController gameClearTimeline;
    private PlayableController gameClearTimelineHighScore;


    void Start()
    {
        highScore = PlayerPrefs.GetFloat("HIGHSCORE", 9999);
        Debug.Log(highScore.ToString("f2") + "秒");
        //オブジェクトを取得
        clearTimeText = GameObject.Find("ClearTimeText");
        //コンポーネントの取得
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody>();
        scoreTime = GameObject.Find("ScoreTime").GetComponent<Text>();
        gameClearTimeline = GameObject.Find("GameClearTimeline").GetComponent<PlayableController>();
        gameClearTimelineHighScore = GameObject.Find("GameClearTimeline(HighScore)").GetComponent<PlayableController>();
    }

    void Update()
    {
        myAnimator.SetFloat("Speed", animatorSpeed);
        if (isGoal == false)
        {
            countClear += Time.deltaTime;
            clearTimeText.GetComponent<Text>().text = countClear.ToString("f2") + "秒";
        }

        //Unityちゃんの動きを減衰させる
        if (this.isStop)
        {
            Decelerate();
            //障害物で立ち止まった時間の計測
            countTime += Time.deltaTime;
            //Restステートの場合はRestにfalseをセットする
            if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Rest"))
            {
                this.myAnimator.SetBool("Rest", false);
            }
            //休みモーションを開始
            if (countTime > 4)
            {
                this.myAnimator.SetBool("Rest", true);
                countTime = -8;
            }
        }

        //目の前の障害物オブジェクトが破壊されたら
        if (obstacle == null)
        {
            this.isStop = false;
            countTime = 0.0f;
            this.myAnimator.SetBool("Rest", false);
            forwardForce = 300.0f;
            if (animatorSpeed < 1)
            {
                this.myAnimator.SetFloat("Speed", animatorSpeed += 0.1f);
            }
        }
        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);
    }

    //トリガーモードで他のオブジェクトと接触した場合の処理
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GoalTag" || other.gameObject.tag == "RoadBlockerTag" || other.gameObject.tag == "PianoTag" || other.gameObject.tag == "TankTag")
        {
            //Update()内記述のUnityちゃんの動きを減衰させる処理を開始
            this.isStop = true;
            //衝突した障害物の格納
            obstacle = other.gameObject;
            //ゴール地点到達の場合
            if (other.gameObject.tag == "GoalTag")
            {
                isGoal = true;
                scoreTime.text = "スコアタイム" + countClear.ToString("f2") + "秒";
                //生成されたパーティクルのインスタンスを削除
                var clones = GameObject.FindGameObjectsWithTag("Particle_BigExplosion");
                foreach (var clone in clones)
                {
                    Destroy(clone);
                }
                //ハイスコアを更新
                if (countClear < highScore)
                {
                    PlayerPrefs.SetFloat("HIGHSCORE", countClear);
                    isHighScore = true;
                }
                //クリアアニメーション再生
                StartCoroutine("ClearAnimationcoRoutine");
            }
        }
    }

    //減速
    void Decelerate()
    {
        this.forwardForce *= this.decelerateCoefficient;
        this.myAnimator.SetFloat("Speed", animatorSpeed *= this.decelerateCoefficient);
    }

    //クリア時のアニメーション
    IEnumerator ClearAnimationcoRoutine()
    {
        yield return new WaitForSeconds(1);
        if (isHighScore == true)
        {
            gameClearTimelineHighScore.PlayTimeline();
        }
        else
        {
            gameClearTimeline.PlayTimeline();
        }
        VariableReset();
        GetComponent<UnityChanController>().enabled = false;
    }

    //変数のリセットをするメソッド
    void VariableReset()
    {
        obstacle = null;
        isStop = false;
        isGoal = false;
        forwardForce = 300.0f;
        animatorSpeed = 1f;
        countTime = 0.0f;
        countClear = 0.0f;
        decelerateCoefficient = 0.95f;
    }
}