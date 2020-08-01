using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GameMethods : MonoBehaviour
{
    //スタート地点の設定
    private int startPos = 50;
    //ゴール地点の設定
    private int goalPos = 400;
    public GameObject roadblockerPrefab;
    public GameObject pianoPrefab;
    public GameObject tankPrefab;
    private PlayableController gameTitleTimeline;
    
    void Start()
    {
        //ゲーム開始演出Timelineの再生
        GameObject.Find("GameTitleTimeline").GetComponent<PlayableController>().PlayTimeline();
        GameObject.Find("GameTitleTimeline(Chara)").GetComponent<PlayableController>().PlayTimeline();
        gameTitleTimeline = GameObject.Find("GameTitleTimeline").GetComponent<PlayableController>();
    }
 
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
        if (active == true)
        {
            turnOnObject.SetActive(true);
        }
        else
        {
            turnOnObject.SetActive(false);
        }
    }
}
