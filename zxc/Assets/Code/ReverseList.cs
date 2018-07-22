using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseList : MonoBehaviour {

    public class Node
    {
        public Node next;
        public int value;

        public static Node Reverse(Node node)
        {
            if (node.next != null)
            {
                var newNode = Reverse(node.next);
                node.next.next = node;
                node.next = null;
                return newNode;
            }
            else
            {
                return node;
            }
        }

        public void Reverse()
        {
            Node cur = this;
            Node nxt = cur.next;
            cur.next = null;
            while (nxt != null)
            {
                var nextNxt = nxt.next;
                nxt.next = cur;
                cur = nxt;
                nxt = nextNxt;
                if (nxt == null)
                {
                    curNode = cur;
                    break;
                }
            }
            
        }
        public void Print()
        {
            Node cur = this;
            Debug.Log(cur.value);
            while (cur.next != null)
            {
                Debug.Log(cur.next.value);
                cur = cur.next;
            }
        }
    }
    public static Node curNode;
	// Use this for initialization
	void Start () {
        curNode = new Node();
        var cur = curNode;
        for (int i = 1; i < 10; i++)
        {
            cur.value = i;
            cur.next = new Node();
            cur = cur.next;

        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (curNode != null)
            {
                curNode.Print();
            }
            else
            {
                Debug.LogError("curNode == null!");
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            curNode.Reverse();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            curNode = Node.Reverse(curNode);
        }
    }
}
