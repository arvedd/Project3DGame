using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gunfire : MonoBehaviour
{
    [SerializeField] AudioSource gunfire;
    [SerializeField] GameObject gun;
    private Animator gunAnimation;
    public bool isShoot = false;
    public bool canReload = true;


    void Awake()
    {
        gunAnimation = gun.GetComponent<Animator>();
    }

    public void ShootGun()
    {
        StartCoroutine(FiringGun());

    }

    public IEnumerator FiringGun()
    { 
        gunfire.Play();

        gunAnimation.Play("GunFireM4");
        yield return new WaitForSeconds(0.1f);
        
        gunAnimation.Play("Idle");
        yield return new WaitForSeconds(0.1f);

    }
}
