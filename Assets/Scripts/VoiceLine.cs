using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VoiceLine", menuName = "Dialouge/Voice", order = 2)]
public class VoiceLine : ScriptableObject 
{
    public AudioClip voiceClip;
    [TextArea]
    public  string voiceLine;
    public Sprite sprite;
}
