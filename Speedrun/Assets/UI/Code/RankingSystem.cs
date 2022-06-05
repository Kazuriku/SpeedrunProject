using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingSystem
{
    int COUNT = 10;
    public void SaveRanking(float time)
    {
        Debug.Log(time);
        float[] bestTime = new float[COUNT];
        string[] bestDate = new string[COUNT];

        float curtime = time;
        string curDate = DateTime.Now.ToString("yy'/'MM'/'dd");

        //PlayerPrefs.SetFloat("CurrentTime", curtime);
        //PlayerPrefs.SetString("CurrentDate", curDate);

        float tmpTime = 0f;
        string tmpDate = "";

        for (int i = 0; i < COUNT; i++)
        {
            bestTime[i] = PlayerPrefs.GetFloat(i + "BestTime");
            bestDate[i] = PlayerPrefs.GetString(i + "BestDate");

            if (bestTime[i] < curtime)
                continue;
            else
            while (bestTime[i] > curtime)
            {
                tmpTime = bestTime[i];
                tmpDate = bestDate[i];
                bestTime[i] = curtime;
                bestDate[i] = curDate;

                PlayerPrefs.SetFloat(i + "BestTime", curtime);
                PlayerPrefs.SetString(i.ToString() + "BestDate", curDate);

                curtime = tmpTime;
                curDate = tmpDate;
            }
        }
        for (int i = 0; i < COUNT; i++)
        {
            PlayerPrefs.SetFloat(i + "BestTime", bestTime[i]);
            PlayerPrefs.SetString(i.ToString() + "BestDate", bestDate[i]);
        }
    }
    public int GetRankingIndex(float curTime)
    {
        for (int i = 0; i < COUNT; i++)
        {
            float bestTime = PlayerPrefs.GetFloat(i + "BestTime");

            if (bestTime < curTime)
                continue;
            else 
                return i + 1;
        }
        return -1;
    }
    public void ResetRanking()
    {
        for (int i = 0; i < COUNT; i++)
        {
            PlayerPrefs.SetFloat(i + "BestTime", 99999999);
            PlayerPrefs.SetString(i.ToString() + "BestDate", "");
        }
    }
    public float[] GetAllRankingTime()
    {
        float[] bestTime = new float[COUNT];
        for (int i = 0; i < COUNT; i++)
        {
            bestTime[i]= PlayerPrefs.GetFloat(i + "BestTime");
        }
        return bestTime;
    }
    public string[] GetAllRankingDate()
    {
        string[] bestDate = new string[COUNT];
        for (int i = 0; i < COUNT; i++)
        {
            bestDate[i] = PlayerPrefs.GetString(i + "BestDate");
        }
        return bestDate;
    }
}
