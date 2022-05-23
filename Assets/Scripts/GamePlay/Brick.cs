using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] int item;

    public void HitByBall()
    {
        hp--;
        if (hp <= 0)
        {
            int itemID = -1;
            if (item > 0)
            {
                itemID = (int)Random.Range(0, 3);
            }
            GamePlayManager.Instance.OnBrickDestroy(transform.position, (Item.ItemType)itemID);
            Destroy(gameObject);
        }
    }
}
