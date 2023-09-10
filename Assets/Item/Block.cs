using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int arrangeId = 0;
    public GameObject doorPrefab;
    public GameObject itemPrefab;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Block: OnCollisionEnter2D");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("Block: OnCollisionExit2D");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Block: OnTriggerEnter2D");
        if (collision.gameObject.tag == "Fire")
        {
            int px = (int)(transform.position.x + 10.5f);
            int py = (int)((transform.position.y - 5.5f) * -1.0f);
            ItemKeeper.delArrayBlock(px, py);
            //Debug.Log("Block: px " + px + ", py " + py);
            Destroy(this.gameObject);

            Quaternion r = Quaternion.Euler(0, 0, 0);
            Vector3 pos = transform.position;
            pos.x = -10.5f + 1.0f * px;
            pos.y = 5.5f - 1.0f * py;

            int val = ItemKeeper.getArray(px, py);
            //if (val % 8 >= 4)
            if (val >= Constants.cFirePower)
            {
                GameObject itemObj = Instantiate(itemPrefab, pos, r);
                ItemBox ib = itemObj.GetComponent<ItemBox>();
                ib.itemId = val;

            }
            //if (val % 4 >= 2)
            if (val == Constants.cDoor)
            {
                GameObject doorObj = Instantiate(doorPrefab, pos, r);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Block: OnTriggerExit2D");
    }
}
