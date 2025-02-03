using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public LevelLoader levelLoader;

    public void PlayGame() {
        levelLoader.LoadScene(1);
    }

    public void LoadMain() {
        levelLoader.LoadScene(0);
    }

    public void EasyLevel() {
        levelLoader.LoadScene(1);
    }

    public void IntermediateLevel() {
        levelLoader.LoadScene(2);
    }
    
    public void HardLevel() {
        levelLoader.LoadScene(3);
    }

    public void BossLevel() {
        levelLoader.LoadScene(4);
    }

    public void Settings() {
        levelLoader.LoadScene(7);
    }

    public void QuitGame() {
        Application.Quit();
    }
}