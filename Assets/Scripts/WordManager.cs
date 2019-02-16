using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WordManager : MonoBehaviour
{
    public List<Word> words;
    public List<Word> settings;
    // Make the main list:

    // Use the test w/ timestamps
    public TextAsset ATTTScript;
   
    //public static string cleanText = System.IO.File.ReadAllText("Assets/Text/AlmostToTheTickScript.txt", System.Text.Encoding.ASCII);
    //public static string[] listOfWords = cleanText.Split('\n');
    public static string cleanText;
    public static string[] listOfWords;
    public VideoController videoController;
    public WordSpawner wordSpawner;

    private static float offset = 0f;
    private bool hasActiveWord;
    private Word activeWord;
    public static int position = 0;
    public string currentWord;
    public float currentTime;
    // if you're using an offset:


    // variables for settings
    public static Vector3 settingsLocation = new Vector3(-4.0f, -1f);
    public static bool timerShowing = false;
    public static bool wpmShowing;
    public static bool accuracyShowing;
    public static bool pauseTimerShowing = false;
    public static bool progressShowing = false;
    public static string settingsString;
    public static float speedFactor = 1f;
    private float PlayUntil = 0.00f;
    private Vector3 defaultLocation = new Vector3(0f, 2.5f, 0f);
    private Vector3 secondLocation = new Vector3(0f, 2.0f, 0f);

    private void Start()
    {
        cleanText = ATTTScript.ToString();
        listOfWords = cleanText.Split('\n');
        currentWord = listOfWords[position].Split('_')[0];
        currentTime = float.Parse(listOfWords[position].Split('_')[1]) + offset;
        GameObject InfoBox = GameObject.Find("InfoBox");
        if (GameObject.Find("InfoBox") != null)
        {
            InfoBoxScript info = InfoBox.GetComponent<InfoBoxScript>();
            speedFactor = info.getSpeed();
            Debug.Log("Playing at " + speedFactor + "speed");
            Debug.Log("StatsOn = " + info.getStatsOn());
            if (info.getStatsOn())
            {
                timerShowing = true;
                pauseTimerShowing = true;
                progressShowing = true;
            }
        }
        settingsString = makeSettingsString();
        Word settingsWord = new Word("settings", 9999f, wordSpawner.SpawnWord(settingsLocation));
        settingsWord.ChangeSize(14);
        settingsWord.ChangeAlign("Upper Left");
        settingsWord.ChangeWord(settingsString);
        settings.Add(settingsWord);
        AddWord();
        AddWord();
        videoController.SetPlaybackSpeed(speedFactor);
    }

    private void FixedUpdate()
    {
        //settings stuff
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            timerShowing = !timerShowing;
            pauseTimerShowing = !pauseTimerShowing;
            progressShowing = !progressShowing;
        }
        settingsString = makeSettingsString();
        settings[0].ChangeWord(settingsString);

        if (!videoController.IsPrepared)
        {
            // Debug.Log("Not ready!");
            return;
        }
        if (videoController.currentTime < PlayUntil)
        {
            // Debug.Log(video.time + " < " + PlayUntil);
            videoController.PlayVideo();
        }
        else
        {
            // Debug.Log(video.time + " > " + PlayUntil);
            videoController.PauseVideo();
        }
        if (words[0].GetVector3() != defaultLocation)
        {
            words[0].MoveTowards(defaultLocation);
        }
    }
    public void AddWord()
    {
        Vector3 newWordLocation = defaultLocation;
        Word lastWord = null;
        if (words.Count == 0)
        {
            newWordLocation = defaultLocation;
        }
        else
        {
            newWordLocation = secondLocation;
        }

        if (position > listOfWords.Length - 1)
        {
            Debug.Log("Done");
            SceneManager.LoadScene(2);
        }
        currentWord = listOfWords[position].Split('_')[0];
        currentTime = float.Parse(listOfWords[position].Split('_')[1]) + offset;
        Word word = new Word(currentWord, currentTime, wordSpawner.SpawnWord(newWordLocation), lastWord);
        Debug.Log(currentWord);
        words.Add(word);
        position = position + 1;       
    }

    public void TypeLetter(char letter)
    {
        if (hasActiveWord)
        {
            if (activeWord.GetNextLetter() == letter)
            {
                activeWord.TypeLetter();
            }
        }
        else
        {
            if (words[0].GetNextLetter() == letter)
            {
                activeWord = words[0];
                hasActiveWord = true;
                words[0].TypeLetter();
            }
        }

        if (hasActiveWord && activeWord.WordTyped())
        {
            hasActiveWord = false;
            PlayUntil = activeWord.GetEndTime();
            words.Remove(activeWord);
            AddWord();
        }
        // ` = skip mode.
        if (hasActiveWord && letter == '`')
        {
            hasActiveWord = false;
            PlayUntil = activeWord.GetEndTime();
            words.Remove(activeWord);
            activeWord.RemoveWord();
            AddWord();
        }
        else if(!hasActiveWord && letter == '`')
        {
            activeWord = words[0];
            hasActiveWord = true;
            words[0].TypeLetter();
        }
    }

    private string makeSettingsString()
    {
        settingsString = "";
        if (timerShowing)
        {
            float videoTime = Mathf.Round((float)videoController.currentTime * 100.0f) / 100f;
            settingsString += "Video Time: " + videoTime + "\n";
        }
        if(pauseTimerShowing)
        {
            float timePaused = Mathf.Round(videoController.getTimePaused*100f)/100f;
            settingsString += "Time spent paused: " + timePaused + "\n";
        }
        if (progressShowing)
        {
            settingsString += "At position " + (position-2) + " of " + listOfWords.Length + "\n";
        }
        return settingsString;
    }
}
