using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public Sprite openImage;        //開いた画像
    public int itemId;
    public GameObject itemPrefabFirePower;
    public GameObject itemPrefabMaxBombs;
    public GameObject itemPrefabRemote;
    public GameObject itemPrefabWalkSpeed;
    public GameObject itemPrefabWallThrough;
    public GameObject itemPrefabBombThrough;
    public GameObject itemPrefabFireman;
    public GameObject itemPrefabPerfectman;

    public bool isClosed = true;       //true=閉まっている　false=開いている

    public int arrangeId = 0;       //配置の識別に使う

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    //接触（物理）
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ItemBox: OnCollisionEnter2D");
        if (isClosed && collision.gameObject.tag == "Player")
        {
            //++++ アイテム取得演出 ++++
            //当たりを消す
            gameObject.GetComponent<CircleCollider2D>().enabled = false;

            //箱が閉まっている状態でプレイヤーに接触
            GetComponent<SpriteRenderer>().sprite = openImage;
            isClosed = false;   //開いてる状態にする
            //if (itemPrefab != null)
            //{
                //アイテムをプレハブから作る
                //Instantiate(itemPrefab, transform.position, Quaternion.identity);
                switch (itemId)
                {
                    case Constants.cFirePower:
                        Debug.Log("Instantiate(itemPrefabFirePower");
                        Instantiate(itemPrefabFirePower, transform.position, Quaternion.identity);
                        break;
                    case Constants.cMaxBombs:
                        Debug.Log("Instantiate(itemPrefabMaxBombs");
                        Instantiate(itemPrefabMaxBombs, transform.position, Quaternion.identity);
                        break;
                    case Constants.cRemote:
                        Debug.Log("Instantiate(itemPrefabRemote");
                        Instantiate(itemPrefabRemote, transform.position, Quaternion.identity);
                        break;
                    case Constants.cWalkSpeed:
                        Debug.Log("Instantiate(itemPrefabWalkSpeed");
                        Instantiate(itemPrefabWalkSpeed, transform.position, Quaternion.identity);
                        break;
                    case Constants.cWallThrough:
                        Debug.Log("Instantiate(itemPrefabWallThrough");
                        Instantiate(itemPrefabWallThrough, transform.position, Quaternion.identity);
                        break;
                    case Constants.cBombThrough:
                        Debug.Log("Instantiate(itemPrefabBombThrough");
                        Instantiate(itemPrefabBombThrough, transform.position, Quaternion.identity);
                        break;
                    case Constants.cFireman:
                        Debug.Log("Instantiate(itemPrefabFireman");
                        Instantiate(itemPrefabFireman, transform.position, Quaternion.identity);
                        break;
                    case Constants.cPerfectman:
                        Debug.Log("Instantiate(itemPrefabPerfectman");
                        Instantiate(itemPrefabPerfectman, transform.position, Quaternion.identity);
                        break;
                    default:
                        Debug.Log("Default");
                        break;
                }
            //}
            Destroy(gameObject, 0.5f);
            //配置Idの記録
            SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
        }
    }
}
