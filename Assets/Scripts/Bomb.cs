using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explode;
    public GameObject shootPoint;

    private bool blowUp = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoolDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (!blowUp)
        {
            //inCoolDown = true;
            GameObject go = Instantiate(explode);
            go.transform.position = shootPoint.transform.position;
            go.transform.rotation = shootPoint.transform.rotation;
            //BulletMove b = go.GetComponent<BulletMove>();
            //b.speed = 10;
            //StartCoroutine(CoolDown());
            Destroy(this.gameObject);
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(1);
        blowUp = false;
    }
}
