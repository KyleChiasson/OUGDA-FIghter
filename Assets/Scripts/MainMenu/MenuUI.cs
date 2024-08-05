using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : Singleton<MenuUI>
{
    public void StartButton()
    {
        Loader.Load(SceneManager.LoadSceneAsync("Battler"));
    }
    public void CharactersButton()
    {
        Loader.Load(SceneManager.LoadSceneAsync("CharacterEditor"));
    }
    public void QuitButton()
    {
        Debug.Log("yepp it quittin'");
        Application.Quit();
    }
}
