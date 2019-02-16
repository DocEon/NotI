using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBoxScript : MonoBehaviour
{
    public bool statsOn;
    public float speedFactor;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void setSpeed(float speed)
    {
        speedFactor = speed;
    }

    public void setStatsOn(bool isOn)
    {
        statsOn = isOn;
    }

    public float getSpeed()
    {
        return speedFactor;
    }
    
    public bool getStatsOn()
    {
        return statsOn;
    }
}
