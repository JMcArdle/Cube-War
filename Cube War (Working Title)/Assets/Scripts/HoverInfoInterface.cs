using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverInfoInterface : MonoBehaviour {
    public GameObject imageObj;
    public Image image;
    public GameObject classNameObj;
    public Text className;
    public GameObject attackObj;
    public Text attack;
    public GameObject defenseObj;
    public Text defense;
    public GameObject costObj;
    public Text cost;
    public GameObject descriptionObj;
    public Text description;
    public ClassLookup lookup;



    public void Start()
    {
        lookup = ClassLookup.getClassLookup();
        image = imageObj.GetComponent<Image>();
        className = classNameObj.GetComponent<Text>();
        attack = attackObj.GetComponent<Text>();
        defense = defenseObj.GetComponent<Text>();
        cost = costObj.GetComponent<Text>();
        description = descriptionObj.GetComponent<Text>();
    }

    public void updateHoverInfo(string check)
    {
        lookup.Lookup(check);
        foreach(Transform t in this.transform)
            if(t.gameObject.name == "Image") TextureManager.applySprite(t.gameObject, lookup.texture[StateMachine.currentTurn() - 1]);
        className.text = lookup.name2String();
        attack.text = "Attack: "+lookup.attack;
        defense.text = "Defense: " + lookup.defense;
        cost.text = lookup.cost + "\nCost";
        description.text = lookup.description;
    }
}
