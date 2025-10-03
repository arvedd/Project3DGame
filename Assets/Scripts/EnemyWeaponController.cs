using System.Collections;
using UnityEditor;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    public Damageable target;
    public Gunfire gun;
    public Transform rayOrigin;
    public float visionRange = 100f;
    public bool isShoot = false;
    public bool isLookingPlayer = false;

    void Update()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, visionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                isLookingPlayer = true;
                target = hit.collider.GetComponent<Damageable>();
                if (!isShoot && isLookingPlayer)
                {
                    StartCoroutine(ShootPlayer());
                }
            }
            else
            {
                isLookingPlayer = false;
                isShoot = false;
                target = null;
            }
        }
    }

    IEnumerator ShootPlayer()
    {
        isShoot = true;

        while (isShoot && target != null)
        {
            gun.ShootGun();
            target.TakeDamage(10);
            yield return new WaitForSeconds(1.5f);
        }
        
    }
}
