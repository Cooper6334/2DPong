using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType type;
    const int SPEED = 5;
    [SerializeField] Rigidbody2D itemRigid;
    [SerializeField] TextMeshPro itemNameText;

    string[] itemName = { "L", "Q", "S" };
    public void Init(Vector2 position, ItemType itemType)
    {
        transform.position = position;
        itemRigid.velocity = SPEED * Vector2.down;
        type = itemType;
        itemNameText.text = itemName[(int)type];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Abyss":
                Destroy(this.gameObject);
                break;
            case "Player":
                GamePlayManager.Instance.GetItem(type);
                Destroy(this.gameObject);
                break;
        }
    }

    public enum ItemType
    {
        None = -1, Long = 0, BallQuick = 1, MoveSpeed = 2
    }
}
