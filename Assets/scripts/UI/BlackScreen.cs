using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
    public float toFadeTime;
    public float blackTime;
    float cd;
    bool darken, black, lighten;

    public RoomSelector roomSelector;

    Image image;

    public void startFade()
    {
        if (!darken)
        {
            darken = true;
            cd = toFadeTime;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        cd--;
        if (darken)
        {
            //image.color = new Color(1f, 1f, 1f, 1f - (cd / toFadeTime));
            image.fillAmount = 1f - (cd / toFadeTime);
            if (cd <= 0)
            {
                //image.color = new Color(1f, 1f, 1f, 1f);
                image.fillAmount = 1f;
                roomSelector.changeRoomInBlack();
                cd = blackTime;
                black = true;
                darken = false;
            }
        }
        else if (black && cd <= 0)
        {
            cd = toFadeTime;
            black = false;
            lighten = true;
        }
        else if (lighten)
        {
           // image.color = new Color(1f, 1f, 1f, (cd / toFadeTime));
            image.fillAmount = (cd / toFadeTime);
            if (cd <= 0)
            {
                // image.color = new Color(1f, 1f, 1f, 0f);
                image.fillAmount = 0f;
                roomSelector.changeRoomFinished();
                cd = blackTime;
                lighten = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
