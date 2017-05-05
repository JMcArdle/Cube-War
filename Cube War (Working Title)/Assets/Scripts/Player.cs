using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player  {

    public int playerNum;
    public int maxPoints;
    public int currentPoints;

    public Player(int pN, int mP)
    {
        playerNum = pN;
        maxPoints = mP;
        currentPoints = 0;
    }

    public int num()
    {
        return playerNum;
    }

    public int points
    {
        get { return currentPoints; }
        set { currentPoints = value;
            //if (currentPoints > maxPoints) print("Player using too many points!");
        }
    }


    public int pointsAvailable
    {
        get { return maxPoints; }
        set { maxPoints = value;
           // if (currentPoints > maxPoints) print("Set too low! It's under their current points!");
        }
    }



}
