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
        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 將滑鼠游標位置轉換成主視角射向3D場景的射線
            RaycastHit hit; // 宣告一個碰撞結構變數，用來接收射線撞擊到物件時回傳的資訊
            // 發射物理射線檢測：
            // ray:要發射的射線
            // out hit:將碰撞到的物體資訊寫入hit變數
            // Mathf.Infinity:射線的最大距離為無限遠
            // 若射線有碰撞到任何帶有Collider的物體，條件即成立（回傳true）
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) // 
            {
                float x = Mathf.RoundToInt(hit.point.x); // 取得射線擊中物體表面處的世界座標（hit.point），對X軸四捨五入取整數（網格吸附效果）
                transform.position = new Vector3(x, 0, 0); // 將basket移動到新的位置
            }
        }*/
    }

    bool IsPointerOverUI()
    {
        if (EventSystem.current == null) return false;
        // 電腦滑鼠判斷
        if (EventSystem.current.IsPointerOverGameObject()) return true;
        // 手機觸控判斷（iOS/Android 必須帶入 fingerId 才準確）
        if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return true;
        return false;
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
