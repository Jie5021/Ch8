using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    GameObject director;
    public AudioClip coinse;
    AudioSource aud;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        this.aud = GetComponent<AudioSource>();
        this.director = GameObject.Find("GameDirector"); //變數director存GameDirector這個物件
    }

    public void LButtonDown()
    {
        if (Time.timeScale == 0f) return; //鎖住左按鈕點擊
        transform.Translate(-1, 0, 0, Space.World);
        Debug.Log("左鍵觸發，目前 X = " + transform.position.x);
    }

    public void RButtonDown()
    {
        if (Time.timeScale == 0f) return; //鎖住右按鈕點擊
        transform.Translate(1, 0, 0, Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f) return; //時間到停止移動
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUI()) return; //如果按到左右移動鍵
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 將滑鼠游標位置轉換成主視角射向3D場景的射線
            RaycastHit hit; // 宣告一個碰撞結構變數，用來接收射線撞擊到物件時回傳的資訊
            // 發射物理射線檢測：
            // ray:要發射的射線
            // out hit:將碰撞到的物體資訊寫入hit變數
            // Mathf.Infinity:射線的最大距離為無限遠
            // 若射線有碰撞到任何帶有Collider的物體，條件即成立（回傳true）
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                float x = Mathf.RoundToInt(hit.point.x); // 取得射線擊中物體表面處的世界座標（hit.point），對X軸四捨五入取整數（網格吸附效果）
                transform.position = new Vector3(x, 0, 0); // 將player移動到新的位置
            }
        }
    }

    bool IsPointerOverUI()
    {
        //PointerEventData用來記錄「這次點擊發生在哪裡、狀態為何」的資料結構。先建立一個新的資料包，並把它綁定到目前的EventSystem
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        //Input.touchCount > 0：檢查螢幕上現在有沒有手指觸控
        //Input.GetTouch(0).position抓取手指按的位置
        //Input.mousePosition抓取滑鼠點的位置
        eventData.position = Input.touchCount > 0
            ? (Vector2)Input.GetTouch(0).position
            : (Vector2)Input.mousePosition;
        //因為畫面上可能有多個UI疊在一起（例如按鈕上的文字跟按鈕底圖），射線可能會同時穿透好幾個，所以用清單把所有命中的結果都存下來
        List<RaycastResult> results = new List<RaycastResult>();
        //從點擊位置向UI層發射射線，把所有命中的UI存入results清單
        EventSystem.current.RaycastAll(eventData, results);
        //若清單內有東西（大於0），代表點到了UI（回傳true）；否則為點在空白處（回傳false）
        return results.Count > 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Gold"))
        {
            this.aud.PlayOneShot(this.coinse);
            //呼叫GameDirector腳本內的GetGoldCoin()函式來加分
            this.director.GetComponent<GameDirector>().GetGoldCoin();
        }
        else if(other.gameObject.CompareTag("Silver"))
        {
            this.aud.PlayOneShot(this.coinse);
            //呼叫GameDirector腳本內的GetSilverCoin()函式來加分
            this.director.GetComponent<GameDirector>().GetSilverCoin();
        }
        else
        {
            this.aud.PlayOneShot(this.coinse);
            //呼叫GameDirector腳本內的GetBronzeCoin()函式來加分
            this.director.GetComponent<GameDirector>().GetBronzeCoin();
        }
        Destroy(other.gameObject);
    }
}
