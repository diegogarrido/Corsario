using System.Collections;
using UnityEngine;

public class FloatingChest : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
        if (GetComponent<Treasure>().failed && GetComponent<Treasure>().active)
        {
            StartCoroutine(SinkAndDestroy());
        }else if (GetComponent<Treasure>().open && GetComponent<Treasure>().active)
        {
            StartCoroutine(WaitAndSink());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !GetComponent<Treasure>().open && !GetComponent<Treasure>().failed)
        {
            GetComponent<Treasure>().active = true;
            GetComponent<Treasure>().ChoosePuzzle();
        }
    }

    public IEnumerator WaitAndSink()
    {
        yield return new WaitForSeconds(3);
        Component.Destroy(gameObject.GetComponent<MeshFilter>());
        Component.Destroy(gameObject.GetComponent<BoatPhysics>());
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public IEnumerator SinkAndDestroy()
    {
        Component.Destroy(gameObject.GetComponent<MeshFilter>());
        Component.Destroy(gameObject.GetComponent<BoatPhysics>());
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
