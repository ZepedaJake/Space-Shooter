using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fortressBossWall : MonoBehaviour {

    Vector3 retractedPosition;
    Vector3 extendedPosition;
    public GameObject center;
    public float moveAwayIn;
    public float stayFor = 10;
    public float moveAwayLerp;
    public float moveToLerp;
    //nvm i know now


    void Start()
    {
        retractedPosition = transform.localPosition;
        extendedPosition = retractedPosition + (transform.forward * -1.5f);
        moveAwayIn = Random.Range(10, 15);
    }

    void Update()
    {
        transform.LookAt(center.transform);
        moveAwayIn -= Time.deltaTime * Time.timeScale;
        
        if(moveAwayIn <= 0)
        {
            if(stayFor > 0)
            {
                moveAwayLerp += Time.deltaTime * Time.timeScale * 2;
                transform.localPosition = Vector3.Lerp(retractedPosition, extendedPosition, moveAwayLerp);
                stayFor -= Time.deltaTime * Time.timeScale;
                moveToLerp = 0;
            }
            else
            {
                moveAwayIn = Random.Range(10, 15);
                moveAwayLerp = 0;
                stayFor = 10;
            }         
        }
        else
        {
            moveToLerp += Time.deltaTime * Time.timeScale * 2;
            transform.localPosition = Vector3.Lerp(extendedPosition, retractedPosition, moveToLerp);
        }
       
    }
}
