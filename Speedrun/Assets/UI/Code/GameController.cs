using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static GameController instance;
   

    public GameObject hudContainer, gameOverPanel, OptionUI;
    public Text timeCounter, countdownText;
    public bool gamePlaying { get; private set;}
    public int countdownTime;

    private float startTime, elapsedTime;
    TimeSpan timePlaying;



    

    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        gamePlaying = true;
        gamePlaying = false;
        timeCounter.text = "00:00.00";
        StartCoroutine(CountdownToStart());
    }

    private void BeginGame()
    {
        gamePlaying = true;
        startTime = Time.time;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
             OptionUI.gameObject.SetActive(OptionUI.gameObject.activeSelf);
            //�ϴ� ���߿�
        }

        if (gamePlaying)
        {
            elapsedTime = Time.time - startTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr; //r���ӽ����ϸ� 0�ʿ��� ����
        }
    }
    private void Finish()
    {
        //if(�� ������ ����){
        //EndGame();}
    }

    private void EndGame()
    {
        gamePlaying = false;
        Invoke("ShowFinishScreen", 1.25f);
    }
        private void ShowFinishScreen()
        {
            gameOverPanel.SetActive(true);
            hudContainer.SetActive(false);
        string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
        gameOverPanel.transform.Find("record").GetComponent<Text>().text = timePlayingStr;
 }
    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        BeginGame();
        countdownText.text = "Start!"
;

        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
            }




    }

