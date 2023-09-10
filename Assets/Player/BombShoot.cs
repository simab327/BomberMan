using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShoot : MonoBehaviour
{
    public float shootSpeed = 12.0f;    //矢の速度
    public float shootDelay = 0.25f;    //発射間隔
    public GameObject bombPrefab;      //矢のプレハブ

    bool inSetting = false;               //攻撃中フラグ

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Fire3")))
        {
            bombSet();
        }
    }

    //攻撃
    public void bombSet()
    {
        //矢を持っている & 攻撃中ではない
        if (ItemKeeper.hasBombs > 0 && inSetting == false)
        {
            ItemKeeper.hasBombs -= 1;	//矢を減らす
            inSetting = true;		//攻撃フラグ立てる

            Quaternion r = Quaternion.Euler(0, 0, 0);
            Vector3 pos = transform.position;

            pos.x = Mathf.Round(pos.x);
            pos.y = Mathf.Round(pos.y);

            if (pos.x > transform.position.x)
            {
                pos.x -= 0.5f;
            }
            else
            {
                pos.x += 0.5f;
            }
            if (pos.y > transform.position.y)
            {
                pos.y -= 0.5f;
            }
            else
            {
                pos.y += 0.5f;
            }
            GameObject bombObj = Instantiate(bombPrefab, pos, r);
            Rigidbody2D body = bombObj.GetComponent<Rigidbody2D>();
            Invoke("StopSet", shootDelay);

            int px = (int) (pos.x + 10.5f);
            int py = (int) ((pos.y - 5.5f) * -1.0f);
            ItemKeeper.setArrayBomb(px, py);

            //SE再生（矢を射る）
            //SoundManager.soundManager.SEPlay(SEType.Shoot);
        }
    }

    //攻撃停止
    public void StopSet()
    {
        inSetting = false;    //攻撃フラグ下ろす
    }
}
