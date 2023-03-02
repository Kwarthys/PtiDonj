using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingManager : MonoBehaviour
{
    private FootIKAnimator[] feet;

    private int maxIndex = -1;
    private int currentIndex = 0;

    private bool lastAllOnGround = false;

    // Start is called before the first frame update
    void Start()
    {
        feet = gameObject.GetComponentsInChildren<FootIKAnimator>();

        for (int i = 0; i < feet.Length; i++)
        {
            if(feet[i].animIndex > maxIndex)
            {
                maxIndex = feet[i].animIndex;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool allOnGround = true;
        for (int i = 0; i < feet.Length && allOnGround; i++)
        {
            if(!feet[i].onGround())
            {
                allOnGround = false;
            }
        }

        if(allOnGround && !lastAllOnGround)
        {
            Debug.Log("Halving " + currentIndex);
            for (int i = 0; i < feet.Length; i++)
            {
                if(feet[i].animIndex == currentIndex)
                {
                    feet[i].halfNextStep();
                }
            }

            currentIndex = (currentIndex + 1) % (maxIndex+1);
        }

        lastAllOnGround = allOnGround;
    }
}
