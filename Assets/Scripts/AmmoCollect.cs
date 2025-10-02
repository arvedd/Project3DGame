using UnityEngine;

public class AmmoCollect : MonoBehaviour
{
    [SerializeField] GameObject reloadText;
    public bool isLookingAmmo = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AmmoBox"))
        {
            reloadText.SetActive(true);
            isLookingAmmo = true;
        }
        else
        {
            reloadText.SetActive(false);
            isLookingAmmo = false;
        }
    }
}
