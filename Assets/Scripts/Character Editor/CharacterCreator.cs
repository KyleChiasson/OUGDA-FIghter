using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CharacterCreator : Singleton<CharacterCreator>
{
    public void BackButton()
    {
        Loader.Load(SceneManager.LoadSceneAsync("MainMenu"));
    }
    public void SaveButton()
    {
        //only save if character is valid
    }
    public void LoadButton()
    {

    }
    public void NewCharacterButton()
    {

    }
    //add static / move
    //remove static / move
 

    [SerializeField] private TMP_InputField Name;
    [SerializeField] private TMP_InputField MaxHealth;
    [SerializeField] private TMP_InputField AP;
    //list of static abilities
    //list of moves

    //each move has a name cost and effect
    //each ability has a name and effect

    //code can be made from a list of blocks
}

public class Character
{
    public string Name;
    public int MaxHealth;
    public int ActionPoints;
    public List<Action> Moves;
    public List<PassiveAbility> Passives;
}

public class Action
{
    public string Name;
    public string Description;
    public int Cost;
    public List<Block> Code;
}

public class PassiveAbility
{
    public string Name;
    public string Description;
    public List<Block> Code;
}

public class Condition
{
    public string Name;
    public bool Stacks;
    public List<Block> Code;
    public List<Block> ClearCode;
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