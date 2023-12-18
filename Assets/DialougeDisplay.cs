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
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        tBox = GetComponentInChildren<TMP_Text>();
        speaker = GetComponentInChildren<AudioSource>();
        canvas = GetComponentInChildren<Canvas>();
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
        canvas.enabled = true;
        if (i <= CHAR.voiceLine.Length - 1)
            if (!sayingLine && CHAR.voiceLine[i] != null && !Finished)
            {
                sayingLine = true;

                tBox.text = CHAR.voiceLine[i].voiceLine;
                speaker.clip = CHAR.voiceLine[i].voiceClip;
                speaker.Play();
            }
            else
            {
                canvas.enabled = false;

                Finished = true; return;
            }
        else canvas.enabled = false;
    }
}