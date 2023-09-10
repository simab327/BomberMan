using System.Collections.Generic;
using UnityEngine;

//BGMタイプ
public enum BGMType
{
    None,       //なし
    Title,      //タイトル
    InGame,     //ゲーム中
    InBoss,     //ボス戦
}
//SEタイプ
public enum SEType
{
    GameClear,  //ゲームクリア
    GameOver,   //ゲームオーバ
    Shoot,      //矢を射る

    DoorOpen,   //ドア開く
    DoorClosed, //ドア閉じている
    ItemGet,    //アイテムゲット
    GetDamage,  //ダメージ
    EnemyKilled,//敵死亡
    BossKilled, //ボス死亡
    Button,     //ボタン押し
}

public class SoundManager : MonoBehaviour
{
    public AudioClip bgmInTitle;    //タイトルBGM
    public AudioClip bgmInGame;     //ゲーム中
    public AudioClip bgmInBoss;     //ボス戦BGM

    public AudioClip meGameClear;   //ゲームクリア
    public AudioClip meGameOver;    //ゲームオーバー
    public AudioClip seShoot;       //矢を射る

    public AudioClip seDoorOpen;    //ドア開く
    public AudioClip seDoorClosed;  //ドア閉まってる
    public AudioClip seItemGet;     //アイテムゲット
    public AudioClip seDamage;      //ダメージ
    public AudioClip seEnemyDead;   //敵死亡
    public AudioClip seBossDead;    //ボス死亡
    public AudioClip seButton;      //ボタン押し

    public static SoundManager soundManager;    //最初のSoundManagerを保存する変数

    public static BGMType plyingBGM = BGMType.None;    //再生中のBGM

    private void Awake()
    {
        //BGM再生
        if (soundManager == null)
        {
            soundManager = this;  //static変数に自分を保存する
            //シーンが変わってもゲームオブジェクトを破棄しない
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);//ゲームオブジェクトを破棄
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //BGM設定
    public void PlayBgm(BGMType type)
    {
        if (type != plyingBGM)
        {
            plyingBGM = type;
            AudioSource audio = GetComponent<AudioSource>();
            if (type == BGMType.Title)
            {
                audio.clip = bgmInTitle;    //タイトル
            }
            else if (type == BGMType.InGame)
            {
                audio.clip = bgmInGame;     //ゲーム中
            }
            else if (type == BGMType.InBoss)
            {
                audio.clip = bgmInBoss;     //ボス戦
            }
            audio.Play();
        }
    }
    //BGM停止
    public void StopBgm()
    {
        GetComponent<AudioSource>().Stop();
        plyingBGM = BGMType.None;
    }

    //SE再生
    public void SEPlay(SEType type)
    {
        if (type == SEType.GameClear)
        {
            GetComponent<AudioSource>().PlayOneShot(meGameClear);   //ゲームクリア
        }
        else if (type == SEType.GameOver)
        {
            GetComponent<AudioSource>().PlayOneShot(meGameOver);   //ゲームオーバー
        }
        else if (type == SEType.Shoot)
        {
            GetComponent<AudioSource>().PlayOneShot(seShoot);       //矢を射る
        }

        else if (type == SEType.DoorClosed)
        {
            GetComponent<AudioSource>().PlayOneShot(seDoorClosed);  //ドア閉まっている
        }
        else if (type == SEType.DoorOpen)
        {
            GetComponent<AudioSource>().PlayOneShot(seDoorOpen);    //ドア開く
        }
        else if (type == SEType.ItemGet)
        {
            GetComponent<AudioSource>().PlayOneShot(seItemGet);     //アイテムゲット
        }
        else if (type == SEType.GetDamage)
        {
            GetComponent<AudioSource>().PlayOneShot(seDamage);      //ダメージ
        }
        else if (type == SEType.EnemyKilled)
        {
            GetComponent<AudioSource>().PlayOneShot(seEnemyDead);   //敵死亡
        }
        else if (type == SEType.BossKilled)
        {
            GetComponent<AudioSource>().PlayOneShot(seBossDead);    //ボス死亡
        }
        else if (type == SEType.Button)
        {
            GetComponent<AudioSource>().PlayOneShot(seButton);      //ボタン押し
        }
    }

}

