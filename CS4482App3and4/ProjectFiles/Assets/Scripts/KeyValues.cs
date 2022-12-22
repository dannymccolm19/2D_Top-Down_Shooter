using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "keyValues", menuName = "KeyValues", order = 6)]
public class KeyValues : ScriptableObject
{
    public string[] dialogueKeys = new string [100];
    public string[] response1 = new string[100];
    public string[] response2 = new string[100];
    public string[] response3 = new string[100];
    public string[] response4 = new string[100];
    public string[] response5 = new string[100];

    public int[] responseLink1 = new int[100];
    public int[] responseLink2 = new int[100];
    public int[] responseLink3 = new int[100];
    public int[] responseLink4 = new int[100];
    public int[] responseLink5 = new int[100];

    public bool[] active1 = new bool[100];
    public bool[] active2 = new bool[100];
    public bool[] active3 = new bool[100];
    public bool[] active4 = new bool[100];
    public bool[] active5 = new bool[100];
    
    
    public int x = 0;
}
