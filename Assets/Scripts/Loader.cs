using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DDOL))]
public class Loader : Singleton<Loader>
{
    [SerializeField] private GameObject screen;
    [SerializeField] private Image dot1;
    [SerializeField] private Image dot2;
    [SerializeField] private Image dot3;
    [SerializeField] private float speed = .5f;
    private static bool loading = false;
    private static float time = 0f;
    public static IEnumerator Load(AsyncOperation operation)
    {
        if (!loading)
        {
            loading = true;
            time = 0f;
            Instance.screen.SetActive(true);
            while (!operation.isDone)
            {
                if (time >= Instance.speed)
                {
                    Instance.IncreaseState();
                    time -= Instance.speed;
                }
                time += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Instance.screen.SetActive(false);
            Instance.ResetState();
            loading = false;
        }
    }
    static bool[][] dotStates = new bool[][]{
        new bool[]{ false, false, false },
        new bool[]{ true, false, false },
        new bool[]{ true, true, false },
        new bool[]{ false, true, true },
        new bool[]{ false, false, true },
    };
    static Color grey = new Color(175, 175, 175);
    int index = 0;
    private void IncreaseState()
    {
        index++;
        if (index >= 6)
            index -= 6;
        dot1.color = dotStates[index][0] ? Color.white : grey;
        dot2.color = dotStates[index][1] ? Color.white : grey;
        dot3.color = dotStates[index][2] ? Color.white : grey;
    }
    private void ResetState()
    {
        index = 0;
        dot1.color = dotStates[index][0] ? Color.white : grey;
        dot2.color = dotStates[index][1] ? Color.white : grey;
        dot3.color = dotStates[index][2] ? Color.white : grey;
    }
}