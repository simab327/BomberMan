using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int arrangeId = 0;       //配置の識別に使う

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //カギを持っている
            if (ItemKeeper.hasKeys > 0)
            {
                ItemKeeper.hasKeys--;   //カギを１つ減らす
                Destroy(this.gameObject);          //ドアを開ける（削除する）

                //配置Idの記録
                SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);

                //SE再生（ドア開ける）
                SoundManager.soundManager.SEPlay(SEType.DoorOpen);
            }
            else
            {
                //SE再生（ドア閉まってる）
                SoundManager.soundManager.SEPlay(SEType.DoorClosed);
            }
        }
    }
}
