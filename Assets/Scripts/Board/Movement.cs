using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool test;
    public Vector3Int destiny;

    SpriteRenderer sr;

    Transform jumper;
    TileLogic actualTile;

    // Start is called before the first frame update
    void Start()
    {
        jumper = transform.Find("Jumper");
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (test)
        {
            test = false;
            StopAllCoroutines();
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        yield return null;
        TileLogic t = Board.instance.tiles[destiny];
        //transform.position = t.worldPos;

        Vector3 startPos = transform.position;
        Vector3 endPos = t.worldPos;
        float totalTime = 1;
        float tempTime = 0;


        if (actualTile == null)
        {
            actualTile = t;
        }
        if (actualTile.floor != t.floor)
        {
            StartCoroutine(Jump(t, totalTime));
        }

        while (transform.position != endPos)
        {
            tempTime += Time.deltaTime;
            float perc = tempTime / totalTime;
            transform.position = Vector3.Lerp(startPos, endPos, perc);
            yield return null;
        }

        sr.sortingOrder = t.contentOrder;
        t.content = this.gameObject;
    }

    IEnumerator Jump(TileLogic t, float totalTime)
    {
        Vector3 halfwayPos;
        Vector3 startpos = halfwayPos = jumper.localPosition;
        halfwayPos.y += 0.5f;
        float tempTime = 0;

        if (actualTile.floor.tilemap.tileAnchor.y < t.floor.tilemap.tileAnchor.y)
        {
            sr.sortingOrder = t.contentOrder;
        }
        while (jumper.localPosition != halfwayPos)
        {
            tempTime += Time.deltaTime;
            float perc = tempTime / (totalTime / 2);
            jumper.localPosition = Vector3.Lerp(startpos, halfwayPos, perc);
            yield return null;
        }
        tempTime = 0;
        while (jumper.localPosition != startpos)
        {
            tempTime += Time.deltaTime;
            float perc = tempTime / (totalTime / 2);
            jumper.localPosition = Vector3.Lerp(halfwayPos, startpos, perc);
            yield return null;
        }
    }
}
