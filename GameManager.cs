using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private GameObject _playerPrefab;

    public Text scoreText;
    private int _score;
    private int newScore = 0;

    public static GameManager instance = null;
    private bool _playerActive = false;
    private bool _gameOver = false;
    private bool _gameStarted = false;
    private bool _playerRespawn = false;

    public bool PlayerActive
    {
        get { return _playerActive; }
    }
    public bool GameOver
    {
        get { return _gameOver; }
    }
    public bool GameStart
    {
        get { return _gameStarted; }
    }
    public bool PlayerRespawn{
        
		get { return _playerRespawn; }
    }


	private void Awake()
	{
        if(instance == null){
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Assert.IsNotNull(_mainMenu);
        Assert.IsNotNull(_gameOverMenu);
	}

	void Start () {
        
        scoreText.text = "" + _score;
	}
	
	// Update is called once per frame
	void Update () {
        if(_gameOver){
            _score = newScore;
        }
	}

    public void PlayerCollided(){
        _gameOver = true;
        _playerActive = false;
        StartCoroutine(GameOverScreen());
    }

    public void PlayerStartedGame(){
        _playerActive = true;
        _gameOver = false;
        _gameOverMenu.SetActive(false);
        StartCoroutine(Score(0));
    }

    public void EnterGame(){
        _mainMenu.SetActive(false);
        _playerActive = true;
        _gameStarted = true;
		_gameOver = false;

        PlayerStartedGame();
        Instantiate(_playerPrefab);

        _score = 0;
        scoreText.text = "" + _score;
        newScore = 0;
    }

    IEnumerator GameOverScreen(){
        yield return new WaitForSeconds(3.0f);
        _gameOverMenu.SetActive(true);
        _playerRespawn = false;
        Destroy(_playerPrefab);
    }

    IEnumerator Score(int currentScore){
        while(!_gameOver){
			yield return new WaitForSeconds(0.75f);
			_score += 1;
			newScore = _score;
			scoreText.text = "" + _score;   
        }
        StopCoroutine(Score(newScore));
    }
}
