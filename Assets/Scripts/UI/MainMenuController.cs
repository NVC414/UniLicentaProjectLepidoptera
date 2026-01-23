using UnityEngine;

public sealed class MainMenuController : MonoBehaviour
{
    public void LoadTutorial() => SceneLoader.Load("Level1_FieldDefenseBackdrop");
    public void LoadLevel1() => SceneLoader.Load("Level1_ForestAttack");
    public void LoadLevel2() => SceneLoader.Load("Level2_VillageAttack");
    public void LoadLevel3() => SceneLoader.Load("Level3_Bossfight");
    public void QuitGame() => SceneLoader.Quit();
}