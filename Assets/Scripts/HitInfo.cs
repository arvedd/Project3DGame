using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HitInfo : MonoBehaviour
{
    internal static object action;
    [SerializeField] new Camera camera;
    [SerializeField] bool useCamera = true;
    [SerializeField] Transform rayOrigin;

    public static event Action OnSeeEnemy;
    public static event Action OnExitEnemy;
    public static Damageable currentTarget;


    void Update()
    {
        Ray ray;

        if (useCamera)
        {
            ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        }
        else
        {
            ray = new Ray(rayOrigin.position, rayOrigin.forward);
        }

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            var damageable = hit.collider.GetComponent<Damageable>();
            if (damageable != null)
            {
                currentTarget = damageable;
                OnSeeEnemy?.Invoke();
            }
            else
            {
                currentTarget = null;
                OnExitEnemy?.Invoke();
            }
        }
        else
        {
            currentTarget = null;
            OnExitEnemy?.Invoke();
        }
        
    }


}
