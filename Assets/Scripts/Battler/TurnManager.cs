using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private class Entity
    {
        public bool programmed;
        public int actionPoints;
    }

    private List<Entity> entities;
    private Entity currentEntity;
    private int actionPointsLeft;

    public void PassTurn()
    {
        //get next slot
        //set ap to current slot's ap
    }
}