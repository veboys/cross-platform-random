using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
#if (UNITY_IOS || UNITY_IPHONE || UNITY_WEBGL) && !UNITY_EDITOR
    [DllImport("__Internal")] 
#else
    [DllImport("random_for_unity")]
#endif
    private static extern void set_random_seed(uint seed);

#if (UNITY_IOS || UNITY_IPHONE || UNITY_WEBGL) && !UNITY_EDITOR
    [DllImport("__Internal")] 
#else
    [DllImport("random_for_unity")]
#endif
    private static extern uint get_random_num();

    [SerializeField]
    private Text Text;

    void Start()
    {
        int total = 1000000;

        var sb = new StringBuilder();

        set_random_seed(123456);

        Dictionary<int, int> dict = new Dictionary<int, int>();

        for (int i = 0; i < total; i++)
        {
            var n = (int)(get_random_num() % 73);

            if (dict.ContainsKey(n))
            {
                dict[n] += 1;
            }
            else
            {
                dict[n] = 0;
            }
        }

        foreach (var item in dict)
        {
            sb.AppendLine($"{item.Key:00}---->{item.Value*1f/total:f6}");
        }

        Text.text = sb.ToString();
    }
}
