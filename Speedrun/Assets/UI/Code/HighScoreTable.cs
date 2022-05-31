using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    private void Awake()
    {

        {
            entryContainer = transform.Find("highscoreEntryContainer");
            entryTemplate = entryContainer.Find("highscoreEntryTemplate");

            entryTemplate.gameObject.SetActive(false);

            //float templateHeight = 30f;
                for (int i = 0; i < 10; i++)
            {
                Transform entryTransform = Instantiate(entryTemplate, entryContainer);
                RectTransform entryTectTransform = entryTransform.GetComponent<RectTransform>();
                entryTransform.gameObject.SetActive(true);

                int rank = i + 1;

                string rankString;
                switch (rank)
                {
                    default:
                        rankString = rank + "TH"; break;

                    case 1: rankString = "1ST";
                            break;
                    case 2: rankString = "2ND";
                        break;
                    case 3: rankString = "3RD";
                        break;
                }
            }
        }
    }
}

