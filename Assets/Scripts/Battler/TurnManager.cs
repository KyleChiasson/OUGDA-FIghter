using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{
    private class Entity
    {
        public bool programmed;
        public int actionPoints;
    }

    private List<Entity> entities;
    private Entity currentEntity;
    public int ActionPointsLeft;

    private void Update()
    {
        if (ActionPointsLeft == 0 && !currentEntity.programmed)
            PassTurn();
    }

    private void PassTurn()
    {
        int index = entities.IndexOf(currentEntity);
        if(index == -1)
            throw new System.Exception("Entity removal not implimented");
        index = (index == entities.Count - 1) ? 0 : index + 1;
        ActionPointsLeft = (currentEntity = entities[index]).actionPoints;
    }
    
    public void AddEntity()
    {

    }
    public void RemoveEntity()
    {

    }
}