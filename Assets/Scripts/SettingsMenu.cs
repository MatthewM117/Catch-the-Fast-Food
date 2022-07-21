using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{

    GameObject buttonSelected;
    GameObject buttonSelected2;

    private void Start()
    {
        buttonSelected = GameObject.Find("ButtonSelected");
        buttonSelected2 = GameObject.Find("ButtonSelected2");

        //currentActiveButton();
    }

    public void SelectThemeOne()
    {
        PlayerPrefs.SetInt("Themes", 1);
        PlayerPrefs.Save();
    }

    public void SelectThemeTwo()
    {
        PlayerPrefs.SetInt("Themes", 2);
        PlayerPrefs.Save();
    }

    public void currentActiveButton()
    {
        if (PlayerPrefs.GetInt("Themes") == 1)
        {
            buttonSelected.SetActive(true);
            buttonSelected2.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Themes") == 2)
        {
            buttonSelected.SetActive(false);
            buttonSelected2.SetActive(true);
        }
    }
}
