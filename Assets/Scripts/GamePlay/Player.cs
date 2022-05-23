using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRigid;

    const float DEFAULT_SPEED = 5;
    const float MOVE_RANGE = 1.9f;
    Direction direction = Direction.Stay;

    public void SetDirection(Direction newDirection)
    {
        if (newDirection == Direction.Left && transform.position.x <= -MOVE_RANGE)
        {
            newDirection = Direction.Stay;
        }
        else if (newDirection == Direction.Right && transform.position.x >= MOVE_RANGE)
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
                playerRigid.velocity = DEFAULT_SPEED * Vector2.left;
                break;
            case Direction.Right:
                playerRigid.velocity = DEFAULT_SPEED * Vector2.right;
                break;
            case Direction.Stay:
                playerRigid.velocity = Vector2.zero;
                break;
        }
    }

    private void Update()
    {
        if (direction == Direction.Left && transform.position.x <= -MOVE_RANGE)
        {
            SetDirection(Direction.Stay);
            transform.position = new Vector2( -MOVE_RANGE, transform.position.y);
        }
        else if (direction == Direction.Right && transform.position.x >= MOVE_RANGE)
        {
            SetDirection(Direction.Stay);
            transform.position = new Vector2(MOVE_RANGE, transform.position.y);
        }
    }

    public Vector2 GetVelocity()
    {
        return playerRigid.velocity;
    }

    public enum Direction
    {
        Left = -1, Stay = 0, Right = 1
    }
}
