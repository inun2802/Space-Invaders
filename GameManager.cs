using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject youWinUI;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    private Player player;
    private Invaders invaders;
    private MysteryShip mysteryShip;
    private Bunker[] bunkers;


    private int score;
    private int lives;

    public int Score => score;
    public int Lives => lives;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        invaders = FindObjectOfType<Invaders>();
        mysteryShip = FindObjectOfType<MysteryShip>();
        bunkers = FindObjectsOfType<Bunker>();

        NewGame();
    }

    
    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return)) {
            ReturnToMainMenu(); 
        }
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0); 
    }

    private void LevelSelect()
    {
        SceneManager.LoadScene(6);
    }

    private void NewGame()
    {
        gameOverUI.SetActive(false);
        youWinUI.SetActive(false);

        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        Respawn();
    }

    private void Respawn()
    {
        Vector3 position = player.transform.position;
        position.x = 0f;
        player.transform.position = position;
        player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true);
        invaders.gameObject.SetActive(false);
        FindObjectOfType<AudioManager>().StopBackgroundMusic();
        FindObjectOfType<AudioManager>().Play("OverTheme");
    }

    private void YouWin()
    {
        youWinUI.SetActive(true);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 4 || currentSceneIndex == 5)
        {
            Invoke(nameof(LevelSelect), 5f);
        }
        else
        {
            Invoke(nameof(LoadNextScene), 5f);
        }
        FindObjectOfType<AudioManager>().StopBackgroundMusic();
        FindObjectOfType<AudioManager>().Play("WinTheme");
    }


    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(4, '0');
    }

    private void SetLives(int lives)
    {
        this.lives = Mathf.Max(lives, 0);
        livesText.text = this.lives.ToString();
    }

    public void OnPlayerKilled(Player player)
    {
        SetLives(lives - 1);

        player.gameObject.SetActive(false);

        if (lives > 0) {
            Invoke(nameof(NewRound), 1f);
        } else {
            GameOver();
        }
    }

    public void OnInvaderKilled(Invader invader)
    {
        invader.gameObject.SetActive(false);

        SetScore(score + invader.score);

        FindObjectOfType<AudioManager>().Play("Explosion");

        if (invaders.GetAliveCount() == 0) {
            YouWin();
        }
    }

    public void OnMysteryShipKilled(MysteryShip mysteryShip)
    {
        SceneManager.LoadScene(5);
    }

    public void OnBoundaryReached()
    {
        if (invaders.gameObject.activeSelf)
        {
            invaders.gameObject.SetActive(false);

            OnPlayerKilled(player);
        }
    }
}

