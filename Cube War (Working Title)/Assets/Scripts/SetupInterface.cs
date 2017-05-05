using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupInterface : MonoBehaviour {
    public string targetClass = "King"; // This is what you will set to change what class you get from the button
    public UnitClass unitClass;
    public GameObject cubePrefab;
    public GameObject newUnit;
    public GameDriver driver;
    public ClassLookup classInfo;

    public void Start()
    {
        classInfo = ClassLookup.getClassLookup();
        driver = GameDriver.getGameDriverRef();
        textureButtons(1);
    }



    //Not currently in use.
    /*public void instantiateNewUnit()
    {
        if (StateMachine.turnState != Turn.pause)
        {
			classInfo.Lookup (targetClass);
			print (classInfo.cost);
            if (!StateMachine.isPlacingCube && driver.getPlayerPointsRemaining() > classInfo.cost)
            {
				newUnit = Instantiate(cubePrefab, new Vector3(0,2,0), Quaternion.identity) as GameObject;
				newUnit.GetComponent<Cube> ().SetToPlacing();
				newUnit.GetComponent<UnitClass>().unitSetup(classInfo.Lookup(targetClass, newUnit.GetComponent<UnitClass>()));
                driver.placingCube(newUnit);
            }
        }
    }*/

    public void instantiateNewUnit(string target)      //An overload in case the interface calls it this way
    {
        if (StateMachine.turnState != Turn.pause)
        {
            classInfo.Lookup(target);
            if (StateMachine.gamePhase == GamePhase.setup && !StateMachine.isPlacingCube && driver.getPlayerPointsRemaining() >= classInfo.cost)
            {
                if (target == "King")
                {
                    if ((StateMachine.currentTurn() == 1 && StateMachine.p1King == true) || (StateMachine.currentTurn() == 2 && StateMachine.p2King == true))
                    {
                        return;
                    }
                }
                if (target == "Paralyze")
                {
                    if ((StateMachine.currentTurn() == 1 && StateMachine.p1Paralyze == true) || (StateMachine.currentTurn() == 2 && StateMachine.p2Paralyze == true))
                    {
                        return;
                    }
                }
                if (target == "Bomb")
                {
                    if ((StateMachine.currentTurn() == 1 && StateMachine.p1Bomb == true) || (StateMachine.currentTurn() == 2 && StateMachine.p2Bomb == true))
                    {
                        return;
                    }
                }
                if (target == "Healer")
                {
                    if ((StateMachine.currentTurn() == 1 && StateMachine.p1Healer == true) || (StateMachine.currentTurn() == 2 && StateMachine.p2Healer == true))
                    {
                        return;
                    }
                }
                if (target == "Peasant")
                {
                    if ((StateMachine.currentTurn() == 1 && StateMachine.p1Peasant >= StateMachine.peasantLimit) || (StateMachine.currentTurn() == 2 && StateMachine.p2Peasant >= StateMachine.peasantLimit))
                    {
                        return;
                    }
                }
                newUnit = Instantiate(cubePrefab) as GameObject;
                newUnit.GetComponent<Cube>().SetToPlacing();
                newUnit.GetComponent<UnitClass>().unitSetup(classInfo.Lookup(target, newUnit.GetComponent<UnitClass>()));
                driver.placingCube(newUnit);
            }
            else if (StateMachine.gamePhase == GamePhase.healer && !StateMachine.isPlacingCube && classInfo.cost + driver.healerPoints <= driver.healMax && GameDriver.checkDeadCubes(StateMachine.currentTurn(), target))
            {
                newUnit = Instantiate(cubePrefab) as GameObject;
                newUnit.GetComponent<Cube>().SetToPlacing();
                newUnit.GetComponent<UnitClass>().unitSetup(classInfo.Lookup(target, newUnit.GetComponent<UnitClass>()));
                driver.placingCube(newUnit);
            }
        }
    }

    public void endPlayerSetup()
    {
        if (StateMachine.turnState != Turn.pause)
        {
            if (StateMachine.isPlacingCube == false)
            {
                if (StateMachine.currentTurn() == 1 && StateMachine.p1King == true)
                {
                    StateMachine.endP1Setup();
                    StateMachine.passTurn();
                }
                else if (StateMachine.currentTurn() == 2 && StateMachine.p2King == true)
                {
                    StateMachine.endP2Setup();
                    StateMachine.passTurn();
                }
            }
        }
    }


    public void textureButtons(int i)
    {
        foreach (Transform t in transform)
        {
            switch (t.name)
            {
                case "King":
                    classInfo.Lookup("King");
                    if (StateMachine.gamePhase == GamePhase.setup && StateMachine.currentTurn() == 1) TextureManager.activeButton(t.gameObject, !StateMachine.p1King);
                    else if (StateMachine.gamePhase == GamePhase.setup && StateMachine.currentTurn() == 2) TextureManager.activeButton(t.gameObject, !StateMachine.p2King);
                    else if (StateMachine.gamePhase == GamePhase.healer)
                    {
                        if (GameDriver.checkDeadCubes(StateMachine.currentTurn(), className.King)) TextureManager.activeButton(t.gameObject, true);
                        else TextureManager.activeButton(t.gameObject, false);
                    }
                    break;
                case "Brawler":
                    classInfo.Lookup("Brawler");
                    if (StateMachine.gamePhase == GamePhase.healer)
                    {
                        if (GameDriver.checkDeadCubes(StateMachine.currentTurn(), className.Brawler)) TextureManager.activeButton(t.gameObject, true);
                        else TextureManager.activeButton(t.gameObject, false);
                    }
                    break;
                case "Sentinel":
                    classInfo.Lookup("Sentinel");
                    if (StateMachine.gamePhase == GamePhase.healer)
                    {
                        if (GameDriver.checkDeadCubes(StateMachine.currentTurn(), className.Sentinel)) TextureManager.activeButton(t.gameObject, true);
                        else TextureManager.activeButton(t.gameObject, false);
                    }
                    break;
                case "Shadow":
                    classInfo.Lookup("Shadow");
                    if (StateMachine.gamePhase == GamePhase.healer)
                    {
                        if (GameDriver.checkDeadCubes(StateMachine.currentTurn(), className.Shadow)) TextureManager.activeButton(t.gameObject, true);
                        else TextureManager.activeButton(t.gameObject, false);
                    }
                    break;
                case "Grunt":
                    classInfo.Lookup("Grunt");
                    if (StateMachine.gamePhase == GamePhase.healer)
                    {
                        if (GameDriver.checkDeadCubes(StateMachine.currentTurn(), className.Grunt)) TextureManager.activeButton(t.gameObject, true);
                        else TextureManager.activeButton(t.gameObject, false);
                    }
                    break;
                case "Peasant":
                    classInfo.Lookup("Peasant");
                    if (StateMachine.gamePhase == GamePhase.setup && StateMachine.currentTurn() == 1) TextureManager.activeButton(t.gameObject, !(StateMachine.p1Peasant == StateMachine.peasantLimit));
                    else if (StateMachine.gamePhase == GamePhase.setup && StateMachine.currentTurn() == 2) TextureManager.activeButton(t.gameObject, !(StateMachine.p2Peasant == StateMachine.peasantLimit));
                    else if (StateMachine.gamePhase == GamePhase.healer)
                    {
                        if (GameDriver.checkDeadCubes(StateMachine.currentTurn(), className.Peasant)) TextureManager.activeButton(t.gameObject, true);
                        else TextureManager.activeButton(t.gameObject, false);
                    }
                    break;
                case "Healer":
                    classInfo.Lookup("Healer");
                    if (StateMachine.gamePhase == GamePhase.setup && StateMachine.currentTurn() == 1) TextureManager.activeButton(t.gameObject, !StateMachine.p1Healer);
                    else if (StateMachine.gamePhase == GamePhase.setup && StateMachine.currentTurn() == 2) TextureManager.activeButton(t.gameObject, !StateMachine.p2Healer);
                    else if (StateMachine.gamePhase == GamePhase.healer)
                    {
                        if (GameDriver.checkDeadCubes(StateMachine.currentTurn(), className.Healer)) TextureManager.activeButton(t.gameObject, true);
                        else TextureManager.activeButton(t.gameObject, false);
                    }
                    break;
                case "Paralyze":
                    classInfo.Lookup("Paralyze");
                    if (StateMachine.gamePhase == GamePhase.setup && StateMachine.currentTurn() == 1) TextureManager.activeButton(t.gameObject, !StateMachine.p1Paralyze);
                    else if (StateMachine.gamePhase == GamePhase.setup && StateMachine.currentTurn() == 2) TextureManager.activeButton(t.gameObject, !StateMachine.p2Paralyze);
                    else if (StateMachine.gamePhase == GamePhase.healer)
                    {
                        if (GameDriver.checkDeadCubes(StateMachine.currentTurn(), className.Paralyze)) TextureManager.activeButton(t.gameObject, true);
                        else TextureManager.activeButton(t.gameObject, false);
                    }
                    break;
                case "Titan":
                    classInfo.Lookup("Titan");
                    if (StateMachine.gamePhase == GamePhase.healer)
                    {
                        if (GameDriver.checkDeadCubes(StateMachine.currentTurn(), className.Titan)) TextureManager.activeButton(t.gameObject, true);
                        else TextureManager.activeButton(t.gameObject, false);
                    }
                    break;
                case "Bomb":
                    classInfo.Lookup("Bomb");
                    if (StateMachine.gamePhase == GamePhase.setup && StateMachine.currentTurn() == 1) TextureManager.activeButton(t.gameObject, !StateMachine.p1Bomb);
                    else if (StateMachine.gamePhase == GamePhase.setup && StateMachine.currentTurn() == 2) TextureManager.activeButton(t.gameObject, !StateMachine.p2Bomb);
                    else if (StateMachine.gamePhase == GamePhase.healer)
                    {
                        if (GameDriver.checkDeadCubes(StateMachine.currentTurn(), className.Bomb)) TextureManager.activeButton(t.gameObject, true);
                        else TextureManager.activeButton(t.gameObject, false);
                    }
                    break;
                default:
                    classInfo.Lookup("blank");
                    break;
            }
            if (classInfo.cName != className.Null)
            {
                TextureManager.applySprite(t.gameObject, classInfo.texture[i - 1]);
            }
        }
    }


}
