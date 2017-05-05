using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum className
{
	King,
	Brawler,
	Sentinel,
	Shadow,
	Grunt,
	Peasant,
	Healer,
	Paralyze,
	Titan,
	Bomb,
	//TODO: Remove these.
	//These are Temp values I made to fix complie errors --Jason
	className1,
	className2,
	className3,
    Null
}

public enum classType
{
    classType1,
    classType2,
    classType3
}

public class ClassLookup : MonoBehaviour {

    //This class contains the lookup info for all of the different unit classes.
    //
    //Note: The hardcoded values are only here for testing, these will need to be modified when
    //we come up With the real numbers for all the units.
    //
    public static ClassLookup classinfo;
    public className cName;
    public classType type;
    public float attack;
    public float defense;
    public int cost;
    public Texture[] texture;
    public string description;

    public List<className> p1TexKey;
    public List<className> p2TexKey;
    public List<Texture> p1TexVal;
    public List<Texture> p2TexVal;
    public Dictionary<className, Texture> p1Textures = new Dictionary<className, Texture>();
    public Dictionary<className, Texture> p2Textures = new Dictionary<className, Texture>();


    public void Awake()
    {
        classinfo = this;
        texture = new Texture[2];
        if (p1TexKey.Count == p1TexVal.Count)
        {
            for (int i = 0; i < p1TexKey.Count; i++)
            {
                p1Textures.Add(p1TexKey[i], p1TexVal[i]);
            }
        }
        if (p2TexKey.Count == p2TexVal.Count)
        {
            for (int i = 0; i < p2TexKey.Count; i++)
            {
                p2Textures.Add(p2TexKey[i], p2TexVal[i]);
            }
        }
    }

    public string name2String()
    {
        if (cName == className.King) return "King";
        else if (cName == className.Brawler) return "Brawler";
        else if (cName == className.Sentinel) return "Sentinal";
        else if (cName == className.Shadow) return "Shadow";
        else if (cName == className.Grunt) return "Grunt";
        else if (cName == className.Peasant) return "Peasant";
        else if (cName == className.Healer) return "Healer";
        else if (cName == className.Paralyze) return "Paralyze";
        else if (cName == className.Titan) return "Titan";
        else if (cName == className.Bomb) return "Bomb";
        else return "Derp, it broke.";
    }


    public UnitClass Lookup(string n, UnitClass unitLookup)
    {
       /* switch (n)
        {
            case "King":
                cName = className.King;
                type = classType.classType1;
                attack = 2;
                defense = 6;
                cost = 0;
				description = "The Leaders of their respective forces; Kings are to be protected at all costs. When your King dies, you lose.";
                //If it is knocked off of the board or defeated, it's owner loses the game. Limit: 1 per player per round.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                unitLookup.unitSetup(cName, type, attack, defense, StateMachine.currentTurn(), cost, texture);
                return unitLookup;
            case "Brawler":
                cName = className.Brawler;
                type = classType.classType1;
                attack = 3;
                defense = 2;
                cost = 3;
                description = "A decent attacking unit. Good for throwing at your opponent repeatedly.";
                //If it lands on your side of the field after the initial flick, it can be flicked a second time in the same turn.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                unitLookup.unitSetup(cName, type, attack, defense, StateMachine.currentTurn(), cost, texture);
                return unitLookup;
			case "Sentinel":
				cName = className.Sentinel;
				type = classType.classType1;
				attack = 2;
				defense = 4;
				cost = 4;
                description = "An excellent defender. Holds the line and can clear your side of the field.";
                // No ability.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                unitLookup.unitSetup(cName, type, attack, defense, StateMachine.currentTurn(), cost, texture);
                return unitLookup;
			case "Shadow":
				cName = className.Shadow;
				type = classType.classType1;
				attack = 4;
				defense = 1;
				cost = 3;
                description = "An espionage expert. This piece can disappear if it lands on your opponent's side, and reappear later!";
                //If it lands on opponent's side of the field at the end of your turn, you may remove from play. Use your flick action to place on board on a later turn.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                unitLookup.unitSetup(cName, type, attack, defense, StateMachine.currentTurn(), cost, texture);
                return unitLookup;
			case "Grunt":
				cName = className.Grunt;
				type = classType.classType1;
				attack = 2;
				defense = 2;
				cost = 3;
                description = "A basic utility unit that is cheap and effective.";
                //No ability.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                unitLookup.unitSetup(cName, type, attack, defense, StateMachine.currentTurn(), cost, texture);
                return unitLookup;
			case "Peasant":
				cName = className.Peasant;
				type = classType.classType1;
				attack = 1;
				defense = 1;
				cost = 1;
                description = "Not intended for combat, but very expendable. These units are best used as a wall from enemy attacks.";
                //May be flicked twice in the same turn. Limit: 4 per player per round.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                unitLookup.unitSetup(cName, type, attack, defense, StateMachine.currentTurn(), cost, texture);
                return unitLookup;
			case "Healer":
				cName = className.Healer;
				type = classType.classType1;
				attack = 0;
				defense = 6;
				cost = 4;
                description = "This piece can be sacrificed in order to return destroyed pieces to your side of the field.";
                //Can not be flicked. Can be sacrificed to return up to 6 cost worth of troops from the dead to your side of the field (depends on coin toss per attempted piece). Limit: 1 per player per round.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                unitLookup.unitSetup(cName, type, attack, defense, StateMachine.currentTurn(), cost, texture);
                return unitLookup;
			case "Paralyze":
				cName = className.Paralyze;
				type = classType.classType1;
				attack = 0;
				defense = 3;
				cost = 2;
                description = "Stops your opponent from utilizing any unit touching it. Immune to other Paralyze units.";
                //Can only be placed on top of an opponent's cube instead of using a flicking action. Stops that cube from being used. Must be flicked off to remove effect.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                unitLookup.unitSetup(cName, type, attack, defense, StateMachine.currentTurn(), cost, texture);
                return unitLookup;
			case "Titan":
				cName = className.Titan;
				type = classType.classType1;
				attack = 6;
				defense = 4;
				cost = 7;
				description = "A colossal offensive unit. This expensive piece is to be feared.";
                //No ability.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                unitLookup.unitSetup(cName, type, attack, defense, StateMachine.currentTurn(), cost, texture);
                return unitLookup;
            case "Bomb":
                cName = className.Bomb;
                type = classType.classType1;
                attack = 8;
                defense = 2;
                cost = 4;
				description = "KABOOOOOOOOOOOOOOOOOOOOOOOOOOM!!!";
                //Use flick action to detonate, creating an area of effect collision. Limit: 1 per player per round.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                unitLookup.unitSetup(cName, type, attack, defense, StateMachine.currentTurn(), cost, texture);
                return unitLookup;
            default:
                return null;
        }*/
		  Lookup (n);
		  if (cName == className.Null) {
			return null;
		  } 
       unitLookup.unitSetup(cName, type, attack, defense, StateMachine.currentTurn(), cost, texture);
		    return unitLookup;
    }

