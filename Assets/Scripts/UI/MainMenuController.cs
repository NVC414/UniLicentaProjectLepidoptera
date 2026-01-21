using UnityEngine;

public sealed class MainMenuController : MonoBehaviour
{
    public void LoadTutorial() => SceneLoader.Load("Tutorial");
    public void LoadLevel1() => SceneLoader.Load("Level1_FieldDefenseBackdrop");
    public void LoadLevel2() => SceneLoader.Load("Level2_BanditCamp");
    public void LoadLevel3() => SceneLoader.Load("Level3_Sorcerers");
    public void QuitGame() => SceneLoader.Quit();
}