using UnityEngine;
using UnityEngine.UI;
public class BarraCirculo : MonoBehaviour
{

    public GameObject cooldownCircle;
    public GameObject cooldownsRight;
    public GameObject cooldownsLeft;

    private InventoryScript inv;
    private BoatController boat;

    private void Start()
    {
        boat = GameObject.FindGameObjectWithTag("Player").GetComponent<BoatController>();
        inv = GetComponent<InventoryScript>();
        for (int i = 0; i < inv.cannonsEquipped.Length; i++)
        {
            GameObject r = Instantiate(cooldownCircle, cooldownsRight.transform);
            GameObject l = Instantiate(cooldownCircle, cooldownsLeft.transform);
            l.GetComponent<CoolDownScript>().cannon = boat.cannonsLeft[i].GetComponentInChildren<CannonScript>();
            r.GetComponent<CoolDownScript>().cannon = boat.cannonsRight[i].GetComponentInChildren<CannonScript>();
        }
    }

    public void EquipCannos(CannonScript left, CannonScript right,int index)
    {
        cooldownsLeft.transform.GetChild(index).gameObject.GetComponent<CoolDownScript>().cannon = left;
        cooldownsRight.transform.GetChild(index).gameObject.GetComponent<CoolDownScript>().cannon = right;
    }

}
