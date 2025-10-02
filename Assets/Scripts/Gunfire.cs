using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gunfire : MonoBehaviour
{
    [SerializeField] AudioSource gunfire;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject extraCross;

    public bool isShoot = false;
    public InputActionReference attackAction;

    // public Ammo ammo;

    private void OnEnable()
    {
        attackAction.action.Enable();
        attackAction.action.performed += OnShooting;
        attackAction.action.canceled += OnStopShooting;
    }

    private void OnDisable()
    {
        attackAction.action.Disable();
        attackAction.action.performed -= OnShooting;
        attackAction.action.canceled -= OnStopShooting;
    }

    void OnShooting(InputAction.CallbackContext context)
    {
        if (!isShoot && Ammo.ammoCount > 0)
        {
            StartCoroutine(FiringGun());
            Ammo.ammoCount -= 1;
        }
        
    }

    void OnStopShooting(InputAction.CallbackContext context)
    {
        isShoot = false;
    }

    IEnumerator FiringGun()
    {
        isShoot = true;

        while (isShoot)
        {
            gunfire.Play();
            extraCross.SetActive(true);

            gun.GetComponent<Animator>().Play("GunFireM4");
            yield return new WaitForSeconds(0.1f);

            extraCross.SetActive(false);
            gun.GetComponent<Animator>().Play("Idle");
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
