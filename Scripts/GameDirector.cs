using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameDirector : MonoBehaviour
{
    GameObject timerText;
    GameObject pointText;
    GameObject generator;
    TextMeshProUGUI levelText; // 宣告用來存放關卡文字元件的變數
    float time = 30.0f;
    int point = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GetGoldCoin()
    {
        this.point += 1000;
    }
    public void GetSilverCoin()
    {
        this.point += 100;
    }
    public void GetBronzeCoin()
    {
        this.point += 10;
    }

    void Start()
    {
        this.timerText = GameObject.Find("Time");
        this.pointText = GameObject.Find("Point");
        this.levelText = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        this.generator = GameObject.Find("CoinGenerator");
    }

    // Update is called once per frame
    void Update()
    {
        this.time -= Time.deltaTime;
        if (this.time <= 0)
        {
            this.time = 0;
            this.levelText.text = "Time Up!";
            this.generator.GetComponent<CoinGenerator>().setParameter(10000.0f, 0, 0, 0);
            Time.timeScale = 0f; // 凍結物理、動畫與 Time.deltaTime
            return;

        }
        else if (30 >= this.time && this.time > 20)
        {
            this.generator.GetComponent<CoinGenerator>().setParameter(1.0f, -0.04f, 2, 3);
            this.levelText.text = (this.time > 28.5f) ? "Level 1" : "";
        }
        else if (20 >= this.time && this.time > 10)
        {
            this.generator.GetComponent<CoinGenerator>().setParameter(0.8f, -0.05f, 2, 4);
            this.levelText.text = (this.time > 18.5f) ? "Level 2" : "";
        }
        else if (10 >= this.time && this.time > 5)
        {
            this.generator.GetComponent<CoinGenerator>().setParameter(0.5f, -0.07f, 3, 4);
            this.levelText.text = (this.time > 8.5f) ? "Level 3" : "";
        }
        else if (5 >= this.time && this.time > 0)
        {
            this.generator.GetComponent<CoinGenerator>().setParameter(0.6f, -0.06f, 3, 5);
            this.levelText.text = (this.time > 3.5f) ? "Level 4" : "";
        }
        this.timerText.GetComponent<TextMeshProUGUI>().text = this.time.ToString("F1");
        this.pointText.GetComponent<TextMeshProUGUI>().text = this.point.ToString() + " Point";
    }
}
