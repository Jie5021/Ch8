using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinGenerator : MonoBehaviour
{
    public GameObject GoldPrefab;
    public GameObject SilverPrefab;
    public GameObject BronzePrefab;
    float span = 1.0f;      //生成的時間間隔
    float delta = 0;        //計時器累加變數，記錄自上次生成以來經過的時間
    int goldratio = 2;      //金硬幣生成的機率權重
    int silverratio = 3;    //銀硬幣生成的機率權重
    float speed = -0.1f;    //硬幣的下落速度
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void setParameter(float span, float speed, int goldratio, int silverratio) //能讓其他腳本能調整遊戲參數
    {
        this.span = span;
        this.speed = speed;
        this.goldratio = goldratio;
        this.silverratio = silverratio;
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime; //累加每幀經過的時間
        if (this.delta > this.span) //當累加時間超過設定的生成間隔
        {
            this.delta = 0; //重置計時器
            GameObject item; //宣告生成出來的道具變數
            float dice = Random.Range(1, 10); // 隨機產生範圍為[1, 10)的數，即1~10
            if(dice <= this.goldratio) //當dice小於等於goldratio(預設為 2)時生成金硬幣，20%
            {
                item = Instantiate(GoldPrefab);
            }
            else if (dice <= this.goldratio + this.silverratio) //當dice小於等於goldratio + silverratio = 5，時生成銀硬幣，30%
            {
                item = Instantiate(SilverPrefab);
            }
            else //當dice不是1~5(是6~10)時生成銅硬幣，50%
            {
                item = Instantiate(BronzePrefab);
            }
            //隨機決定生成位置x軸上，範圍是-5~5
            float x = Random.Range(-5, 5);
            item.transform.position = new Vector3(x, 8, 0); //將生成的道具放在前面隨機位置產生器產生的位置，且高度固定4
            item.GetComponent<CoinController>().dropspeed = this.speed; //將道具的下落速度傳遞給道具身上的ItemController腳本
        }
    }
}
