using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{

    public float shootSpeed = 12.0f;    //矢の速度
    public float shootDelay = 0.25f;    //発射間隔

    public float deleteTime = 10;  //削除時間

    public GameObject firePrefab;      //矢のプレハブ

    // Use this for initialization
    void Start()
    {
        Invoke("expBomb", 3);
    }
    //ゲームオブジェクトに接触
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bomb: OnCollisionEnter2D");
        //Destroy(this, 0.0f);
    }

    //ゲームオブジェクトから脱出
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Bomb: OnCollisionExit2D");
        GetComponent<CircleCollider2D>().isTrigger = false;
    }

    //ゲームオブジェクトに接触
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bomb: OnTriggerEnter2D");
    }

    //ゲームオブジェクトから脱出
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Bomb: OnTriggerExit2D");
        GetComponent<CircleCollider2D>().isTrigger = false;
    }

    public void expBomb()
    {
        float[] array = { 0.0f, 90.0f, 180.0f, 270.0f };

        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        Destroy(gameObject, 0.0f);

        int px = (int)(transform.position.x + 10.5f);
        int py = (int)((transform.position.y - 5.5f) * -1.0f);
        ItemKeeper.delArrayBomb(px, py);

        foreach (int i in array)
        {
            float angleZ = i;
            Quaternion r0 = Quaternion.Euler(0, 0, angleZ);
            GameObject fireObj0 = Instantiate(firePrefab, transform.position, r0);
            float x0 = Mathf.Cos(angleZ * Mathf.Deg2Rad);
            float y0 = Mathf.Sin(angleZ * Mathf.Deg2Rad);
            Vector3 v0 = new Vector3(x0, y0) * shootSpeed;
            //Vector3 v0 = new Vector3(x0, y0) * ItemKeeper.firePower;
            Rigidbody2D body0 = fireObj0.GetComponent<Rigidbody2D>();
            //body0.constraints = RigidbodyConstraints2D.FreezePositionY;
            body0.AddForce(v0, ForceMode2D.Impulse);
        }
    }
}
