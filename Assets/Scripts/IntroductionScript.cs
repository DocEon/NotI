using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroductionScript : MonoBehaviour
{
    public Dropdown speed;
    public Toggle statistics;
    public GameObject InfoBox;
    private float speedFactor = 1f;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(InfoBox);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Starting game at:" + speed.captionText.text + " speed with statistics " + statistics.isOn);
            if (speed.captionText.text == "Fast")
            {
                speedFactor = 1f;
            }
            if (speed.captionText.text == "Medium")
            {
                speedFactor = .75f;
            }
            if (speed.captionText.text == "Slow")
            {
                speedFactor = .5f;
            }

            InfoBoxScript info = InfoBox.GetComponent<InfoBoxScript>();

            info.setSpeed(speedFactor);
            info.setStatsOn(statistics.isOn);
            SceneManager.LoadScene(1);
        }
    }
}
