using UnityEngine.SceneManagement;

public static class SceneController 
{
    public const string MainMenuSceneTitle = "MainMenu";
    public const string CitySceneTitle = "City";
    public const string Match3Title = "Match3";

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneTitle);
    }

    public static void LoadSceneCity()
    {
        SceneManager.LoadScene(CitySceneTitle);
    }

    public static void LoadMatch3()
    {
        SceneManager.LoadScene(Match3Title);
    }

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }
}
