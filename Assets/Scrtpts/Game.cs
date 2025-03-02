using TMPro;
using UnityEngine;

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
    [SerializeField] private int _numberOfEnemies = 1;
    [SerializeField] private GameObject _pointPrefab;
    [SerializeField] private int _numberOfPointsToVictory = 5;
    [SerializeField] private TextMeshProUGUI _countHeartsText;
    [SerializeField] private int _startHearts = 3;
    [SerializeField] private TextMeshProUGUI _countPointsText;
    [SerializeField]  private float speedBoostValue;
    [SerializeField]  private float maxSpeed = 10;
    [SerializeField]  private float chanceOfSpawnSpeedBoost = 0.5f;
    
    private Movement _movement;
    private int _countHearts;
    private int _countPoints = 0;
    private bool _gameIsEnded;
    private float _timer = 0;
    private float _nextSpawnTime; 

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _countHearts = _startHearts;
        _nextSpawnTime = _timer + secondsToSpawnWave;
        SpawnObjects();
        UpdateHeartsInfo();
        UpdatePointsInfo();
    }

    private void Update()
    {
        if (!_gameIsEnded)
        {
            TimerTick();
        }
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
        _gameIsEnded = true;
        _countPointsText.text = "ВЫ ПРОИГРАЛИ!";
        return;
    }

    private void Victory()
    {
        _gameIsEnded = true;
        _countPointsText.text = "Поздравляем вы собрали все монетки!";
    }

    private void SpawnObjects()
    {
        SpawnObject(_playerPrefab, new Vector3(0, 0.2f, 0));

        for (int i = 0; i < _numberOfEnemies; i++)
        {
            float posX = Random.Range(-4, 4);
            float posZ = Random.Range(-4, 4);
            SpawnObject(_enemyPrefab, new Vector3(posX, 0.16f, posZ));
        }

    }

    private GameObject SpawnObject(GameObject prefab, Vector3 pos)
    {
        return Instantiate(prefab, pos, Quaternion.identity);
    }

    public void PlayerCollectedPoint(GameObject point)
    {
          if (!_gameIsEnded)
        {
            Destroy(point);
            _countPoints++;
            UpdatePointsInfo();
            if (_countPoints >= _numberOfPointsToVictory) 
            {
                 Victory();
            }
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
       if (!_gameIsEnded)
        {
            _countHearts--;
            UpdateHeartsInfo();
            if (_countHearts <= 0)
            {
                Lose();
            }
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
            SpawnObject(_healPrefab, new Vector3(Random.Range(-4, 4), 0.16f, Random.Range(-4, 4)));
            SpawnObject(_speedBoostPrefab, new Vector3(Random.Range(-4, 4), 0.16f, Random.Range(-4, 4)));
        }
        enemiesPerWave--;
        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnObject(_enemyPrefab, new Vector3(Random.Range(-4, 4), 0.16f, Random.Range(-4, 4)));
        }
        for (int i = 0; i < speedBoostPerWave; i++)
        {   
            if (RandomBool(chanceOfSpawnSpeedBoost))
            {
            SpawnObject(_speedBoostPrefab, new Vector3(Random.Range(-4, 4), 0.16f, Random.Range(-4, 4)));
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
    
}