using UnityEngine;

public class Ammo : MonoBehaviour
{
    public static int ammoCount = 10;
    [SerializeField] GameObject ammoDisplay;

    void Update()
    {
        ammoDisplay.GetComponent<TMPro.TMP_Text>().text = "" + ammoCount;
    }
}
