using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class DialougeDisplay : MonoBehaviour
{
    public CharacterDialouge myLine;
    public TMP_Text tBox;
    public AudioSource speaker;
    bool sayingLine = false;
    int i = 0;
    bool Finished = false;
    // Start is called before the first frame update
    void Start()
    {
        tBox = GetComponentInChildren<TMP_Text>();
        speaker = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!speaker.isPlaying && sayingLine)
        {
            sayingLine = false;
            i++;
            playLine(myLine);
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        playLine(myLine);
    }


    void playLine(CharacterDialouge CHAR)
    {
        if (Finished) return;

        if (!sayingLine && CHAR.voiceLine[i] != null)
        {
                sayingLine = true;
            {
                tBox.text = CHAR.voiceLine[i].voiceLine;
                speaker.clip = CHAR.voiceLine[i].voiceClip;
                speaker.Play();
            }
        }

        else Finished = true; return;
        
    }
}