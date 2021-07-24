using System.Collections;
using System.Collections.ObjectModel;
using UnityEngine;

public static class Mover
{
    private static readonly System.Random rand = new System.Random();

    private static double _dicrement;

    public static double Dicrement
    {
        get => _dicrement;
        set
        {
            _dicrement = value;
            animationDuration = new Vector3(0, (float)_dicrement, 0);
        }
    }
    private static Vector3 animationDuration;

    public static ObservableCollection<GameObject> _platform_Pool = new ObservableCollection<GameObject>();

    public static IEnumerator Move_Platform()
    {
        while (true)
        {
            foreach (var platform in _platform_Pool)
            {
                platform.transform.position -= animationDuration;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public static Vector3 Random_Place(Vector3 Spawn_rand)
    {
        switch (rand.Next(1, 5))
        {
            case 2: //left
                Spawn_rand.x = -1.2f;
                break;
            case 3: //centre
                Spawn_rand.x = 0;
                break;
            case 4: //right
                Spawn_rand.x = 1.2f;
                break;
        }
        return Spawn_rand;
    }
}
