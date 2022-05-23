using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRigid;

    const float MAX_LENGTH = 3;
    const float DEFAULT_LENGTH = 1;
    const float DEFAULT_HEIGHT = 0.2f;
    const float DEFAULT_SPEED = 5;
    const float SPEED_UP = 5;
    const float LENGTH_UP = 1;
    const float DEFAULT_MOVE_RANGE = 1.9f;

    Direction direction = Direction.Stay;
    float speed = DEFAULT_SPEED;
    float length = DEFAULT_LENGTH;
    float moveRange = DEFAULT_MOVE_RANGE;

    public void SetDirection(Direction newDirection)
    {
        if (newDirection == Direction.Left && transform.position.x <= -moveRange)
        {
            newDirection = Direction.Stay;
        }
        else if (newDirection == Direction.Right && transform.position.x >= moveRange)
        {
            newDirection = Direction.Stay;
        }
        if (newDirection == direction)
        {
            return;
        }
        direction = newDirection;
        switch (direction)
        {
            case Direction.Left:
                playerRigid.velocity = speed * Vector2.left;
                break;
            case Direction.Right:
                playerRigid.velocity = speed * Vector2.right;
                break;
            case Direction.Stay:
                playerRigid.velocity = Vector2.zero;
                break;
        }
    }

    private void Update()
    {
        if (direction == Direction.Left && transform.position.x <= -moveRange)
        {
            SetDirection(Direction.Stay);
            transform.position = new Vector2(-moveRange, transform.position.y);
        }
        else if (direction == Direction.Right && transform.position.x >= moveRange)
        {
            SetDirection(Direction.Stay);
            transform.position = new Vector2(moveRange, transform.position.y);
        }
    }

    public Vector2 GetVelocity()
    {
        return playerRigid.velocity;
    }
    public void GetItem(Item.ItemType type)
    {
        switch (type)
        {
            case Item.ItemType.Long:
                length = Mathf.Clamp(length + LENGTH_UP, DEFAULT_LENGTH, MAX_LENGTH);
                moveRange -= LENGTH_UP / 2;
                transform.localScale = new Vector2(length, DEFAULT_HEIGHT);
                break;
            case Item.ItemType.MoveSpeed:
                speed += SPEED_UP;
                break;
        }
    }

    public enum Direction
    {
        Left = -1, Stay = 0, Right = 1
    }
}
