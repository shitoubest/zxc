using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntVec3
{
	public int x;
	public int y;
	public int z;
}

public class Message
{
	public int frame;
	public Dictionary<int,List<Command>> commands = new Dictionary<int,List<Command>>();
}
public enum CommandType
{
    Move,
    MoveStop,
    Attack,
}

public class Command
{
    public CommandType type;
    public Command(CommandType type)
    {
        this.type = type;
    }
}

public class MoveCommand : Command
{
    public MoveCommand(Vector3 dir) : base(CommandType.Move)
    {
        this.dir = dir;
    }
    public Vector3 dir;
}

public class Client : MonoBehaviour {


	public void SendMessage()
	{
		Message msg = new Message();
		msg.frame = clientFrame;
		msg.commands.Add(clientID,commandCache);
        Server.Instance.HandleMessage(msg);
		commandCache.Clear();
	}

    public void HandleMessage(Message msg)
    {
        clientFrame = msg.frame;
        foreach (var item in msg.commands)
        {
            if (item.Key == hostPlayer.playerID)
            {
                hostPlayer.UpdateCommand(item.Value);
            }
            else if (item.Key == otherPlayer.playerID)
            {
                otherPlayer.UpdateCommand(item.Value);
            }
        }
    }

	// Use this for initialization
	void Start () {
        Server.Instance.AddClient(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
        float deltaTime = Time.deltaTime;
        CheckSendMessage(deltaTime);
        TickFrame(deltaTime);

    }
    
	void TickFrame(float deltaTime)
	{
        TickInput(deltaTime);
        TickLogic(deltaTime);
    }
    
    void TickLogic(float deltaTime)
    {
        hostPlayer.UpdateLogic(deltaTime);
        otherPlayer.UpdateLogic(deltaTime);
    }

    void TickInput(float deltaTime)
    {
        Vector3 dest = transform.localPosition;
        bool isMove = false;
        if (Control1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                isMove = true;
                dest = dest + new Vector3(0, 1, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                isMove = true;
                dest = dest + new Vector3(-1, 0, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                isMove = true;
                dest = dest + new Vector3(0, -1, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                isMove = true;
                dest = dest + new Vector3(1, 0, 0);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                isMove = true;
                dest = dest + new Vector3(-1,0 , 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                isMove = true;
                dest = dest + new Vector3(1,0, 0);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                isMove = true;
                dest = dest + new Vector3(0, 1, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                isMove = true;
                dest = dest + new Vector3(0, -1, 0);
            }
        }
        if (isMove)
        {
            commandCache.Add(new MoveCommand((dest - transform.localPosition).normalized));
        }
//          test
//         hostPlayer.UpdateCommand(commandCache);
//         commandCache.Clear();
    }

    public void CheckSendMessage(float deltaTime)
    {
        sendTime -= deltaTime;
        if (sendTime <= 0)
        {
            SendMessage();
            sendTime = oneFrameTime / 2;
        }
    }

    public bool Control1;
    float sendTime = 0;
	int clientFrame = 0;
    int serverFrame = 0;
    public int clientID;
	public List<Command> commandCache = new List<Command>();
	public static int LogicFPS = 10;
    private float oneFrameTime = 1f / LogicFPS;
    public Player hostPlayer;
    public Player otherPlayer;
}
