using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;
    //Unityちゃんを移動させるコンポーネントを入れる
    private Rigidbody myRigidbody;
    //前進するための力
    private float forwardForce = 300.0f;

    //アニメーター
    private float animatorSpeed = 1;

    //経過時間
    private float countTime = 0.0f;

    //クリアタイム
    private float countClear = 0.0f;

    //動きを減速させる係数
    private float decelerateCoefficient = 0.95f;

    //動きを加速させる係数
    private float accelerateCoefficient = 1.05f;

    //障害物のオブジェクトを格納しておく変数
    private GameObject obstacle = null;

    //障害物に塞がれた判定
    private bool isStop = false;

    //ゴールしてないかの判定
    private bool isGoal = false;

    //タイムを表示するテキスト
    private GameObject clearTimeText;


    // Use this for initialization
    void Start()
    {

        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", animatorSpeed);

        //Rigidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody>();

        //シーン中のClearTimeTextオブジェクトを取得
        this.clearTimeText = GameObject.Find("ClearTimeText");
    }
    void Update()
    {

        if(isGoal == false)
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

            if(countTime > 4)
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
        this.isStop = true;

        //衝突した障害物を格納
        obstacle = other.gameObject;

        // //障害物に衝突した場合
        // if (other.gameObject.tag == "RoadBlockerTag" || other.gameObject.tag == "PianoTag" || other.gameObject.tag == "TankTag")
        // {

        // }

        //ゴール地点に到達した場合
        if (other.gameObject.tag == "GoalTag")
        {
            //ゲームクリア
            isGoal = true;
        }
    }




    //減速
    void decelerate()
    {
        this.forwardForce *= this.decelerateCoefficient;

        this.myAnimator.SetFloat("Speed", animatorSpeed *= this.decelerateCoefficient);

    }






}