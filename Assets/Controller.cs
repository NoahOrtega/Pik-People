using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject captain;
    public GameObject sprout;
    public GameObject[] pikPeople;
    public GameObject message;

    float PIK_SPAWN_HEIGHT = 4.16f;
    int CAM_DIST_FROM_CENTER = 18;

    int MAX_PIK_PEOPLE = 15;
    int curPikPeople = 0;
    GameObject[] sproutsOnScreen = { null, null, null, null };

    System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        captain = Instantiate(captain.gameObject, new Vector3(0, 3f, 0), Quaternion.identity);
        plantSeed();
        plantSeed();
        plantSeed();
    }

    float newRandXPos()
    {
        return (float)(rand.NextDouble() * (CAM_DIST_FROM_CENTER * 2) - CAM_DIST_FROM_CENTER);
    }

    void plantSeed()
    {
        bool scanning = true;
        for (int i = 0; i < sproutsOnScreen.Length && scanning; i++)
        {
            if (sproutsOnScreen[i] == null)
            {
                curPikPeople++;
                sproutsOnScreen[i] = Instantiate(sprout.gameObject, new Vector3(newRandXPos(), .5f, 0), sprout.transform.rotation);
                scanning = false;
                Debug.Log("Planted at " + sproutsOnScreen[i].transform.position.x);
            }
        }
        if (scanning)
        {
            Debug.Log("Max sprouts planted");
        }
    }

    public void tryPullPikPerson()
    {
        int closestPikIndex = -1;
        float minDist = 1000f;

        for (int i = 0; i < sproutsOnScreen.Length; i++)
        {
            if (sproutsOnScreen[i] != null)
            {
                float curDist = Mathf.Abs(sproutsOnScreen[i].transform.position.x - captain.transform.position.x);
                if (curDist < minDist)
                {
                    minDist = curDist;
                    closestPikIndex = i;
                }
            }
        }

        if (minDist < 1.5f)
        {
            pullPikPerson(closestPikIndex);
        }
    }

    void pullPikPerson(int index)
    {
        Instantiate(pikPeople[rand.Next(0, 3)].gameObject,
            new Vector3(sproutsOnScreen[index].transform.position.x, PIK_SPAWN_HEIGHT, 0), Quaternion.identity);
        Destroy(sproutsOnScreen[index]);
        sproutsOnScreen[index] = null;
        plantNewSeeds();
    }

    void plantNewSeeds()
    {
        if (curPikPeople < MAX_PIK_PEOPLE)
        {
            plantSeed();
        }
        else
        {
            //only for celebration
            Instantiate(message.gameObject, new Vector3(0, 15f, 0), Quaternion.identity);
        }
    }

    public void quitGame()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}

