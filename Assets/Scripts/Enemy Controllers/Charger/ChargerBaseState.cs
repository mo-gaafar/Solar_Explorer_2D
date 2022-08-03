using UnityEngine;

public abstract class ChargerBaseState {
    public abstract void EnterState (ChargerEnemyController chargerStateManager);
    public abstract void UpdateState (ChargerEnemyController chargerStateManager);
    public void OnCollisionEnter2D (ChargerEnemyController controller, Collision2D other) { }
}