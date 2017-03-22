﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Turn
{
    player1,
    player2,
    idle,
    pause
}

public enum GameState
{
    active,
    inactive
}

public enum GamePhase
{
    setup,
    battle
}

public class StateMachine : MonoBehaviour {

    public static GameState state;
    public static Turn turnState;
    public static Turn holdTurn;
    public static GamePhase gamePhase;
    public static bool cubePlace;

    public static void initiateTurns()
    {
        if (state == GameState.active) turnState = Turn.player1;
    }

    public static void passTurn()
    {
        if (state == GameState.active)
        {
            switch (turnState)
            {
                case Turn.idle:
                case Turn.player1:
                    turnState = Turn.player2;
                    break;
                case Turn.player2:
                    turnState = Turn.player1;
                    break;
                case Turn.pause:
                    print("Game paused, can't pass turn now!");
                    break;
            }
        }
    }

    public static int currentTurn()
    {
        if (turnState == Turn.player1) return 1;
        else if (turnState == Turn.player2) return 2;
        else if (turnState == Turn.pause && holdTurn == Turn.player1) return 1;
        else if (turnState == Turn.pause && holdTurn == Turn.player2) return 2;
        else
        {
            print("Asked for current turn, but it broke!");
            return 0;
        }
    }

    public static void setupPhase()
    {
        if (state == GameState.active)
        {
            gamePhase = GamePhase.setup;
        }
    }

    public static void battlePhase()
    {
        if (state == GameState.active)
        {
            gamePhase = GamePhase.battle;
        }
    }

    public static void pause()
    {
        if (turnState != Turn.pause)
        {
            holdTurn = turnState;
            turnState = Turn.pause;
        }
        else
        {
            print("Can't pause, game is already paused!");
        }
    }

    public static void unPause()
    {
        if (turnState == Turn.pause)
        {
            turnState = holdTurn;
            holdTurn = Turn.idle;
        }
        else
        {
            print("Game isn't paused, you can't unpause it!");
        }
    }

    public static bool isPlacingCube
    {
        get
        {
            return cubePlace;
        }
        set
        {
            cubePlace = value;
        }
    }


    public static void activate()
    {
        state = GameState.active;
    }

    public static void deactivate()
    {
        state = GameState.inactive;
    }

}
