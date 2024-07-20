using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : Singleton<MenuUI>
{
    public void StartButton()
    {
        SceneManager.LoadScene("Battler");
    }
    public void CharactersButton()
    {
        SceneManager.LoadScene("CharacterEditor");
    }
    public void QuitButton()
    {
        Debug.Log("yepp it quittin'");
        Application.Quit();
    }
}
