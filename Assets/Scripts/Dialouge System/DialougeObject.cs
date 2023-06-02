using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialouges/Dialouge Object")]
public class DialougeObject : ScriptableObject
{
    [SerializeField] [TextArea] string[] dialouge;
    [SerializeField] Response[] responses;

    public string[] Dialouge => dialouge;
    public bool HasResponses => Responses != null && Responses.Length > 0;
    public Response[] Responses => responses;
}
