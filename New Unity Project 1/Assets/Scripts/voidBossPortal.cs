using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voidBossPortal : MonoBehaviour {
    public bool fullSize = false;
    public bool changeSize = true;
    public bool ready = false;

    Vector3 minSize = new Vector3(.5f, .5f, .5f);
    Vector3 maxSize = new Vector3(10, .5f, 10);

    float growTimer;
    float readyTimer = 3;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //keep portal open for short time
        if(ready)
        {
            readyTimer -= Time.timeScale * Time.deltaTime;
            //if its big, shrink it
            if(readyTimer <= 0 && fullSize)
            {
                changeSize = true;
                ready = false;
            }
            //if its little get rid of it
            else if(readyTimer <= 0 && !fullSize)
            {
                Destroy(gameObject);
            }
        }

       
        
        //if its not full size and needs to grow.
        if (!fullSize && changeSize)
        {
            growTimer += Time.deltaTime * Time.timeScale;
            transform.localScale = Vector3.Slerp(minSize, maxSize, growTimer );
            if(transform.localScale == maxSize)
            {
                changeSize = false;
                ready = true;
                fullSize = true;
                growTimer = 0;
            }
        }
        //shrink
        else if(fullSize&& changeSize)
        {
            growTimer += Time.deltaTime * Time.timeScale;
            transform.localScale = Vector3.Slerp(maxSize, minSize, growTimer );
            if (transform.localScale == minSize)
            {
                changeSize = false;
                ready = true;
                fullSize = false;
                growTimer = 0;
            }
        }

    }
}
