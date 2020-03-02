﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;
using UnityEngine.Profiling;
using System.Runtime.InteropServices;

public class Stats : MonoBehaviour
{
    public float Used { get; private set; }
    public float Unused { get; private set; }
    public float Total { get; private set; }

    public string UsedText { get; private set; }
    public string UnusedText { get; private set; }
    public string TotalText { get; private set; }

    public void Update()
    {
        // Unity によって割り当てられたメモリ
        Used = (Profiler.GetTotalAllocatedMemoryLong() >> 10) / 1024f;

        // 予約済みだが割り当てられていないメモリ
        Unused = (Profiler.GetTotalUnusedReservedMemoryLong() >> 10) / 1024f;

        // Unity が現在および将来の割り当てのために確保している総メモリ
        Total = (Profiler.GetTotalReservedMemoryLong() >> 10) / 1024f;

        UsedText = Used.ToString("0.0") + " MB";
        UnusedText = Unused.ToString("0.0") + " MB";
        TotalText = Total.ToString("0.0") + " MB";

        var sb = new StringBuilder();
        sb.AppendLine("<b>Unity</b>");
        sb.AppendLine();
        sb.AppendLine($"    Used: {UsedText}");
        sb.AppendLine($"    Unused: {UnusedText}");
        sb.AppendLine($"    Total: {TotalText}");

        var text = sb.ToString();
        m_text.text = text;
    }

    private void Start()
    {
        
    }

    [DllImport("__Internal")]
    public static extern long GetMemorySize();

    [SerializeField]
    private TMP_Text m_text;
}
