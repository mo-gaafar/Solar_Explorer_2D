using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _health = 100; //TODO: make private after debugging

    CameraShake CameraShake;

    public UnityEvent<GameObject> onDeath;
    public UnityEvent<float> onHit;
    public UnityEvent<float> onHeal;
    public UnityEvent<float> onHealthChanged;
    public UnityEvent<float> onMaxHealthChanged;

    LevelManager Levelmanager;
    public float health {
        get {
            return _health;
        }
        set {
            _health = value;
            if (_health <= 0) {
                onDeath.Invoke (gameObject);
            }
            if (_health > MaxHealth) {
                _health = MaxHealth;
            }
            onHealthChanged.Invoke (_health);
            onMaxHealthChanged.Invoke (_maxHealth);
        }
    }

    public float MaxHealth {
        get {
            return _maxHealth;
        }
        set {
            _maxHealth = value;
            if (_maxHealth <= 0) {
                _maxHealth = 1;
            }
            if (health > _maxHealth) {
                health = _maxHealth;
            }
            if (health < _maxHealth) {
                health += _maxHealth - health;
            }
            onHealthChanged.Invoke (_health);
            onMaxHealthChanged.Invoke (_maxHealth);
        }
    }
    private void Awake () {
        CameraShake = GameObject.FindGameObjectWithTag ("PlayerCamera").GetComponent<CameraShake> ();
        Levelmanager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
    }

    private void Start () {
        onHealthChanged.Invoke (_health);
        onMaxHealthChanged.Invoke (_maxHealth);
    }

    private void OnEnable () {
        // _health = MaxHealth;
        onHit.AddListener ((float damage) => {
            TakeDamage (damage);
            // CameraShake.Shake ();
        });
        onHeal.AddListener ((float heal) => {
            Heal (heal);
            Debug.Log ("Healing");
        });
        onHeal.AddListener((float heal) => {
            AddMaxHealth(heal);
            Debug.Log("MaxHealing");
        });
        onDeath.AddListener ((GameObject argu) => {
            if (argu.GetComponent<PlayerController> ()) {
                // Levelmanager.GameOver ();
                return;
            }
            if (argu.GetComponent<MainController> ()) {
                Levelmanager.DecreaseLivingEnemies ();
            }
            Destroy (gameObject);
        });

    }
    public void TakeDamage (float damage) {
        health -= damage;
    }

    public bool Heal (float fraction) {
        if (health == MaxHealth) {
            return false;
        }
        health = Mathf.Min (health + fraction * MaxHealth, MaxHealth);
        return true;
    }

    public void AddMaxHealth (float AddedHealth)
    {
        _maxHealth += AddedHealth;
        _health += AddedHealth;
        onMaxHealthChanged.Invoke(_maxHealth);
        onHealthChanged.Invoke(_health);
    }

}