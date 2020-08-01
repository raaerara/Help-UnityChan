using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class OpCounter : MonoBehaviour {
    private float alpha = 1.0f;
    //カウント終了時のImageフェードアウトスピード
    private float alphaSpeed = 0.01f;
    private bool isFadeout = false;
    private UnityChanController uCC;
    private VOICEController vOICEController;
    private Image opImg;
    private PlayableDirector sceneAnimation;
    
    void Start () 
	{
        opImg = this.GetComponent<Image>();
        uCC = GameObject.Find("unitychan").GetComponent<UnityChanController>();
        vOICEController = GameObject.Find("VOICE").GetComponent<VOICEController>();
        sceneAnimation = GameObject.Find("GameSceneTimeline").GetComponent<PlayableDirector>();
    }
	
	void Update () 
	{
        ////Imageフェードアウト処理
        if (isFadeout == true)
        {
            opImg.color = new Color(opImg.color.r, opImg.color.g, opImg.color.b, alpha);
            alpha -= alphaSpeed;
            if (alpha <= 0)
            {
                opImg.enabled = false;
                opImg.color = new Color(opImg.color.r, opImg.color.g, opImg.color.b, 1.0f);
                isFadeout = false;
            }
        }
	}

　  //ボタンから呼び出すため記述
    public void CallcoRoutine()
    {
        StartCoroutine("OpcoRoutine");
    }


    //OPカウントダウンをするコルーチン
    public Sprite[] opCountImages = new Sprite[4];
    public IEnumerator OpcoRoutine()
    {
        yield return new WaitForSeconds(2);
        for (int i = 3; i >= 0; i--)
        {
            if (i == 0)
            {
                uCC.enabled = true;
                isFadeout = true;
                alpha = 1.0f;
            }
            opImg.sprite = opCountImages[i];
            opImg.enabled = true;
            vOICEController.CountVoiceSound(i);
            yield return new WaitForSeconds(1);
        }
    }
}
