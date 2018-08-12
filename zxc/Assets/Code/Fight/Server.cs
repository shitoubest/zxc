using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour {

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.realtimeSinceStartup - curFrame * oneFrameTime > 1)
        {
            cacheMsg.frame = ++curFrame;
            SendMessage();
        }	
	}

    public void HandleMessage(Message msg)
    {
        foreach (var item in msg.commands)
        {
            if (!cacheMsg.commands.ContainsKey(item.Key))
            {
                cacheMsg.commands.Add(item.Key, new List<Command>());
            }
            cacheMsg.commands[item.Key].AddRange(item.Value);
        }
    }

    public void SendMessage()
    {
        for (int i = 0; i < clients.Count; i++)
        {
            clients[i].HandleMessage(cacheMsg);
        }
        foreach (var item in cacheMsg.commands.Values)
        {
            item.Clear();
        }
    }
    public void AddClient(Client client)
    {
        clients.Add(client);
    }
    List<Client> clients = new List<Client>();

    Message cacheMsg = new Message();

    
    private int curFrame = 0;
    private const int LogicFPS = 10;
    private float oneFrameTime = 1f / LogicFPS;
    public static Server Instance;
}
