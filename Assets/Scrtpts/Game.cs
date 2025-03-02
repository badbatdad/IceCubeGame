using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float secondsToSpawnWave = 15f;
    [SerializeField] private int enemiesPerWave = 3;
    [SerializeField] private int healsPerWave = 1; 
    [SerializeField] private int speedBoostPerWave = 1; 
    [SerializeField] private GameObject _healPrefab;
    [SerializeField] private GameObject _speedBoostPrefab;
    [SerializeField] private int _startnumberOfEnemies = 1;
    [SerializeField] private GameObject _pointPrefab;
    [SerializeField] private int _numberOfPointsToVictory = 5;
    [SerializeField] private TextMeshProUGUI _countHeartsText;
    [SerializeField] private int _startHearts = 3;
    [SerializeField] private TextMeshProUGUI _countPointsText;
    [SerializeField] private float speedBoostValue;
    [SerializeField] private float maxSpeed = 10;
    [SerializeField] private float chanceOfSpawnSpeedBoost = 0.5f;
    [SerializeField] private Button restartGameButton;
    
    private Movement _movement;
    
    private int _countHearts;
    private int _countPoints = 0;
    private float _timer;
    private float _nextSpawnTime;
    private string[] tagsToDelete = { "Player", "Point", "Enemy", "SpeedBoost", "Heal" }; 
    private int _numberOfEnemies;
    private int enemiesPerWaves;
    

    

    private void Start()
    {   
        _numberOfEnemies = _startnumberOfEnemies;
        _movement = GetComponent<Movement>();
        _countHearts = _startHearts;
        _timer = 0;
        _nextSpawnTime = _timer + secondsToSpawnWave;
        enemiesPerWaves= enemiesPerWave;
        SpawnObjects();
        UpdateHeartsInfo();
        UpdatePointsInfo();

        restartGameButton.gameObject.SetActive(false);
        restartGameButton.interactable = false;
        restartGameButton.onClick.AddListener(RestartGame);
        
    }

    private void Update()
    {
        TimerTick();
        if (_timer >= _nextSpawnTime)
        {
            SpawnWave();
            _nextSpawnTime = _timer + secondsToSpawnWave;
        }
        
    }

    private void TimerTick()
    {
        _timer += Time.deltaTime;
    }

    private void Lose()
    {
        _countPointsText.text = "ВЫ ПРОИГРАЛИ!";
        _nextSpawnTime = _timer + secondsToSpawnWave;
        RestartGame();
        
    }

    private void Victory()
    {
        _countPointsText.text = "Поздравляем вы собрали все монетки!";
        Time.timeScale = 0;
        restartGameButton.gameObject.SetActive(true);
        restartGameButton.interactable = true;
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < _numberOfEnemies; i++)
        {
            SpawnObject(_enemyPrefab, new Vector3(Random.Range(-3, 3), 0.16f, Random.Range(-3, 3)));
        }

        SpawnObject(_playerPrefab, new Vector3(0, 0.2f, 0));
    }

    private GameObject SpawnObject(GameObject prefab, Vector3 pos)
    {
        return Instantiate(prefab, pos, Quaternion.identity);
    }

    public void PlayerCollectedPoint(GameObject point)
    {
        Destroy(point);
        _countPoints++;
        UpdatePointsInfo();
        if (_countPoints >= _numberOfPointsToVictory) 
        {
             Victory();
        }
    }
    public void PlayerHealPoint(GameObject point)
    {
        Destroy(point);
        _countHearts++;
        UpdateHeartsInfo();
    }

    public void PlayerHit()
    {
        _countHearts--;
        UpdateHeartsInfo();
        if (_countHearts <= 0)
        {
            Lose();
        }
        
    }

    private void UpdatePointsInfo()
    {
        _countPointsText.text = "Монетки: " + _countPoints;
    }

    private void UpdateHeartsInfo()
    {
        string hearts = new string('♥', _countHearts);
        _countHeartsText.text = hearts;
    }
    
    private void SpawnWave()
    {
        for (int i = 0; i < healsPerWave; i++)
        {
            SpawnObject(_healPrefab, new Vector3(Random.Range(-3.2f, 3.2f), 0.16f, Random.Range(-3.2f, 3.2f)));
        }

        enemiesPerWaves--;
        for (int i = 0; i < enemiesPerWaves; i++)
        {
            SpawnObject(_enemyPrefab, new Vector3(Random.Range(-3.2f, 3.2f), 0.16f, Random.Range(-3.2f, 3.2f)));
        }
        for (int i = 0; i < speedBoostPerWave; i++)
        {   
            if (RandomBool(chanceOfSpawnSpeedBoost))
            {
            SpawnObject(_speedBoostPrefab, new Vector3(Random.Range(-3.2f, 3.2f), 0.16f, Random.Range(-3.2f, 3.2f)));
            }
        }
    }

    bool RandomBool(float probability)
    {
        return Random.value < probability;
    }

    public void PlayerSpeedBoost(GameObject speedBoost, Movement _movement)
    {
        Destroy(speedBoost);
        if (_movement.movementSpeed < maxSpeed)
        _movement.movementSpeed += speedBoostValue;
        else
        {
            _countPoints+=2;
            UpdatePointsInfo();
            if (_countPoints >= _numberOfPointsToVictory) 
            {
                 Victory();
            }
        }
    }
    private void RestartGame()
    {
        _countPoints = 0;
        Time.timeScale = 1;
        ClearLevel();
        Start();
    }
    private void ClearLevel()
    {
        foreach (string tag in tagsToDelete)
        {
            GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objectsToDelete)
                Destroy(obj);
        }
        
               
    
    }
    
}