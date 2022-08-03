using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Gun;
using UnityEngine;
using UnityEngine.Events;

public class GunController : MonoBehaviour {

    private MainController mainController;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private Gun currentGun;

    //TODO: make private later
    static private int _inventorySize = 3;
    public List<Gun> _gunInventory = new List<Gun> (_inventorySize);
    public List<GunCollectible> _CollectibleInventory = new List<GunCollectible> (_inventorySize);
    // private int _currentGunIndex = 0;

    private ShootingState _currentShootingState = 0;
    [SerializeField] private float switchingTime = 0.5f;
    private float lastPickupTime = 0f;
    const float PICKUP_INTERVAL = 2f;
    GunCollectible currentCollectible;

    int GunIndex=0;
    enum ShootingState {
        IdleReady,
        Shooting,
        Reloading,
        Switching,
        PickupWeapon
    }

    enum ShootingMode {
        Automatic,
        SemiAutomatic,
        SingleShot,
        Burst
    }

    void Awake () {
        // gunInventory = new Gun[2];
        // gunInventory[0] = new LaserGun();
        // gunInventory[1] = new ProjectileGun();
        // currentGun = 0;
    }

    // Start is called before the first frame update
    void Start () {
        mainController = GetComponent<MainController> ();

        mainController.onShoot.AddListener (() => {
            if (_currentShootingState == ShootingState.IdleReady || _currentShootingState == ShootingState.Shooting) {
                currentGun?.Shoot (firingPoint);
                _currentShootingState = ShootingState.Shooting;
            }
        });

        mainController.onStopShoot.AddListener (() => currentGun?.StopShoot ());
        mainController.onReload.AddListener (() => currentGun?.Reload ());
        //TODO: add scroll up and down functionality to switch guns in mainController
        mainController.onSwitchWeapon.AddListener (() => SwitchWeapon (-1));
    }

    // Update is called once per frame
    void Update () {

    }




    public void PickupGunCollectible(GunCollectible Collected) {
        // check if inventory fits
        Gun gun = Collected.TheGun;
        if (_gunInventory.Count == _inventorySize ) {

            if (GunIndex == 2)
            {
                GunIndex = 0;
            }
            else
            {
                GunIndex++;

            }
            //GunIndex = (GunIndex == 3) ? 0 : GunIndex++;

            Debug.Log("Gun index is " + GunIndex);

            //Remove first weapon, insert the one we took
            GunCollectible ThrownCollectible = _CollectibleInventory[GunIndex];
            Gun GunToDestroy = _gunInventory[GunIndex];
            Destroy(GunToDestroy);

            //throw it 
            ThrownCollectible.Spawn(transform.position);


            _gunInventory[GunIndex] = gun;
            _CollectibleInventory[GunIndex] = Collected;
            SwitchWeapon(GunIndex);
            return;
        }

        // Add weapon to last slot in inventory array
        //TODO: add copy constructor here

        //Debug.Log("OUT Gun index is " + GunIndex);
        _CollectibleInventory.Add(Collected);
        _gunInventory.Add (gun);

        GunIndex = _gunInventory.Count - 1;
        // Switch to weapon?
        SwitchWeapon (GunIndex);
    }

    async void SwitchWeapon (int gunIndex) {
        // TODO: add loading bar for switching weapon

        // check if inventory only contains one gun
        Debug.Log("Switch ONNNNN");
        if (_currentShootingState == ShootingState.Switching) {
            Debug.Log ("Already switching");
            return;
        }
        if (_gunInventory.Count == 0) {
            Debug.Log ("Inventory Empty");
            return;
        }
        if (_gunInventory.Count == 1) {
            currentGun = _gunInventory[0];
            Debug.Log ("No weapon to switch to");
        }
        //update state
        _currentShootingState = ShootingState.Switching;

        await Task.Delay ((int) switchingTime);

        if (gunIndex == -1) {
            //TODO: rethink of this cornercase implementation
            //Switches to the next weapon
            gunIndex = (_gunInventory.IndexOf (currentGun) + 1) % _gunInventory.Count;
        }

        //check second gun exists then do the switching
        if (_gunInventory != null && _gunInventory.Count > 1 && _gunInventory[gunIndex] != null) {

            currentGun = _gunInventory[gunIndex];
        }

        //TODO: check for reload ??

        //update state
        _currentShootingState = ShootingState.IdleReady;

    }

}