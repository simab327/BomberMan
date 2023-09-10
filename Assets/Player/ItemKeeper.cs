using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Constants
{
    public const int cNull = 0;
    public const int cPillar = 1;
    public const int cBlock = 100;
    public const int cBomb = 200;
    public const int cDoor = 3;
    public const int cFirePower = 4;
    public const int cMaxBombs = 5;
    public const int cRemote = 6;
    public const int cWalkSpeed = 7;
    public const int cWallThrough = 8;
    public const int cBombThrough = 9;
    public const int cFireman = 10;
    public const int cPerfectman = 11;
}

public class ItemKeeper : MonoBehaviour
{
    public static int hasKeys = 0;          //カギの数
    public static int hasArrows = 0;        //矢の所持数
    public static int hasBombs = 1000;        //矢の所持数

    public static int firePower = 1;
    public static int maxBombs = 1;
    public static int hasRemote = 0;
    public static int walkSpeed = 2;
    public static int wallThrough = 0;
    public static int bombThrough = 0;
    public static int fireman = 0;
    public static int perfectman = 0;

    public GameObject blockPrefab;      //矢のプレハブ

    public int sx = 0;
    public int sy = 0;

    public static int[,] mapArray = new int[21,11];
    //int[] mapAarray = { 0.0f, 90.0f, 180.0f, 270.0f };

    // Start is called before the first frame update
    void Start()
    {
        hasKeys = PlayerPrefs.GetInt("Keys");
        hasArrows = PlayerPrefs.GetInt("Arrows");
        mapSet();
    }

    void Update()
    {

    }

    public static void SaveItem()
    {
        PlayerPrefs.SetInt("Keys", hasKeys);
        PlayerPrefs.SetInt("Arrows", hasArrows);
    }

    public static int getArray(int x, int y)
    {
        return mapArray[x, y];
    }

    public static void delArrayBlock(int x, int y)
    {
        mapArray[x, y] -= Constants.cBlock;
        //Debug.Log("ItemKeeper: x " + x + ", y " + y);
    }

    public static void setArrayBomb(int x, int y)
    {
        mapArray[x, y] += Constants.cBomb;
        //Debug.Log("ItemKeeper: x " + x + ", y " + y);
    }

    public static void delArrayBomb(int x, int y)
    {
        mapArray[x, y] -= Constants.cBomb;
        //Debug.Log("ItemKeeper: x " + x + ", y " + y);
    }

    public void mapSet()
    {
        for (int y = 0; y < 11; y++)
        {
            for (int x = 0; x < 21; x++)
            {
                if (x == 0 || x == 20 || ((x % 2 == 1) && (y % 2 == 1)) || y == 0 || y == 10)
                {
                    mapArray[x, y] = Constants.cPillar;
                }
                else
                {
                    mapArray[x, y] = Constants.cNull;
                }
            }
        }
        Debug.Log("ItemKeeper: map reset done");

        Quaternion r = Quaternion.Euler(0, 0, 0);
        Vector3 pos = transform.position;

        //ブロック
        int i = 1;
        while ( i < 60 )
        {
            int rndx = Random.Range(1, 19);
            int rndy = Random.Range(1, 9);
            if (mapArray[rndx, rndy] == Constants.cNull)
            {
                //Debug.Log("ItemKeeper: i " + i);
                //Debug.Log("ItemKeeper: rndx " + rndx);
                //Debug.Log("ItemKeeper: rndy " + rndy);
                mapArray[rndx, rndy] = Constants.cBlock;
                i++;
            }
        }
        //Debug.Log("ItemKeeper: loop done");

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int px = (int) (player.transform.position.x + 10.5f);
        int py = (int) ((player.transform.position.y - 5.5f) * -1.0f);
        //Debug.Log("ItemKeeper: px " + px + ", py " + py);

        mapArray[px, py] = 0;
        mapArray[px+1, py] = 0;
        mapArray[px-1, py] = 0;
        mapArray[px, py+1] = 0;
        mapArray[px, py-1] = 0;

        //ドア
        i = 1;
        while (i < 2)
        {
            int rndx = Random.Range(1, 19);
            int rndy = Random.Range(1, 9);
            if (mapArray[rndx, rndy] == Constants.cBlock)
            {
                mapArray[rndx, rndy] += Constants.cDoor;
                i++;
            }
        }

        //お宝
        i = 1;
        while (i < 5)
        {
            int rndx = Random.Range(1, 19);
            int rndy = Random.Range(1, 9);
            if (mapArray[rndx, rndy] == Constants.cBlock)
            {
                mapArray[rndx, rndy] += Constants.cFirePower;
                i++;
            }
        }

        i = 1;
        while (i < 3)
        {
            int rndx = Random.Range(1, 19);
            int rndy = Random.Range(1, 9);
            if (mapArray[rndx, rndy] == Constants.cBlock)
            {
                mapArray[rndx, rndy] += Constants.cMaxBombs;
                i++;
            }
        }

        i = 1;
        while (i < 3)
        {
            int rndx = Random.Range(1, 19);
            int rndy = Random.Range(1, 9);
            if (mapArray[rndx, rndy] == Constants.cBlock)
            {
                mapArray[rndx, rndy] += Constants.cRemote;
                i++;
            }
        }

        i = 1;
        while (i < 5)
        {
            int rndx = Random.Range(1, 19);
            int rndy = Random.Range(1, 9);
            if (mapArray[rndx, rndy] == Constants.cBlock)
            {
                mapArray[rndx, rndy] += Constants.cWalkSpeed;
                i++;
            }
        }

        i = 1;
        while (i < 3)
        {
            int rndx = Random.Range(1, 19);
            int rndy = Random.Range(1, 9);
            if (mapArray[rndx, rndy] == Constants.cBlock)
            {
                mapArray[rndx, rndy] += Constants.cWallThrough;
                i++;
            }
        }

        i = 1;
        while (i < 3)
        {
            int rndx = Random.Range(1, 19);
            int rndy = Random.Range(1, 9);
            if (mapArray[rndx, rndy] == Constants.cBlock)
            {
                mapArray[rndx, rndy] += Constants.cBombThrough;
                i++;
            }
        }

        i = 1;
        while (i < 3)
        {
            int rndx = Random.Range(1, 19);
            int rndy = Random.Range(1, 9);
            if (mapArray[rndx, rndy] == Constants.cBlock)
            {
                mapArray[rndx, rndy] += Constants.cFireman;
                i++;
            }
        }

        i = 1;
        while (i < 3)
        {
            int rndx = Random.Range(1, 19);
            int rndy = Random.Range(1, 9);
            if (mapArray[rndx, rndy] == Constants.cBlock)
            {
                mapArray[rndx, rndy] += Constants.cPerfectman;
                i++;
            }
        }

        //ブロック生成
        for (int y = 1; y < 9; y++)
        {
            for (int x = 1; x < 19; x++)
            {
                pos.x = -10.5f + 1.0f * x;
                pos.y = 5.5f - 1.0f * y;
                if (mapArray[x, y] >= Constants.cBlock)
                {
                    GameObject blockObj = Instantiate(blockPrefab, pos, r);
                }
            }
         }
    }

}
