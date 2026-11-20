using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class CoinController : MonoBehaviour
{
    public float dropspeed = -0.2f;
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f) return; //時間到後停止掉落
        transform.Translate(0, this.dropspeed, 0, Space.World);
        if (transform.position.y < -1.0f)
        {
            Destroy(gameObject);
        }
    }
}
