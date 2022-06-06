using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static GameController instance;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuCanvas;

    public int hitPoints;
    public GameObject hudContainer, gameOverPanel, OptionUI;
    public Text timeCounter, countdownText;
    public bool gamePlaying { get; private set;}
    public int countdownTime;
    public GameObject finishObj;

    private float startTime, elapsedTime;
    TimeSpan timePlaying;


    public GameObject MainMenuUI;
    public GameObject InGameUI;

    public GameObject RankUI;
    public GameObject RankLayoutUI;
    public GameObject RankPrefab;
    ///////////////
    public Text FinishTimeTxt;



    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        gamePlaying = false;
        timeCounter.text = "00:00.00";
        s = new RankingSystem();
    }
    public void ActiveMainMenu()
    {
        MainMenuUI.SetActive(true);
        OptionUI.SetActive(false);
        RankUI.SetActive(false);
    }
    public void ActiveRankUI()
    {
    
        MainMenuUI.SetActive(false);
        RankUI.SetActive(true);

        rankSetting();
    }
    private void rankSetting()
    {
        RankingSystem rank = new RankingSystem();
        float[] timeArr = rank.GetAllRankingTime();
        string[] dateArr = rank.GetAllRankingDate();
        for (int i =0; i < 10; i++)
        {
            GameObject obj = Instantiate(RankPrefab);
            obj.transform.SetParent(RankLayoutUI.transform);
            Text[] textArr = obj.GetComponentsInChildren<Text>();
            textArr[0].text = (i+1).ToString();
            textArr[1].text = dateArr[i];
            textArr[2].text = timeArr[i] > 99999 ? "" : timeArr[i].ToString();
        }
    }
    private void BeginGame()
    {
        gamePlaying = true;
        startTime = Time.time;
    }
    Coroutine countCor;
    public void StartGame()
    {
        MainMenuUI.SetActive(false);
        InGameUI.SetActive(true);

        StartCoroutine(CountdownToStart());

        timeCounter.text = "00:00.00";
        OptionUI.SetActive(false);
       
            
       
        if (countCor == null)
            countCor = StartCoroutine(CountdownToStart());
        else
        {
            StopCoroutine(countCor);
            countCor = StartCoroutine(CountdownToStart());
        }

    }
    public float testtime;
    RankingSystem s;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            s.SaveRanking(testtime);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(s.GetRankingIndex(testtime));
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            s.ResetRanking();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            //s.DebugRanking();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           Time.timeScale = 0;
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
                //일단 나중에
        }

        if (gamePlaying)
        {
            elapsedTime = Time.time - startTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            
            string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr; //r게임시작하면 0초에서 시작
        }
    }

    public void Resume()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause(){
        
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        
    }
    public bool GetPlaying()
    {
        return gamePlaying;
    }


    public void EndGame()
    {
        gamePlaying = false;
        FinishTimeTxt.text = timePlaying.ToString("mm':'ss'.'ff");
        Invoke("ShowFinishRanking", 3.5f);
      
        s.SaveRanking((float)timePlaying.TotalSeconds);
        

    }
        private void ShowFinishScreen()
        {
            gameOverPanel.SetActive(true);
            hudContainer.SetActive(false);
        string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
        gameOverPanel.transform.Find("record").GetComponent<Text>().text = timePlayingStr;
 }
    private void ShowFinishRanking()
    {
        RankUI.SetActive(true);
        RankLayoutUI.SetActive(true);
        RankPrefab.SetActive(true);
       
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

