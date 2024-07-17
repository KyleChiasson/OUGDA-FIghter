using System.Collections.Generic;

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
}

public class PassiveAbility
{
    public string Name;
    public string Description;
}

//why tf am I designing scratch