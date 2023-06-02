using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] string responseText;
    [SerializeField] DialougeObject dialougeObject;

    public string ResponseText => responseText;
    public DialougeObject DialougeObject => dialougeObject;
}
