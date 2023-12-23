using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class MenuController : MonoBehaviour
{
    [Header("Select Level to Load")]
    public LevelKeys LevelKeyOption;

    [SerializeField] private GameObject NoSavedGame = null;
    private string levelToLoad;
    private string NewGame;

    public void NewGameDialogYes() 
    {
        SceneManager.LoadScene(GetLevelString());
    }

    public void LoadGameDialogYes()
    {
        
        if(PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            NoSavedGame.SetActive(true);
        }                
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public string GetLevelString()
    {        
        switch (LevelKeyOption)
        {
            case LevelKeys.MainMenu:
                return "MainMenu";
            case LevelKeys.Level_1:
                return "Level_1";
            case LevelKeys.Garage_1 :
                return "Garage_1";
        }
        
        return "MainMenu";
    }

    public enum LevelKeys
    {
        MainMenu,
        Level_1,
        Garage_1
    }
}

