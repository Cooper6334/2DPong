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
        if(hp <= 0)
        {
            switch (item)
            {
                default: break;
            }
            Destroy(gameObject);
        }
    }
}
