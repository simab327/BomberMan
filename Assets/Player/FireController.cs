using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float deleteTime = 10;  //削除時間

    public GameObject firePrefab;      //矢のプレハブ

    Vector3 spos;

    // Use this for initialization
    void Start()
    {
        spos = transform.position;
        //Invoke("StopAttack", 1);
    }

    void Update()
    {
        Vector3 cpos = transform.position;

        if ((cpos - spos).magnitude > ItemKeeper.firePower)
        {
            Destroy(gameObject, 0.0f);
        }
    }


    //ゲームオブジェクトに接触
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Fire: OnCollisionEnter2D");
        //接触したゲームオブジェクトの子にする
        //transform.SetParent(collision.transform);

        //当たりを無効化する
        GetComponent<CircleCollider2D>().enabled = false;

        //物理シミュレーションを無効化する
        GetComponent<Rigidbody2D>().simulated = false;
        Destroy(gameObject, 0.0f);
    }

    //ゲームオブジェクトから脱出
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Fire: OnCollisionExit2D");
        //GetComponent<CircleCollider2D>().isTrigger = false;
    }

    //ゲームオブジェクトに接触
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Fire: OnTriggerEnter2D");
        //当たりを無効化する
        GetComponent<CircleCollider2D>().enabled = false;

        //物理シミュレーションを無効化する
        GetComponent<Rigidbody2D>().simulated = false;
        Destroy(gameObject, 0.0f);
    }

    //ゲームオブジェクトから脱出
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Fire: OnTriggerExit2D");
        //GetComponent<CircleCollider2D>().isTrigger = false;
    }

    public void StopAttack()
    {
        //Quaternion r = Quaternion.Euler(1, 0, 0);
        //GameObject fireObj = Instantiate(firePrefab, transform.position, r);
        //Rigidbody2D body = fireObj.GetComponent<Rigidbody2D>();
        Destroy(gameObject, 0.0f);
    }

}
