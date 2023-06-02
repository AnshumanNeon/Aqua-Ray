using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public bool IsRunning {get; private set;}
    public float typeSpeed;

    readonly List<Puntuation> punctuations = new List<Puntuation>()
    {
        new Puntuation(new HashSet<char>() {'.', '!', '?'}, 0.6f),
        new Puntuation(new HashSet<char>() {',', ';', ':'}, 0.3f)
    };

    Coroutine typeCoroutine;

    public void Run(string text, TMP_Text textLabel)
    {
        typeCoroutine = StartCoroutine(Type(text, textLabel));
    }

    public void Stop()
    {
        StopCoroutine(typeCoroutine);
        IsRunning = false;
    }

    IEnumerator Type(string text, TMP_Text textLabel)
    {
        IsRunning = true;

        textLabel.text = string.Empty;

        float t = 0;
        int charIndex = 0;

        while (charIndex < text.Length)
        {
            int lastCharIndex = charIndex;

            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, text.Length);

            for(int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i >= text.Length - 1;
                textLabel.text = text.Substring(0, i + 1);

                if(IsPuntuation(text[i], out float waitTime) && !isLast && !IsPuntuation(text[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }

            textLabel.text = text.Substring(0, charIndex);

            yield return null;
        }

        IsRunning = false;
    }

    bool IsPuntuation(char character, out float waitTime)
    {
        foreach(Puntuation punctuationCategory in punctuations)
        {
            if(punctuationCategory.Punctuations.Contains(character))
            {
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

    readonly struct Puntuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Puntuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
