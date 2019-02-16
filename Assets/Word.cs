using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Word {

    public string word;
    private int typeIndex;
    public float endTime;
    WordDisplay display;

    public Word (string _word, float _endtime, WordDisplay _display, Word ParentWord = null)
    {
        word = _word;
        typeIndex = 0;
        endTime = _endtime;
        display = _display;
        display.SetWord(word);
    }

    public char GetNextLetter()
    {
        return word[typeIndex];
    }

    public void TypeLetter ()
    {
        typeIndex++;
        display.RemoveLetter();
        // remove letter on screen
    }

    public Vector3 GetVector3()
    {
        return display.currentPosition();
    }

    public void ChangeAlign(string newAlign)
    {
        display.text.alignment = TextAnchor.UpperLeft;
    }
    public float GetEndTime()
    {
        return endTime;
    }

    public void ChangeWord(string newWord)
    {
        display.text.text = newWord;
    }

    public void ChangeSize(int newFontSize)
    {
        display.text.fontSize = newFontSize;
    }
    public void MoveTowards(Vector3 location)
    {
        display.MoveTowards(location);
    }

    public bool WordTyped ()
    {
        bool wordTyped = (typeIndex >= word.Length);
        if (wordTyped)
        {
            display.RemoveWord();
            //remove word from screen
        }
        return wordTyped;
    }

    public void RemoveWord()
    {
        display.RemoveWord();
    }
}
