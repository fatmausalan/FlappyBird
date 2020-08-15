using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamecontrol : MonoBehaviour
{
    public GameObject gokyuzu1;
    public GameObject gokyuzu2;
    Rigidbody2D phy1;
    Rigidbody2D phy2;
    float width;
    public float backgroundSpeed = -1.5f ;
    public GameObject engel;
    GameObject[] engeller;
    public int engelCount;
    float changingTime = 0;
    int count = 0;
    bool game = true;
    void Start()
    {
        phy1 = gokyuzu1.GetComponent<Rigidbody2D>();
        phy2 = gokyuzu2.GetComponent<Rigidbody2D>();

        phy1.velocity = new Vector2(backgroundSpeed, 0);
        phy2.velocity = new Vector2(backgroundSpeed, 0);

        width = gokyuzu1.GetComponent<BoxCollider2D>().size.x;
        engeller = new GameObject[engelCount];
        for(int i = 0; i < engeller.Length; i++)
        {
            Debug.Log(i);
            engeller[i] = Instantiate(engel, new Vector2(-20, -20), Quaternion.identity);
            Rigidbody2D phyEngel = engeller[i].AddComponent<Rigidbody2D>();
            phyEngel.gravityScale = 0;
            phyEngel.velocity = new Vector2(backgroundSpeed, 0);
        }
    }

    void Update()
    {
        if (gokyuzu1.transform.position.x <= -width)
        {
            gokyuzu1.transform.position += new Vector3(width * 2, 0);
        }
        if (gokyuzu2.transform.position.x <= -width)
        {
            gokyuzu2.transform.position += new Vector3(width * 2, 0);
        }

        if (game)
        {
            changingTime += Time.deltaTime;
            if (changingTime > 1f)
            {
                changingTime = 0;
                float y = Random.Range(-0.50f, 1.10f);
                engeller[count].transform.position = new Vector3(7, y);
                count++;
                if (count >= engeller.Length)
                {
                    count = 0;
                }
            }
        }
        else
        {

        }
    }

    public void gameOver()
    {
        game = false;
        for(int i = 0; i<engeller.Length; i++)
        {
            engeller[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            phy1.velocity = Vector2.zero;
            phy2.velocity = Vector2.zero;
        }
    }
}
