using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private BaseState _currentState;

    [SerializeField] Player _player;
    public Player player { get; private set; }

    [SerializeField] private float _timeToSurvive;
    public  float _timePassed {  get; private set; }

    [SerializeField] TextMeshProUGUI countdown;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;

    public GameObject winPanel { get; private set; }
    public GameObject losePanel { get; private set; }

    private void OnEnable()
    {
        player = _player;
        winPanel = _winPanel;
        losePanel = _losePanel;
    }

    private void OnDisable()
    {
        
    }

    private void Awake()
    {
 if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(new PlayerState(this));
    }

    private void Update()
    {
        _currentState.Update();
        UpdateTimer();
    }

    public void ChangeState(BaseState state)
    {
        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    private void UpdateTimer()
    {
       
       
        _timePassed += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(_timePassed);
        countdown.text = string.Format("{0:00} : {1:00}", time.Minutes, time.Seconds);

        if (_timeToSurvive == 0) return;

        if (_timePassed >= _timeToSurvive)
        {
            WinGame();
        }
    }

    public void GoBackMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void WinGame()
    {
        ChangeState(new WinState(this));
    }
}
