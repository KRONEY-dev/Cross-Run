using System.Collections.Specialized;
using System.Collections;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class Platform_Spawn_Script : MonoBehaviour
{
    public GameObject Background;
    public GameObject Score_obj;
    public GameObject MainActor;
    public GameObject myPrefab;

    private Text Score_ref;

    private int score = 0;
    private int Score
    {
        get => score;
        set
        {
            State.Setting_state(score, ref platform_fly_time_duration_increment);
            score = value;
        }
    }

    private double platform_fly_time_duration = 0.005d;
    private double Platform_fly_time_duration
    {
        get => platform_fly_time_duration;
        set
        {
            platform_fly_time_duration = value;
            Mover.Dicrement = platform_fly_time_duration;
        }
    }

    private double platform_fly_time_duration_increment = 0.08d;

    private const int max_platform_count = 5;

    private readonly ObservableCollection<GameObject> Platform_Pool = new ObservableCollection<GameObject>();
    private void Platform_Pool_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        Mover.platform_Pool = Platform_Pool;
    }

    private void OnEnable()//Start point
    {
        Platform_Pool.CollectionChanged += Platform_Pool_CollectionChanged;
        Score_ref = Score_obj.GetComponentInChildren<Text>();

        StartCoroutine(Score_Ñounting_and_Game_Settings());
        StartCoroutine(Platforme_Spawn());
    }

    private IEnumerator Score_Ñounting_and_Game_Settings()
    {
        Score = 0;
        Vector2 gravity_index = new Vector2(0, 0.1f);
        Physics2D.gravity = new Vector2(0, -1);

        while (MainActor.activeSelf && !Background.activeSelf)
        {
            Physics2D.gravity -= gravity_index;

            Score_ref.text = $"Score : {Score}";

            Score++;

            Platform_fly_time_duration += platform_fly_time_duration_increment;

            yield return new WaitForSeconds(1);
        }

        yield break;
    }

    private IEnumerator Platforme_Spawn()
    {
        StartCoroutine(Mover.Move_Platform());
        while (MainActor.activeSelf && !Background.activeSelf)
        {
            if (Platform_Pool.Count < max_platform_count)
            {
                Vector3 Spawn_coordinates = new Vector3(transform.position.x, transform.position.y);

                GameObject platform = Instantiate(myPrefab, Mover.Random_Place(Spawn_coordinates), Quaternion.identity);
                Platform_Pool.Add(platform);
            }
            yield return new WaitForSeconds(3);
        }
        StopCoroutine(Mover.Move_Platform());
        foreach (GameObject platform in Platform_Pool)
        {
            Destroy(platform);
        }
        Platform_Pool.Clear();

        yield break;
    }
}
