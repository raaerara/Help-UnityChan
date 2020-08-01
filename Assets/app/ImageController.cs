using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class ImageController : MonoBehaviour
{
    private float alpha = 1.0f;
    private float alphaSpeed;
    private bool isFadeout = false;
    private GameObject unityChan;
    private Image image;
    

    void Start()
    {
        unityChan = GameObject.Find("unitychan");
        image = this.GetComponent<Image>();
    }

    void Update()
    {
        //アタッチしているImageのフェードアウト処理
        if (isFadeout == true)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            alpha -= alphaSpeed;

            if (alpha <= 0)
            {
                image.enabled = false;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);
                isFadeout = false;
            }
        }
    }

    //第二引数に入れた数値ずつフェードアウトさせる
    void Fadeout(float num)
    {
        //Update()のImageフェードアウト処理を開始
        isFadeout = true; 
        alphaSpeed = num;
    }

    //リトライボタン(Button_Retry)から呼び出す用。押したときに各オブジェクトをゲーム開始位置に戻す処理
    public void OnRetry(bool onlyFade)
    {
        if(onlyFade != true)
        {
            unityChan.transform.position = new Vector3(0, 0, 0);
            }
        Fadeout(0.1f);
    }
}
