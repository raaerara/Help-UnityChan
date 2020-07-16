using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMethods : MonoBehaviour
{
    ////////何かを取得する用の変数
    //////Prefabの取得
    ////生成する障害物Prefabの取得
    public GameObject roadblockerPrefab; //carPrefabを入れる
    public GameObject pianoPrefab; //pianoPrefabを入れる
    public GameObject tankPrefab; //tankPrefabを入れる
    ////シーン中オブジェクトの取得
    private GameObject player; //unityちゃんのオブジェクトを入れる変数
    //////数値・係数を扱う変数
    ////ステージ設定
    private int startPos = 50; //スタート地点
    private int goalPos = 400; //ゴール地点
    
    
    // Use this for initialization
    void Start()
    {
        //ゲーム開始時にアイテム生成
        ItemGenerate();

        GameObject.Find("GameSceneTimeline").GetComponent<PlayableController>().PlayTimeline(); //クリアアニメーションtimelineのplayableをオン
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //一定の距離ごとに障害物オブジェクトを生成するメソッド
    public void ItemGenerate()
    {
        
        for (int i = startPos; i < goalPos; i += 30)
        {
            //どのアイテムを出すかランダムに設定
            int num = Random.Range(1, 11);
            if (num <= 4)
            {
                //ロードブロッカーを生成
                GameObject roadblocker = Instantiate(roadblockerPrefab) as GameObject;
                roadblocker.transform.position = new Vector3(i, roadblocker.transform.position.y, roadblocker.transform.position.z);
            }
            else if (num > 4 && num <= 9)
            {
                //ピアノを生成
                GameObject piano = Instantiate(pianoPrefab) as GameObject;
                piano.transform.position = new Vector3(i, piano.transform.position.y, piano.transform.position.z);
            }
            else
            {
                //戦車を生成
                GameObject tank = Instantiate(tankPrefab) as GameObject;
                tank.transform.position = new Vector3(i, tank.transform.position.y, tank.transform.position.z);
            }
        }
    }


    //第一引数に入れた名前のゲームオブジェクトの状態を第二引数でtrueかfalseに設定するメソッド
    public void ObjectActivate(string name, bool active)
    {
        GameObject turnOnObject = GameObject.Find(name);
        if(active == true)
        {
            turnOnObject.SetActive(true);
        }else
        {
            turnOnObject.SetActive(false);
        }
    }
}
