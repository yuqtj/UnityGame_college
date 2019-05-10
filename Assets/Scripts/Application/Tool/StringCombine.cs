using UnityEngine;
using System.Collections;
using System.Text;


//字符串拼接工具类
public static class StringCombine{

    private static StringBuilder sb;


    static StringCombine()
    {
        sb = new StringBuilder();
    }

    public static string AppendStr(string sourStr)
    {
        sb.Append(sourStr);

        return sb.ToString();
    }

    public static string GetCurrentStr()
    {
        return sb.ToString();
    }

    public static void Clear()
    {
        sb.Length = 0;
    }
}
