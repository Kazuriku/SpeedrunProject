using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameOption : MonoBehaviour
{
    public IngameOption optionMenu;

    private void Update()
    {
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                optionMenu.gameObject.SetActive(optionMenu.gameObject.activeSelf);
            }
        }
    }

}
