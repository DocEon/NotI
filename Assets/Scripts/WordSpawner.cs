using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    public GameObject wordPrefab;
    public Transform wordCanvas;
    public Vector3 defaultLocation = new Vector3 (0f, -2f, 0f);
    public WordDisplay SpawnWord(Vector3 location)
    {
        if (location.Equals(defaultLocation)){
            Debug.Log("default position");
        }
        GameObject wordObj = Instantiate(wordPrefab, location, Quaternion.identity,  wordCanvas);
        WordDisplay wordDisplay = wordObj.GetComponent<WordDisplay>();

        return wordDisplay;
    }
}
