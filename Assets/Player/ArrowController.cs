using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float deleteTime = 2;  //削除時間

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, deleteTime); //一定時間で消す
    }
    //ゲームオブジェクトに接触
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //接触したゲームオブジェクトの子にする
        transform.SetParent(collision.transform);
        //当たりを無効化する
        GetComponent<CircleCollider2D>().enabled = false;
        //物理シミュレーションを無効化する
        GetComponent<Rigidbody2D>().simulated = false;
    }
}
