using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;

    private AudioManager _audioManager;
    private Pooler _pooler;
    private float _health;
    
    public delegate void Defeat();
    public static event Defeat defeated;    

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        _health = 100;
        _healthSlider.maxValue = _health;
        _healthSlider.value = _health;

        _pooler = Pooler.Instance;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //If touched lava - DIE
        if (other.gameObject.CompareTag("Lava"))
        {
            Killed();
        }
    }

    public void GetDamage(float damage)
    {
        _audioManager.Play("Hurt");

        _health -= damage;
        _healthSlider.value = _health;
        if (_health <= 0)
        {
            Killed();
        }
    }

    private void Killed()
    {
        defeated();

        //Play sound
        _audioManager.Play("PlayerDeath");
        
        //Pooler
        _pooler.GetPooledObject("PlayerExplosion", transform.position, Quaternion.identity);        

        //Some shake
        CameraShake.ShakeOnce = true;
                
        _health = 0;
        _healthSlider.value = _health;
        
        //Turn off player
        gameObject.SetActive(false);
    }
}
