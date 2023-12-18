using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exampleCode : MonoBehaviour
{
    public CharacterDialouge myLine;
    bool saidLine = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        playLine(myLine);
    }


    void playLine(CharacterDialouge CHAR)
    {
        saidLine = true;

        // HANDLE THING HERE;
    }
}
