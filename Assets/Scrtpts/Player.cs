using UnityEngine;

public class Player : MonoBehaviour
{
    
    private Game _game;
    private Movement _movement;
    
    void Start()
    {
        _movement = GetComponent<Movement>();
        _game = FindFirstObjectByType<Game>();;
        
    }
    

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Point"))
        {
            _game.PlayerCollectedPoint(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            _game.PlayerHit();
        }
        else if (other.CompareTag("Heal"))
        {
            _game.PlayerHealPoint(other.gameObject);
        }
        else if (other.CompareTag("SpeedBoost"))
        {
            _game.PlayerSpeedBoost(other.gameObject, _movement);
        }
        
    }
}