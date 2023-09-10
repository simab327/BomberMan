using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //移動スピード
    public float speed = 3.0f;
    //アニメーション名
    public string upAnime = "PlayerUp";         // 上向
    public string downAnime = "PlayerDown";     // 下向
    public string rightAnime = "PlayerRight";   // 右向
    public string leftAnime = "PlayerLeft";     // 左向
    public string deadAnime = "PlayerDead";     // 死亡
    //現在のアニメーション
    string nowAnimation = "";
    //以前のアニメーション 
    string oldAnimation = "";

    public float step = 1.0f;

    public float oldX;
    public float oldY;
    public Vector2 velo;

    public float axisHin;                    //横軸値（-1.0 〜 0.0 〜 1.0）
    public float axisVin;                    //縦軸値（-1.0 〜 0.0 〜 1.0）
    public float axisH;                    //横軸値（-1.0 〜 0.0 〜 1.0）
    public float axisV;                    //縦軸値（-1.0 〜 0.0 〜 1.0）

    public float angleZ = -90.0f;   //回転角

    Rigidbody2D rbody;          	   //Rigidbody 2D
    bool isMoving = false;          //移動中フラグ

    public bool isWalking = false;          //移動中フラグ


    //ダメージ対応
    public static int hp = 3;		//プレイヤーのHP
    public static string gameState;     //ゲームの状態
    bool inDamage = false;              //ダメージ中フラグ

    // Use this for initialization
    void Start()
    {
        //Rigidbody2Dを得る
        rbody = GetComponent<Rigidbody2D>();
        //アニメーション
        oldAnimation = downAnime;
        //ゲームの状態をプレイ中にする
        gameState = "playing";
        //HPの更新
        hp = PlayerPrefs.GetInt("PlayerHP");

        oldX = transform.position.x;
        oldY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム中以外とダメージ中は何もしない
        if (gameState != "playing" || inDamage || isWalking)
        {
            return;
        }
        if (isMoving == false)
        {
            axisHin = Input.GetAxisRaw("Horizontal"); //左右キー入力
            axisVin = Input.GetAxisRaw("Vertical");   //上下キー入力
        }

        int px = (int) (transform.position.x + 10.5f);
        int py = (int) ((transform.position.y - 5.5f) * -1.0f);
        Debug.Log("PlayerController: px " + px + ", py " + py);
        Debug.Log("PlayerController: (R) " + ItemKeeper.getArray(px + 1, py));
        Debug.Log("PlayerController: (L) " + ItemKeeper.getArray(px - 1, py));
        Debug.Log("PlayerController: (U) " + ItemKeeper.getArray(px, py - 1));
        Debug.Log("PlayerController: (D) " + ItemKeeper.getArray(px, py + 1));

        //Debug.Log("PlayerController: firePower " + ItemKeeper.firePower);
        //Debug.Log("PlayerController: maxBombs " + ItemKeeper.maxBombs);
        //Debug.Log("PlayerController: hasRemote " + ItemKeeper.hasRemote);
        //Debug.Log("PlayerController: walkSpeed " + ItemKeeper.walkSpeed);
        //Debug.Log("PlayerController: wallThrough " + ItemKeeper.wallThrough);
        //Debug.Log("PlayerController: bombThrough " + ItemKeeper.bombThrough);
        //Debug.Log("PlayerController: fireman " + ItemKeeper.fireman);
        //Debug.Log("PlayerController: perfectman " + ItemKeeper.perfectman);

        axisH = 0;
        axisV = 0;;

        int ax = ItemKeeper.getArray((px + (int)axisHin), py);
        if (ax < Constants.cBlock && ax != Constants.cPillar)
        {
            axisH = axisHin;
        }
        int ay = ItemKeeper.getArray(px, (py - (int)axisVin));
        if (ay < Constants.cBlock && ay != Constants.cPillar)
        {
            axisV = axisVin;
        }

        //キー入力から移動角度を求める
        Vector2 fromPt = transform.position;
        //Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
        Vector2 toPt = new Vector2(fromPt.x + axisHin, fromPt.y + axisVin);

        angleZ = GetAngle(fromPt, toPt);

        //移動角度から向いている方向とアニメーション更新
        if (angleZ >= -45 && angleZ < 45)
        {
            //右向き
            nowAnimation = rightAnime;
        }
        else if (angleZ >= 45 && angleZ <= 135)
        {
            //上向き
            nowAnimation = upAnime;
        }
        else if (angleZ >= -135 && angleZ <= -45)
        {
            //下向き
            nowAnimation = downAnime;
        }
        else
        {
            //左向き
            nowAnimation = leftAnime;
        }
        // アニメを切り換える
        if (nowAnimation != oldAnimation)
        {
            oldAnimation = nowAnimation;
            GetComponent<Animator>().Play(nowAnimation);
        }
    }
    void FixedUpdate()
    {
        //ゲーム中以外は何もしない
        if (gameState != "playing")
        {
            return;
        }
        if (inDamage)
        {
            //ダメージ中点滅させる
            float val = Mathf.Sin(Time.time * 50);
            Debug.Log(val);
            if (val > 0)
            {
                //スプライトを表示
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                //スプライトを非表示
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            return; //ダメージ中は操作による移動させない
        }

        Vector3 pos = transform.position;

        if (oldX == pos.x && oldY == pos.y)
        {
            if (axisH == 0 && axisV == 0)
            {
                return;
            }
            //oldX = transform.position.x;
            //oldY = transform.position.y;
            //rbody.velocity = new Vector2(axisH, axisV) * speed;
            velo = new Vector2(axisH, axisV) * ItemKeeper.walkSpeed;
            rbody.velocity = velo;
            step = 1.0f;
            isWalking = true;
            axisH = 0;
            axisV = 0;
        }
        else if (Mathf.Abs(pos.x - oldX) >= 1.0f)
        {
            if (pos.x > oldX)
            {
                pos.x = oldX + 1.0f;
            }
            else
            {
                pos.x = oldX - 1.0f;
            }
            pos.y = oldY;
            transform.position = pos;
            rbody.velocity = new Vector2(0, 0);
            oldX = pos.x;
            isWalking = false;
        }
        else if (Mathf.Abs(pos.y - oldY) >= 1.0f)
        {
            if (pos.y > oldY)
            {
                pos.y = oldY + 1.0f;
            }
            else
            {
                pos.y = oldY - 1.0f;
            }
            pos.x = oldX;
            transform.position = pos;
            rbody.velocity = new Vector2(0, 0);
            oldY = pos.y;
            isWalking = false;
        }
        else
        {
            //Vector3 newpos = gameObject.transform.position;
            //if ((Mathf.Abs(pos.x - oldX) / step ) <= 0.01f)
            //{
            //    newpos.x = oldX;
            //    isWalking = false;
            //}
            //if ((Mathf.Abs(pos.y - oldY) / step ) <= 0.01f)
            //{
            //    newpos.y = oldY;
            //    isWalking = false;
            //}
            //gameObject.transform.position = newpos;
            ////transform.position = pos;
            //step += 1.0f;
            //if (step > 100)
            //{
            //    rbody.velocity = new Vector2(0, 0);
            //    isWalking = false;
            //}
            rbody.velocity = velo;
            //rbody.velocity = new Vector2(0, 0);
            //Debug.Log("PlayerController: step " + step);
            //Debug.Log("PlayerController: oldX " + oldX);
            //Debug.Log("PlayerController: oldY " + oldY);
            //Debug.Log("PlayerController: (Mathf.Abs(pos.x - oldX) / step ) " + (Mathf.Abs(pos.x - oldX) / step));
            //Debug.Log("PlayerController: (Mathf.Abs(pos.y - oldY) / step ) " + (Mathf.Abs(pos.y - oldY) / step));
            //Debug.Log("PlayerController: rbody.velocity " + rbody.velocity);
        }
        //移動速度を更新する
        //rbody.velocity = new Vector2(axisH, axisV) * speed;
    }

    public void SetAxis(float h, float v)
    {
        axisHin = h;
        axisVin = v;
        if (axisHin == 0 && axisVin == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
    //p1からp2の角度を返す
    float GetAngle(Vector2 p1, Vector2 p2)
    {
        float angle;
        if (axisHin != 0 || axisVin != 0)
        {
            //移動中であれば角度を更新する
            //p1からp2への差分（原点を０にするため）
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;
            //アークタンジェント２関数で角度（ラジアン）を求める
            float rad = Mathf.Atan2(dy, dx);
            //ラジアンを度に変換して返す
            angle = rad * Mathf.Rad2Deg;
        }
        else
        {
            //停止中であれば以前の角度を維持
            angle = angleZ;
        }
        return angle;
    }
    //接触（物理）
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GetDamage(collision.gameObject);
        }
    }

    //ダメージ
    void GetDamage(GameObject enemy)
    {
        if (gameState == "playing")
        {
            hp--;   //HPを減らす
            //HPの更新
            PlayerPrefs.SetInt("PlayerHP", hp);
            if (hp > 0)
            {
                //移動停止
                rbody.velocity = new Vector2(0, 0);
                //敵キャラの反対方向にヒットバックさせる
                Vector3 toPos = (transform.position - enemy.transform.position).normalized;
                rbody.AddForce(new Vector2(toPos.x * 4,
                                           toPos.y * 4),
                                           ForceMode2D.Impulse);
                //ダメージフラグON
                inDamage = true;
                Invoke("DamageEnd", 0.25f);

                //SE再生（ダメージ）
                SoundManager.soundManager.SEPlay(SEType.GetDamage);
            }
            else
            {
                //ゲームオーバー
                GameOver();
            }
        }
    }
    //ダメージ終了
    void DamageEnd()
    {
        //ダメージフラグOFF
        inDamage = false;
        //スプライトを元に戻す
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    //ゲームオーバー
    void GameOver()
    {
        Debug.Log("ゲームオーバー！！");
        gameState = "gameover";
        //=====================
        //ゲームオーバー演出
        //=====================
        //プレイヤーあたりを消す
        GetComponent<CircleCollider2D>().enabled = false;
        //移動停止
        rbody.velocity = new Vector2(0, 0);
        //重力を戻してプレイヤーを上に少し跳ねあげる演出
        rbody.gravityScale = 1;
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        // アニメーションを切り換える
        GetComponent<Animator>().Play(deadAnime);
        //１秒後にプレイヤーキャラクター消す
        Destroy(gameObject, 1.0f);

        //BGM停止
        SoundManager.soundManager.StopBgm();
        //SE再生（ゲームオーバー）
        SoundManager.soundManager.SEPlay(SEType.GameOver);
    }
}
