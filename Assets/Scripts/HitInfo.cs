using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HitInfo : MonoBehaviour
{
    internal static object action;
    [SerializeField] new Camera camera;

    public static event Action OnSeeEnemy;
    public static event Action OnExitEnemy;


    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit))
        {

            if (hit.collider.CompareTag("Enemy"))
            {
                Transform objectHit = hit.transform;
                OnSeeEnemy?.Invoke();
            }
            else
            {
                OnExitEnemy?.Invoke();
            }
            
               
        }
    }


}
