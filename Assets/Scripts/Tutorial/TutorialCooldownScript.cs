using UnityEngine;
using UnityEngine.UI;
public class TutorialCooldownScript : MonoBehaviour
{

    public GameObject cooldownCircle;
    public GameObject cooldownsRight;
    public GameObject cooldownsLeft;

    private TutorialInventoryScript inv;
    private TutorialBoatController boat;

    private void Start()
    {
        boat = GameObject.FindGameObjectWithTag("Player").GetComponent<TutorialBoatController>();
        inv = GetComponent<TutorialInventoryScript>();
        for (int i = 0; i < inv.cannonsEquipped.Length; i++)
        {
            GameObject r = Instantiate(cooldownCircle, cooldownsRight.transform);
            GameObject l = Instantiate(cooldownCircle, cooldownsLeft.transform);
            l.GetComponent<TutorialCoolDown>().cannon = boat.cannonsLeft[i].GetComponentInChildren<CannonScript>();
            r.GetComponent<TutorialCoolDown>().cannon = boat.cannonsRight[i].GetComponentInChildren<CannonScript>();
        }
    }

    public void EquipCannos(CannonScript left, CannonScript right, int index)
    {
        cooldownsLeft.transform.GetChild(index).gameObject.GetComponent<TutorialCoolDown>().cannon = left;
        cooldownsRight.transform.GetChild(index).gameObject.GetComponent<TutorialCoolDown>().cannon = right;
    }

}
