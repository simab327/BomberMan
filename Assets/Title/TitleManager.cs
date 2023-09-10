using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject startButton;      //スタートボタン
    public GameObject continueButton;   //コンティニューボタン
    public string firstSceneName;       //ゲーム開始シーン名

    // Start is called before the first frame update
    void Start()
    {
        string sceneName = PlayerPrefs.GetString("LastScene");      //保存シーン
        if (sceneName == "")
        {
            continueButton.GetComponent<Button>().interactable = false; //無効化
        }
        else
        {
            continueButton.GetComponent<Button>().interactable = true; //有効化
        }
        //タイトルBGM再生
        SoundManager.soundManager.PlayBgm(BGMType.Title);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Fire3")))
        {
            StartButtonClicked();
        }
        else if ((Input.GetButtonDown("Fire2")) && continueButton.GetComponent<Button>().interactable)
        {
            ContinueButtonClicked();
        }
    }

    //スタートボタン押し
    public void StartButtonClicked()
    {
        //セーブデータをクリア
        PlayerPrefs.DeleteAll();
        //HPを戻す
        PlayerPrefs.SetInt("PlayerHP", 3);
        //ステージ情報をクリア
        PlayerPrefs.SetString("LastScene", firstSceneName); //シーン名初期化
        RoomManager.doorNumber = 0;

        SceneManager.LoadScene(firstSceneName);

        //SE再生（ボタン）
        SoundManager.soundManager.SEPlay(SEType.Button);
    }

    //つづきからボタン押し
    public void ContinueButtonClicked()
    {
        string sceneName = PlayerPrefs.GetString("LastScene");      //保存シーン
        RoomManager.doorNumber = PlayerPrefs.GetInt("LastDoor");    //ドア番号
        SceneManager.LoadScene(sceneName);

        //SE再生（ボタン）
        SoundManager.soundManager.SEPlay(SEType.Button);
    }
}

