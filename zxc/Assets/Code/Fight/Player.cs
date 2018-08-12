using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public void UpdateLogic(float deltaTime)
    {
        if (isMoving)
        {
            transform.localPosition = transform.localPosition + moveDir * deltaTime * moveSpeed;
        }
    }

    public void UpdateCommand(List<Command> commands)
    {
        isMoving = false;
        foreach (var item in commands)
        {
            if (item.type == CommandType.Move)
            {
                moveDir = (item as MoveCommand).dir;
                isMoving = true;
            }
        }
    }
    public Vector3 moveDir;
    public bool isMoving = false;
    public int moveSpeed = 20;
    public int playerID;
    public bool IsHost;
}
