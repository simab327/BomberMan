using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public int itemType;           //アイテムの種類
    public int count = 1;           //アイテム数

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ItemData: OnTriggerEnter2D");
        if (collision.gameObject.tag == "Player")
        {
            //++++ アイテム取得演出 ++++
            ////当たりを消す
            //gameObject.GetComponent<CircleCollider2D>().enabled = false;

            if (itemType == Constants.cFirePower)
            {
                ItemKeeper.firePower += 1;
            }
            else if (itemType == Constants.cMaxBombs)
            {
                ItemKeeper.maxBombs += 1;
            }
            else if (itemType == Constants.cWalkSpeed)
            {
                ItemKeeper.walkSpeed += 1;
            }

            //++++ アイテム取得演出 ++++
            //当たりを消す
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            //アイテムのRigidbody2Dを取ってくる
            Rigidbody2D itemBody = GetComponent<Rigidbody2D>();
            //重力を戻す
            itemBody.gravityScale = 2.5f;
            //上に少し跳ねあげる演出
            itemBody.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
            //0.5秒後に削除
            Destroy(gameObject, 0.5f);

            //配置Idの記録
            SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);

            //SE再生（アイテムゲット）
            SoundManager.soundManager.SEPlay(SEType.ItemGet);
        }
    }
}

