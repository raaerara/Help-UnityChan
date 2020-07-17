using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Playables;

public class UnityChanController : MonoBehaviour
{
    
    private Animator myAnimator; //アニメーションするためのコンポーネントを入れる
    private Rigidbody myRigidbody; //Unityちゃんを移動させるコンポーネントを入れる
    private float forwardForce = 300.0f; //前進するための力
    private float animatorSpeed = 1f; //アニメーター
    private float countTime = 0.0f; //経過時間
    private float countClear = 0.0f; //クリアタイム
    private float decelerateCoefficient = 0.95f; //動きを減速させる係数
    private GameObject obstacle = null; //障害物のオブジェクトを格納しておく変数
    private bool isStop = false; //障害物に塞がれた判定
    private bool isGoal = false; //ゴールしてないかの判定
    private bool isHighScore = false; //ハイスコアを更新したかの判定
    private GameObject clearTimeText; //タイムを表示するテキスト
    private float highScore; //ハイスコア用変数
    

    // Use this for initialization
    void Start()
    {
        //保存しておいたハイスコアを呼び出し取得し保存されていなければ9999になる
        highScore = PlayerPrefs.GetFloat("HIGHSCORE", 9999);

        //ハイスコア表示(仮)
        Debug.Log(highScore.ToString("f2") + "秒");

        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //Rigidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody>();

        //シーン中のClearTimeTextオブジェクトを取得
        this.clearTimeText = GameObject.Find("ClearTimeText");
        
        //GameSceneTimelineを停止
        GameObject.Find("GameSceneTimeline").GetComponent<PlayableController>().StopTimeline();


    }
    void Update()
    {
        //走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", animatorSpeed);

        Debug.Log(animatorSpeed);
        if (isGoal == false)
        {
            //クリアタイムの計算
            countClear += Time.deltaTime;
            //ClearTimeTextにタイムを表示
            this.clearTimeText.GetComponent<Text>().text = countClear.ToString("f2") + "秒";
        }


        //Unityちゃんの動きを減衰させる
        if (this.isStop)
        {
            decelerate();
            //障害物で立ち止まった時間を計測
            countTime += Time.deltaTime;

            //Restステートの場合はRestにfalseをセットする
            if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Rest"))
            {
                this.myAnimator.SetBool("Rest", false);
            }
            if (countTime > 4)
            {
                //休みモーションを開始
                this.myAnimator.SetBool("Rest", true);
                countTime = -8;
            }
        }


        //目の前のオブジェクトが破壊されたら
        if (obstacle == null)
        {
            this.isStop = false;

            //障害物で立ち止まった時間をリセットして休みモーションを終了
            countTime = 0.0f;
            this.myAnimator.SetBool("Rest", false);
            //前に進む
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
        //Update()内のUnityちゃんの動きを減衰させる処理が開始
        this.isStop = true;
        //衝突した障害物を格納
        obstacle = other.gameObject;
        //ゴール地点に到達した場合
        if (other.gameObject.tag == "GoalTag")
        {
            //ゲームクリア
            isGoal = true;
            GameObject.Find("ScoreTime").GetComponent<Text>().text = "スコアタイム" + countClear.ToString("f2") + "秒";
            //ハイスコアを更新
            if (countClear < highScore)
            {
                PlayerPrefs.SetFloat("HIGHSCORE", countClear);
                isHighScore = true;   
            }
            
            //コルーチンでクリアアニメーション再生
            StartCoroutine("ClearAnimationcoRoutine");
        }
    }

    //減速
    void decelerate()
    {
        this.forwardForce *= this.decelerateCoefficient;
        this.myAnimator.SetFloat("Speed", animatorSpeed *= this.decelerateCoefficient);

    }


    //クリア時のアニメーション
    IEnumerator ClearAnimationcoRoutine()
    {
        yield return new WaitForSeconds(1); // 2秒待機
        if(isHighScore == true)
        {
            GameObject.Find("GameClearTimeline(HighScore)").GetComponent<PlayableController>().PlayTimeline(); //クリアアニメーションtimelineのplayableをオン
        }else
        {
            GameObject.Find("GameClearTimeline").GetComponent<PlayableController>().PlayTimeline(); //クリアアニメーションtimelineのplayableをオン

        }
        //変数をリセット
        VariableReset();
        //このスクリプトをオフ
        GetComponent<UnityChanController>().enabled = false;
    }




    //変数のリセットをする関数
    void VariableReset()
    {
    obstacle = null; //障害物のオブジェクトを格納しておく変数
    isStop = false;　//障害物に塞がれた判定
    isGoal = false;　//ゴールしてないかの判定
    forwardForce = 300.0f;
    animatorSpeed = 1f;　//アニメーター
    countTime = 0.0f;　//経過時間
    countClear = 0.0f;　//クリアタイム
    decelerateCoefficient = 0.95f;　//動きを減速させる係数
    }

}