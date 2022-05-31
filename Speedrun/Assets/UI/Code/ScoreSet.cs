using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//·©Å· Á¤·Ä
public class ScoreSet : MonoBehaviour
{
    private float[] bestScore = new float[10];
    private float[] bestTime = new float[10];
   

    void Scorerecord(float currentScore, float currentTime)
    {
        PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);
        PlayerPrefs.SetFloat("CurrentPlayerTime", currentTime);

        float tmpScore = 0f;
        float tmpTime = 0f;

        for (int i = 0; i < 5; i++)
        {
            bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
            bestTime[i] = PlayerPrefs.GetFloat(i + "BestTime");

            while (bestScore[i] < currentScore)
            {
                tmpScore = bestScore[i];
                tmpTime = bestTime[i];
                bestScore[i] = currentScore;
                bestTime[i] = currentTime;

                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
                PlayerPrefs.SetFloat(i + "BestTime", currentTime);

                currentScore = tmpScore;
                currentTime = tmpTime;
            }
        }
        for (int i = 0; i < 10;i++)
        {
            PlayerPrefs.SetFloat(i + "BestScore", bestScore[i]);
            PlayerPrefs.SetFloat(i + "BestTime", bestTime[i]);
        }


    }
}
