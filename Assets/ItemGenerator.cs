using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{


    //carPrefabを入れる
    public GameObject roadblockerPrefab;

    //coinPrefabを入れる
    public GameObject pianoPrefab;

    //cornPrefabを入れる
    public GameObject tankPrefab;

    //アイテムを出すx方向の範囲
    private float posRange = 5.2f;

    //スタート地点
    private int startPos = 50;

    //ゴール地点
    private int goalPos = 400;


    // Use this for initialization
    void Start()
    {
        //一定の距離ごとにアイテム生成
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

    // Update is called once per frame
    void Update()
    {

    }
}
