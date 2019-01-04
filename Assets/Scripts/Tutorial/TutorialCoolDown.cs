using UnityEngine;
using UnityEngine.UI;

public class TutorialCoolDown : MonoBehaviour
{

    public Image circle;
    public Text shoots;
    public CannonScript cannon;

    void Update()
    {
        if (cannon != null && GameObject.FindGameObjectWithTag("Menu").GetComponent<TutorialInventoryScript>().cannonBallEquiped != -1)
        {
            circle.fillAmount = 1 - (cannon.timeLeft / cannon.cannon.coolDown);
            shoots.text = "" + cannon.shoots;
            if (circle.fillAmount == 1)
            {
                circle.color = Color.green;
            }
            else
            {
                circle.color = Color.red;
            }
        }
        else
        {
            circle.color = Color.red;
            circle.fillAmount = 1;
            shoots.text = "" + 0;
        }
    }

}