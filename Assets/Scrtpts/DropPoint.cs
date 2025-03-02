using UnityEngine;

public class DropPoint : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private float timeInterval = 3f;
    private float _timer; 

    void Start()
    {
        _timer = timeInterval;
    }
    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            Instantiate(pointPrefab, transform.position, Quaternion.Euler(90f, 0f, 0f));
            _timer = timeInterval+Random.Range(-1.9f, 2f); 
        }
    }
}
