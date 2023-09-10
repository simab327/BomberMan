﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    int hasKeys = 0;                    //カギの数
    int hasArrows = 0;                  //矢の所持数
    int hp = 0;                         //プレイヤーのHP
    public GameObject arrowText;        //矢の数を表示するText
    public GameObject keyText;          //カギの数を表示するText
    public GameObject hpImage;          //HPの数を表示するImage
    public Sprite life3Image;           //HP3画像
    public Sprite life2Image;           //HP2画像
    public Sprite life1Image;           //HP1画像
    public Sprite life0Image;           //HP0画像
    public GameObject mainImage;        // 画像を持つGameObject
    public GameObject resetButton;      // リセットボタン
    public Sprite gameOverSpr;          // GAME OVER画像
    public Sprite gameClearSpr;         // GAME CLEAR画像
    public GameObject inputPanel;       //バーチャルパッドと攻撃ボタンを配置した操作パネル

    public string retrySceneName = "";  //リトライするシーン名

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("UIManager.Start");
        UpdateItemCount();  //アイテム数更新
        UpdateHP();         //HP更新
        //画像を非表示にする
        Invoke("InactiveImage", 1.0f);
        resetButton.SetActive(false);  //ボタン非表示
    }

    // Update is called once per frame
    void Update()
    {
        UpdateItemCount();  //アイテム数更新
        UpdateHP();         //HP更新

        if (PlayerController.gameState == "gameend")
        {
            if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Fire3"))
            {
                Retry();
            }
            else if (Input.GetButtonDown("Cancel"))
            {
                SceneManager.LoadScene("Title");
            }
        }
    }

    //アイテム数更新
    void UpdateItemCount()
    {
        //矢
        if (hasArrows != ItemKeeper.hasArrows)
        {
            arrowText.GetComponent<Text>().text = ItemKeeper.hasArrows.ToString();
            hasArrows = ItemKeeper.hasArrows;
        }
        //カギ
        if (hasKeys != ItemKeeper.hasKeys)
        {
            keyText.GetComponent<Text>().text = ItemKeeper.hasKeys.ToString();
            hasKeys = ItemKeeper.hasKeys;
        }
    }

    //HP更新
    void UpdateHP()
    {
        //Player取得
        if (PlayerController.gameState != "gameend")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                if (PlayerController.hp != hp)
                {
                    hp = PlayerController.hp;
                    if (hp <= 0)
                    {
                        hpImage.GetComponent<Image>().sprite = life0Image;
                        //プレイヤー死亡！
                        resetButton.SetActive(true);    //ボタン表示
                        mainImage.SetActive(true);      //画像表示
                                                        // 画像を設定する
                        mainImage.GetComponent<Image>().sprite = gameOverSpr;
                        inputPanel.SetActive(false);      //操作UI非表示
                        PlayerController.gameState = "gameend";   //ゲーム終了
                    }
                    else if (hp == 1)
                    {
                        hpImage.GetComponent<Image>().sprite = life1Image;
                    }
                    else if (hp == 2)
                    {
                        hpImage.GetComponent<Image>().sprite = life2Image;
                    }
                    else
                    {
                        hpImage.GetComponent<Image>().sprite = life3Image;
                    }
                }
            }
        }
    }

    //リトライ
    public void Retry()
    {
        //HPを戻す
        PlayerPrefs.SetInt("PlayerHP", 3);

        //BGMをクリア
        SoundManager.plyingBGM = BGMType.None;

        //ゲーム中に戻す
        SceneManager.LoadScene(retrySceneName);   //シーン移動
    }

    //画像を非表示にする
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    //ゲームクリア
    public void GameClear()
    {
        //画像表示
        mainImage.SetActive(true);
        mainImage.GetComponent<Image>().sprite = gameClearSpr;//「GAMR CLEAR」を設定する
        //操作UI非表示
        inputPanel.SetActive(false);
        //ゲームクリアにする
        PlayerController.gameState = "gameclear";
        //3秒後にタイトルに戻る
        Invoke("GoToTitle", 3.0f);
    }
    //タイトルに戻る
    void GoToTitle()
    {
        PlayerPrefs.DeleteKey("LastScene");     //保存シーンを削除
        SceneManager.LoadScene("Title");        //タイトルに戻る
    }
}
