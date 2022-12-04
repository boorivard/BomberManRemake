using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shootPoint;

    private bool inCoolDown = false;
    private int bombCount = 1;

    //private AudioSource PewPew;

    // Start is called before the first frame update
    void Start()
    {
        //PewPew = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !inCoolDown && bombCount > 0)
        {
            /*if (!PewPew.isPlaying)
                PewPew.PlayOneShot(PewPew.clip, 1.0f);*/
            inCoolDown = true;
            GameObject go = Instantiate(bullet);
            go.transform.position = shootPoint.transform.position;
            go.transform.rotation = shootPoint.transform.rotation;
            bombCount--;
            Debug.Log("Bomb Count is: " + bombCount);
            //BulletMove b = go.GetComponent<BulletMove>();
            //b.speed = 10;
            StartCoroutine(CoolDown());
            StartCoroutine(AddCount());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.tag == "More")
        {
            Debug.Log("Increase bomb count by 1");
            bombCount++;
            Destroy(gameObject);
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(1);
        inCoolDown = false;
    }

    IEnumerator AddCount()
    {
        yield return new WaitForSeconds(2);
        bombCount++;
    }
}
