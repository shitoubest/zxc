using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atoi : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TestFunc();
        }
	}
    
    int StrToInt(char[] str)
    {
        if (str == null || str.Length == 0)
        {
            Debug.LogError("error");
            return 0;
        }
        long num = 0;
        int startNum = 0;
        int minus = -1;
        if (str[0] == '+')
        {
            startNum = 1;
            minus = 1;
        }
        else if (str[0] == '-')
        {
            startNum = 1;
            minus = -1;
        }
        for (int i = startNum; i < str.Length; i++)
        {
            if (str[i] >= '0' && str[i] <= '9')
            {
                num = num * 10 + (str[i] - '0');
                if (num * minus > int.MaxValue || num * minus < int.MinValue)
                {
                    Debug.LogError("error");
                    return 0;
                }
            }
            else
            {
                Debug.LogError("error");
                return 0;
            }
        }
        return (int)(num * minus);
    }

    void Test(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            Debug.LogError("error");
            return;
        }
        string log = str;
        int rs = StrToInt(str.ToCharArray());
        Debug.Log(log + " ==> " + rs);

    }

    void TestFunc()
    {
        Test(null);

        Test("");

        Test("123");

        Test("+123");

        Test("-123");

        Test("1a33");

        Test("+0");

        Test("-0");

        //有效的最大正整数, 0x7FFFFFFF
        Test("+2147483647");

        Test("-2147483647");

        Test("+2147483648");

        //有效的最小负整数, 0x80000000
        Test("-2147483648");

        Test("+2147483649");

        Test("-2147483649");

        Test("+");

        Test("-");
        
    }

}
