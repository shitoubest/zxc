using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSort : MonoBehaviour {
    
    public int arrayNum = 10;
    public List<int> array = new List<int>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Q))
        {
            Init();
            Print();
            DoQuickSort();
            Print();
            Verify(array);
        }
	}

    private void Init()
    {
        array.Clear();
        for (int i = 0; i < arrayNum; i++)
        {
            array.Add(Random.Range(0,100));
        }
    }

    private void Print()
    {
        string rs = "";
        foreach (var item in array)
        {
            rs += (item + " , ");
        }
        Debug.Log(rs);
    }

    private void DoQuickSort()
    {
        int length = array.Count;
        //int mark = length / 3;
        Sort(array,0, length - 1);

    }

    private void Sort(List<int> iArray, int low, int high)
    {
        if (low >= high)
        {
            return;
        }
        int j = partition(iArray,low,high);
        Sort(iArray,low,j-1);
        Sort(iArray,j+1,high);
    }

    private int partition(List<int> iArray,int i , int j)
    {
        int low = i;
        int high = j;
        int k = iArray[i];

        while(true)
        {
            while (iArray[++i] < k)
            {
                if (i==high)
                {
                    break;
                }
            }
            while (iArray[j] > k)
            {
                j--;
                if (j == low)
                {
                    break;
                }
            }
            if (i >= j)
            {
                break;
            }
            Exch(iArray, i, j);
        }
        Exch(iArray,low,j);
        return j;
    }
    private void Exch(List<int> iArray,int i ,int j)
    {
        int temp = iArray[i];
        iArray[i] = iArray[j];
        iArray[j] = temp;
    }

    private void Verify(List<int> iArray)
    {
        int temp = iArray[0];
        for (int i = 0; i < iArray.Count; i++)
        {
            if (iArray[i] < temp)
            {
                Debug.LogError("error!!");
            }
            temp = iArray[i];
        }
    }

}
