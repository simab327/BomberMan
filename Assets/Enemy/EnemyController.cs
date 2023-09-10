using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ヒットポイント
    public int hp = 3;
    //移動スピード
    public float speed = 0.5f;
    // 反応距離
    public float reactionDistance = 4.0f;
    //アニメーション名
    public string idleAnime = "EnemyIdle";		// 停止
    public string upAnime = "EnemyUp";          // 上向
    public string downAnime = "EnemyDown";		// 下向
    public string rightAnime = "EnemyRight";    // 右向
    public string leftAnime = "EnemyLeft";		// 左向
    public string deadAnime = "EnemyDead";		// 死亡
    //現在のアニメーション
    string nowAnimation = "";
    //以前のアニメーション 
    string oldAnimation = "";

    float axisH;            //横軸値（-1.0 〜 0.0 〜 1.0）
    float axisV;            //縦軸値（-1.0 〜 0.0 〜 1.0）
    Rigidbody2D rbody;      //Rigidbody 2D

    bool isActive = false;      //アクティブフラグ

    public int arrangeId = 0;   //配置の識別に使う

    // Use this for initialization
    void Start()
    {
        //Rigidbody2Dを得る
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Playerのゲームオブジェクトを得る
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (isActive)
            {
                //プレイヤーへの角度を求める
                float dx = player.transform.position.x - transform.position.x;
                float dy = player.transform.position.y - transform.position.y;
                float rad = Mathf.Atan2(dy, dx);
                float angle = rad * Mathf.Rad2Deg;
                //移動角度でアニメーションを変更する
                if (angle > -45.0f && angle <= 45.0f)
                {
                    nowAnimation = rightAnime;
                }
                else if (angle > 45.0f && angle <= 135.0f)
                {
                    nowAnimation = upAnime;
                }
                else if (angle >= -135.0f && angle <= -45.0f)
                {
                    nowAnimation = downAnime;
                }
                else
                {
                    nowAnimation = leftAnime;
                }
                //移動するベクトルを作る
                axisH = Mathf.Cos(rad) * speed;
                axisV = Mathf.Sin(rad) * speed;
            }
            else
            {
                //プレイヤーとの距離チェック
                float dist = Vector2.Distance(transform.position, player.transform.position);
                if (dist < reactionDistance)
                {
                    isActive = true;    //アクティブにする
                }
            }
        }
        else if (isActive)
        {
            isActive = false;
            rbody.velocity = Vector2.zero;
        }
    }
    void FixedUpdate()
    {
        if (isActive && hp > 0)
        {
            //移動
            rbody.velocity = new Vector2(axisH, axisV);
            if (nowAnimation != oldAnimation)
            {
                // アニメを切り換える
                oldAnimation = nowAnimation;
                Animator animator = GetComponent<Animator>();
                animator.Play(nowAnimation);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            //ダメージ
            hp--;
            if (hp <= 0)
            {
                //死亡！
                //=====================
                //死亡演出
                //=====================
                //当たりを消す
                GetComponent<CircleCollider2D>().enabled = false;
                //移動停止
                rbody.velocity = new Vector2(0, 0);
                // アニメを切り換える
                Animator animator = GetComponent<Animator>();
                animator.Play(deadAnime);
                //0.5秒後に消す
                Destroy(gameObject, 0.5f);

                //SE再生（敵死亡）
                SoundManager.soundManager.SEPlay(SEType.EnemyKilled);

                //配置Idの記録
                SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
            }
        }
    }
}
