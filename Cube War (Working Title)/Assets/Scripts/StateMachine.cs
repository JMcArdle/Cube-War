using System.Collections;
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
    battle,
    healer,
	gameOver
}

public class StateMachine : MonoBehaviour {

    public static GameState state;
    public static Turn turnState;
    public static Turn holdTurn;
    public static GamePhase gamePhase;
    public static bool cubePlace = false;
	public static bool cubeLaunch = false;
    public static bool p1Setup = false;
    public static bool p2Setup = false;
    public static bool p1King = false;
    public static bool p2King = false;
    public static bool p1Paralyze = false;
    public static bool p2Paralyze = false;
    public static bool p1Bomb = false;
    public static bool p2Bomb = false;
    public static bool p1Healer = false;
    public static bool p2Healer = false;
    public static int p1Peasant = 0;
    public static int p2Peasant = 0;
    public static int peasantLimit = 5;




    public static void clearStateMachine()
    {
        cubePlace = false;
		cubeLaunch = false;
        p1Setup = false;
        p2Setup = false;
        p1King = false;
        p2King = false;
        p1Paralyze = false;
        p2Paralyze = false;
        p1Bomb = false;
        p2Bomb = false;
        p1Healer = false;
        p2Healer = false;
        p1Peasant = 0;
        p2Peasant = 0;
        unPause();
    }



    public static void initiateTurns()
    {
        if (state == GameState.active)
        {
            turnState = Turn.player1;
            GameDriver.updateTurnInterface();
        }
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
				if (getPhase () == GamePhase.setup && p2Setup == true) {
					GameDriver.gameDriver.p1Side.SetActive (true);
					GameDriver.gameDriver.p2Side.SetActive (false);
				}
				if (getPhase () == GamePhase.battle) {
					GameDriver.clearFlicks ();
					if (flickableCubesAvailable (2) == 0) {
						if (flickableCubesAvailable (1) == 0) {
							GameDriver.resolveStalemate ();
						} else {
							passTurn ();
						}
					}
				}
                    if (getPhase() == GamePhase.setup && p2Setup == false) turnState = Turn.player1;
                    GameDriver.updateTurnInterface();
                    GameDriver.updateSetupInterface();
                    if (getPhase() == GamePhase.setup && p1Setup == false && p2Setup == false) GameDriver.endSetup();
                        break;
                case Turn.player2:
                    turnState = Turn.player1;
					if (getPhase () == GamePhase.setup && p1Setup == true) {
						GameDriver.gameDriver.p1Side.SetActive (false);
						GameDriver.gameDriver.p2Side.SetActive (true);
					}
					if (getPhase () == GamePhase.battle) {
						GameDriver.clearFlicks ();
						if (flickableCubesAvailable (1) == 0) {
							if (flickableCubesAvailable (2) == 0) {
							GameDriver.resolveStalemate ();
							} else {
								passTurn ();
							}
						}
					}
                    if (getPhase() == GamePhase.setup && p1Setup == false) turnState = Turn.player2;
                    GameDriver.updateTurnInterface();
                    GameDriver.updateSetupInterface();
                    if (getPhase() == GamePhase.setup && p1Setup == false && p2Setup == false) GameDriver.endSetup();
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
            p1Setup = true;
            p2Setup = true;
			GameDriver.gameDriver.p2Side.SetActive (true);
        }
    }

    public static void battlePhase()
    {
        if (state == GameState.active)
        {
            gamePhase = GamePhase.battle;
			GameDriver.gameDriver.p1Side.SetActive (false);
			GameDriver.gameDriver.p2Side.SetActive (false);
			if ((flickableCubesAvailable (1) == 0) && (flickableCubesAvailable (2) == 0)) {
				GameDriver.resolveStalemate ();
			}
        }
    }

    public static void healerPhase()
    {
        gamePhase = GamePhase.healer;
    }

    public static void endHealerPhase()
    {
        gamePhase = GamePhase.battle;
        passTurn();
    }

	public static void gameOverPhase()
	{
		if (state == GameState.active) 
		{
			gamePhase = GamePhase.gameOver;
		}
	}

	public static int flickableCubesAvailable(int playerNum){
		int cubes = 0;
		foreach (GameObject cb in GameDriver.gameDriver.cubesInPlay) {
			if (!cb.GetComponent<Cube> ().stunned && cb.GetComponent<UnitClass> ().owningPlayer == playerNum) {
				cubes += 1;
			}
		}
		return cubes;
	}

    public static void pause()
    {
        if (turnState != Turn.pause)
        {
            holdTurn = turnState;
            turnState = Turn.pause;
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0;
			GameDriver.PauseCubes();
            GameDriver.showMenuInterface();
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
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
			GameDriver.UnPauseCubes ();
            GameDriver.hideMenuInterface();
        }
        else
        {
            print("Game isn't paused, you can't unpause it!");
        }
    }

    public void unPauseFromButton()
    {
        StateMachine.unPause();
    }

    public static GamePhase getPhase()
    {
        return gamePhase;
    }

    public static GameState getGameState()
    {
        return state;
    }


    public static void endP1Setup()
    {
        p1Setup = false;
    }

    public static void endP2Setup()
    {
        p2Setup = false;
    }


    //For setting unit limits//
    public static void p1KingPlaced()
    {
        p1King = true;
    }

    public static void p2KingPlaced()
    {
        p2King = true;
    }

    public static void p1ParalyzePlaced()
    {
        p1Paralyze = true;
    }

    public static void p2ParalyzePlaced()
    {
        p2Paralyze = true;
    }

    public static void p1BombPlaced()
    {
        p1Bomb = true;
    }

    public static void p2BombPlaced()
    {
        p2Bomb = true;
    }

    public static void p1HealerPlaced()
    {
        p1Healer = true;
    }

    public static void p2HealerPlaced()
    {
        p2Healer = true;
    }

    public static void p1PeasantPlaced()
    {
        p1Peasant += 1;
    }

    public static void p2PeasantPlaced()
    {
        p2Peasant += 1;
    }

    //And for removing them

    public static void p1KingRemoved()
    {
        p1King = false;
    }

    public static void p2KingRemoved()
    {
        p2King = false;
    }

    public static void p1ParalyzeRemoved()
    {
        p1Paralyze = false;
    }

    public static void p2ParalyzeRemoved()
    {
        p2Paralyze = false;
    }

    public static void p1BombRemoved()
    {
        p1Bomb = false;
    }

    public static void p2BombRemoved()
    {
        p2Bomb = false;
    }

    public static void p1HealerRemoved()
    {
        p1Healer = false;
    }

    public static void p2HealerRemoved()
    {
        p2Healer = false;
    }

    public static void p1PeasantRemoved()
    {
        p1Peasant -= 1;
    }

    public static void p2PeasantRemoved()
    {
        p2Peasant -= 1;
    }

    //End of the unit limit code



    public static void endSetup()
    {
        GameDriver.startBattle();
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

	public static bool isCubeLaunched
	{
		get
		{
			return cubeLaunch;
		}
		set 
		{
			cubeLaunch = value;
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
