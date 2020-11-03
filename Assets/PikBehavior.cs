using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikBehavior : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite[] growthSprites;
    int growthStage = 0;

    float birthTime = 0f;
    float timeToGrow = 0f;

    double moveStartXPos = 0;

    double horizontalSpeed = 0;
    double howFarToMove = 0;

    bool moving = false;
    bool moveRight = true;

    System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        birthTime = Time.time;
        timeToGrow = rand.Next(27) + 3;
        spriteRenderer = transform.Find("Plant").gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(newMove());
        float heightMod = (float)rand.NextDouble() * 2 - 1;
        transform.position = new Vector3(transform.position.x, transform.position.y + heightMod, transform.position.z + heightMod);
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (howFarToMove < Mathf.Abs((float)moveStartXPos - transform.position.x))
            {
                moving = false;
                StartCoroutine(newMove());
            }

            Vector3 toMove = (Vector3.right * (float)horizontalSpeed * Time.deltaTime) + new Vector3(.002f, 0, 0);
            if (moveRight)
            {
                transform.position += toMove;
            }
            else
            {
                transform.position -= toMove;
            }
        }
    }

    IEnumerator newMove()
    {
        moveStartXPos = transform.position.x;

        //plot movement
        double moveRightChance = rand.NextDouble();
        if (moveStartXPos < 0) { moveRightChance += .1; }
        else { moveRightChance -= .1; }
        if (moveStartXPos < -16) { moveRightChance += .2; }
        else if (moveStartXPos > 16) { moveRightChance -= .2; }
        moveRight = (moveRightChance > .5) ? true : false;

        howFarToMove = rand.NextDouble() * 7.0;
        horizontalSpeed = rand.NextDouble() * howFarToMove;

        //decide if pik should grow
        if ((growthStage < 2) && timeToGrow < (Time.time - birthTime))
        {
            birthTime = Time.time;

            growthStage++;
            spriteRenderer.sprite = growthSprites[growthStage];
        }

        //decide how long it should wait
        double timeToWaitWeighted = rand.NextDouble();
        int timeToWait = 0;

        if (timeToWaitWeighted > .93)
        {
            timeToWait = 8;
        }
        else if (timeToWaitWeighted > .70)
        {
            timeToWait = 2;
        }
        float now = Time.time;
        yield return new WaitForSeconds(timeToWait);
        moving = true;
    }
}

