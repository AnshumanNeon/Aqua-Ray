using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialougeUI : MonoBehaviour
{
    public TMP_Text textLabel;
    TypewriterEffect typewriter;
    ResponseHandler responseHandler;

    public bool IsOpen {get; private set;}
    public GameObject dialougeBox;

    // Start is called before the first frame update
    void Start()
    {
        typewriter = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialougeBox();
    }

    public void ShowDialouge(DialougeObject dialouge)
    {
        IsOpen = true;
        dialougeBox.SetActive(true);
        StartCoroutine(StepThroughDialouge(dialouge));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    IEnumerator StepThroughDialouge(DialougeObject dialougeObject)
    {
        for(int i = 0; i < dialougeObject.Dialouge.Length; i++)
        {
            string dialouge = dialougeObject.Dialouge[i];
            yield return RunTypingEffect(dialouge);

            textLabel.text = dialouge;

            if(i == dialougeObject.Dialouge.Length - 1 && dialougeObject.HasResponses) break;

            yield return null;
            yield return new WaitUntil(() => Input.anyKeyDown);
        }

        if(dialougeObject.HasResponses)
        {
            responseHandler.ShowResponses(dialougeObject.Responses);
        }
        else
        {
            CloseDialougeBox();
        }
    }

    IEnumerator RunTypingEffect(string dialouge)
    {
        typewriter.Run(dialouge, textLabel);

        while(typewriter.IsRunning)
        {
            yield return null;

            if(Input.anyKeyDown)
            {
                typewriter.Stop();
            }
        }
    }

    public void CloseDialougeBox()
    {
        IsOpen = false;
        dialougeBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
