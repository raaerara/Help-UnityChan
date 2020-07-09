using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class OpCounter : MonoBehaviour
{
    //Unityちゃんを入れる変数
    private GameObject player;
    //UnityちゃんについてるUnityChanControllerスクリプトを入れる変数
    private UnityChanController script;
    private Image image; 

    private float alfa;
    public Sprite[] counts = new Sprite[4];


    //カウント数
    private float count = 3f;

    //カウント終了の判定
    private bool isCountEnd = false;

    //カウント画像透明化スピード
    float alfaSpeed = 0.01f;


    // Use this for initialization
    void Start()
    {
        //Imageを取得
        image = this.GetComponent<Image>();

        alfa = this.GetComponent<Image>().color.a;

        //unityちゃんのオブジェクトとスクリプトを取得
        player = GameObject.Find("unitychan");
        script = player.GetComponent<UnityChanController>();
        
        //コルーチンでカウントダウン
        StartCoroutine("OpcoRoutine");

        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountEnd)
        {
            GetComponent<Image>().color = new Color(255, 255, 255, alfa);
            alfa -= alfaSpeed;
        }
    }

    //Updateを3秒後に呼び出す
    IEnumerator OpcoRoutine()
    {
        Debug.Log("コルーチン使えてるよ");
        yield return new WaitForSeconds(2); // 3秒待機

        for (int i = 3; i >= 0; i--)
        {
            if(i==0)
            {
                //UnityChanControllerスクリプトをオン
                script.enabled = true;
                isCountEnd = true;
            }
            //カウント画像を表示
            image.sprite = counts[i];
            image.enabled = true;
            yield return new WaitForSeconds(1); // 1秒待機
        }
       
    }


}