	public void Lookup(string n)
	{
        switch (n)
        {
            case "King":
                cName = className.King;
                type = classType.classType1;
                attack = 2;
                defense = 20;
                cost = 0;
				description = "The Leaders of their respective forces; Kings are to be protected at all costs. When your King dies, you lose.";
                //If it is knocked off of the board or defeated, it's owner loses the game. Limit: 1 per player per round.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                break;
            case "Brawler":
                cName = className.Brawler;
                type = classType.classType1;
                attack = 3;
                defense = 2;
                cost = 3;
				description = "Your basic attacking unit. Good for throwing at your opponent repeatedly.";
                //If it lands on your side of the field after the initial flick, it can be flicked a second time in the same turn.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                break;
            case "Sentinel":
                cName = className.Sentinel;
                type = classType.classType1;
                attack = 2;
                defense = 4;
                cost = 4;
				description = "An excellent defender. Holds the line and can clear your side of the field.";
                // No ability.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                break;
            case "Shadow":
                cName = className.Shadow;
                type = classType.classType1;
                attack = 4;
                defense = 1;
                cost = 3;
				description = "An espionage expert. This piece can disappear if it lands on your opponent's side, and reappear later!";
                //If it lands on opponent's side of the field at the end of your turn, you may remove from play. Use your flick action to place on board on a later turn.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                break;
            case "Grunt":
                cName = className.Grunt;
                type = classType.classType1;
                attack = 2;
                defense = 2;
                cost = 3;
				description = "A basic utility unit that is cheap and effective.";
                //No ability.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                break;
            case "Peasant":
                cName = className.Peasant;
                type = classType.classType1;
                attack = 1;
                defense = 1;
                cost = 1;
				description = "Not intended for combat, but very expendable. These units are best used as a wall from enemy attacks.";
                //May be flicked twice in the same turn. Limit: 3 per player per round.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                break;
            case "Healer":
                cName = className.Healer;
                type = classType.classType1;
                attack = 0;
                defense = 6;
                cost = 4;
				description = "This piece can be sacrificed in order too return destroyed pieces to your side of the field.";
                //Can not be flicked. Can be sacrificed to return up to 6 cost worth of troops from the dead to your side of the field (depends on coin toss per attempted piece). Limit: 1 per player per round.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                break;
            case "Paralyze":
                cName = className.Paralyze;
                type = classType.classType1;
                attack = 0;
                defense = 3;
                cost = 2;
				description = "Stops your opponent from utilizing any unit touching it. Immune to other Paralyze units.";
                //Can only be placed on top of an opponent's cube instead of using a flicking action. Stops that cube from being used. Must be flicked off to remove effect.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                break;
            case "Titan":
                cName = className.Titan;
                type = classType.classType1;
                attack = 6;
                defense = 4;
                cost = 7;
                description = "A colossal offensive unit. This expensive piece is to be feared.";
                //No ability.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                break;
            case "Bomb":
                cName = className.Bomb;
                type = classType.classType1;
                attack = 0;
                defense = 2;
                cost = 4;
                description = "KABOOOOOOOOOOOOOOOOOOOOOOOOOOM!!!";
                //Use flicking action to detonate, creating an area of effect collision. Limit: 1 per player per round.
                texture[0] = p1Textures[cName];
                texture[1] = p2Textures[cName];
                break;
            default:
			  cName = className.Null;
                break;
        }
	}

    public static ClassLookup getClassLookup()
    {
        return classinfo;
    }
}
