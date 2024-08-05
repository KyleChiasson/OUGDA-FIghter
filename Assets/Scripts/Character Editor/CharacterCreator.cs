using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCreator : Singleton<CharacterCreator>
{
    //save button
    //load button
    //new button 

    [SerializeField] private TMP_InputField Name;
    [SerializeField] private TMP_InputField AP;
    //list of static abilities
    //list of moves

    //each move has a name cost and effect
    //each ability has a name and effect

    //code can be made from a list of blocks
}

public class Block
{
    public string Name;
}

//blocks
//shape (allows user to specify an area)
//damage (amt)
//heal (amt)
//shield (amt) (duration)
//move (amt)
//effect (allows user to apply custom effect)
//generate/remove terrain
//summon summon (specify summon)

//triggers
//when take damage (gives a number)
//when attacked
//until end of turn
//until start of next turn
//at start of turn
//at end of turn
//when action is taken (gives number of ap)

//conditions
//damage change (amt)
//movement change (amt)
//heal change (amt)
//shield change (amt)
//clear condition
//decrement/increment condition

//conditionals
//if / else / endif
//and or not
//player
//enemy
//ally
//terrain

//math
//variable
//= + - * / %