using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _health = 100; //TODO: make private after debugging

    public UnityEvent<GameObject> onDeath;
    public UnityEvent<float> onHit;
    public UnityEvent<float> onHeal;
    public UnityEvent<float> onHealthChanged;
    public UnityEvent<float> onMaxHealthChanged;

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

    private void OnEnable () {
        _health = MaxHealth;
        onHit.AddListener ((float damage) => {
            TakeDamage (damage);
            Debug.Log ("Damaging");
        });
        onHeal.AddListener ((float heal) => {
            Heal (heal);
            Debug.Log ("Healing");
        });

    }
    public void TakeDamage (float damage) {
        health -= damage;
        // if (health <= 0) {
        //     Die ();
        // }

    }

    public void Heal (float percent) {
        health = Mathf.Min (health + percent * MaxHealth, MaxHealth);
    }

}