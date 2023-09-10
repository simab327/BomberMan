﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//出入り口の位置
public enum ExitDirection
{
    right,  //右方向
    left,   //左方向
    down,   //下方向
    up,     //上方向
}

public class Exit : MonoBehaviour
{
    public string sceneName = "";   //異動先のシーン名
    public int doorNumber = 0;      //ドア番号
    public ExitDirection direction = ExitDirection.down;//ドアの位置

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (doorNumber == 100)
            {
                //BGM停止
                SoundManager.soundManager.StopBgm();
                //SE再生（ゲームクリア）
                SoundManager.soundManager.SEPlay(SEType.GameClear);
                //ゲームクリアにする
                GameObject.FindObjectOfType<UIManager>().GameClear();
            }
            else
            {
                RoomManager.ChangeScene(sceneName, doorNumber);
            }
        }
    }
}

