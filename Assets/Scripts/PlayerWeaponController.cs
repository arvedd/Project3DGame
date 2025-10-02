using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] GameObject extraCross;
    [SerializeField] GameObject reloadText;
    [SerializeField] AudioSource emptyGun;
    [SerializeField] AudioSource reloadGun;
    [SerializeField] GameObject ammoBox;
    public Gunfire gun;
    public Enemy enemy;
    private GameObject currentAmmoBox;
    public bool isShoot = false;
    public bool canReload = true;
    public bool isLookingAmmo = false;
    public bool isLookingEnemy = false;
    public InputActionReference gunAction;
    public InputActionReference gunReload;

    void Start()
    {
        ammoBox = null;
    }

    void OnEnable()
    {
        gunAction.action.Enable();
        gunReload.action.Enable();
        gunReload.action.performed += OnReload;
        gunAction.action.performed += OnShooting;
        gunAction.action.canceled += OnStopShooting;
        HitInfo.OnSeeEnemy += OnLookEnemy;
        HitInfo.OnExitEnemy += OnNotLookEnemy;
    }

    void OnDisable()
    {
        gunAction.action.Disable();
        gunReload.action.performed -= OnReload;
        gunAction.action.performed -= OnShooting;
        gunAction.action.canceled -= OnStopShooting;
        HitInfo.OnSeeEnemy -= OnLookEnemy;
        HitInfo.OnExitEnemy -= OnNotLookEnemy;
    }

    void OnShooting(InputAction.CallbackContext context)
    {
        if (!isShoot)
        {
            StartCoroutine(PlayerShoot());
        }

        if (Ammo.ammoCount == 0)
        {
            StartCoroutine(EmptyGun());
        }
    }

    void OnStopShooting(InputAction.CallbackContext context)
    {
        isShoot = false;
    }

    void OnReload(InputAction.CallbackContext context)
    {
        if (isLookingAmmo && currentAmmoBox != null)
        {
            StartCoroutine(ReloadBullet());
        }
    }

    void OnLookEnemy()
    {
        isLookingEnemy = true;
    }

    void OnNotLookEnemy()
    {
        isLookingEnemy = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AmmoBox"))
        {
            reloadText.SetActive(true);
            isLookingAmmo = true;
            currentAmmoBox = other.gameObject;
            ammoBox = currentAmmoBox;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AmmoBox"))
        {
            reloadText.SetActive(false);
            isLookingAmmo = false;
            ammoBox = null;
        }
    }

    IEnumerator ReloadBullet()
    {

        reloadGun.Play();
        Ammo.ammoCount += 10;
        yield return new WaitForSeconds(0.3f);

        Destroy(ammoBox);
        currentAmmoBox = null;
        reloadText.SetActive(false);
        isLookingAmmo = false;
        
        
    }

    IEnumerator PlayerShoot()
    {
        isShoot = true;

        while (isShoot && Ammo.ammoCount > 0)
        {
            gun.ShootGun();
            Ammo.ammoCount--;

            if (isLookingEnemy)
            {
                enemy.Damage(2);
            }

            extraCross.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            extraCross.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator EmptyGun()
    {
        emptyGun.Play();
        yield return new WaitForSeconds(0.2f);
    }
}
