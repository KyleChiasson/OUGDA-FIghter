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