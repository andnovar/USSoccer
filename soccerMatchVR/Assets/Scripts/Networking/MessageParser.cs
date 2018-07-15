using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageParser : MonoBehaviour
{
    NetworkService service;

    private const string FINISHING = "Finishing";
    private const string MARKING = "Marking";
    private const string ACCELERATION = "Acceleration";
    private const string SHORT_PASSING = "Short Passing";
    private const string DRIBBLING = "Dribbling";
    private const string POSITIONING = "Positioning";
    //private const string REACTIONS = "reactions";
    private const string BALL_CONTROL = "Ball Control";
    private const string HEIGHT = "Height";

    private string message;
    private bool flag = false;
    JSONObject gobj = new JSONObject();

    //senglee
    [SerializeField]
    int[] _stats = new int[6];
    public IEnumerable<int> GetParameters() { return _stats; }


    public Text nameplayer;
    GameObject player = null;
    public Transform spawn;

    public GameObject radarchartobj;

    int index = 0;

    // Use this for initialization
    void Start()
    {
        service = NetworkService.Instance;
        service.OnMessageReceived += Service_OnMessageReceived;
    }

    private void LateUpdate()
    {
        if (flag) {
            LoadPlayer();
            flag = false;

            if (radarchartobj) {
                PushToRadarChart push_to_rad = radarchartobj.GetComponent(typeof(PushToRadarChart)) as PushToRadarChart;
                push_to_rad.setRadarChartStar();
            }
        }
    }

    public void Previous()
    {
        if (index > 0) {
            index--;
            LoadPlayer();
        }
    }

    public void Next()
    {
        if (index < 9) {
            index++;
            LoadPlayer();
        }
    }

    void LoadPlayer()
    {
        if (player != null) {
            Destroy(player);
        }

        player = Instantiate(Resources.Load("player", typeof(GameObject))) as GameObject;
        player.transform.position = spawn.position;

        JSONNode N = JSON.Parse(message);

        JSONObject arr = N[FINISHING].AsObject;
        string name = arr[index].Value;
        nameplayer.text = name;
        int finishing = arr[name];
        _stats[3] = finishing;
        JSONObject arrmarking = N[MARKING].AsObject;
        int marking = arrmarking[name];
        _stats[4] = marking;

        JSONObject arracce = N[ACCELERATION].AsObject;
        int acc = arracce[name];
        _stats[0] = acc;

        JSONObject arrshortpass = N[SHORT_PASSING].AsObject;
        int short_pass = arrshortpass[name];
        _stats[5] = short_pass;

        JSONObject arrdribbling = N[DRIBBLING].AsObject;
        int dribbling = arrdribbling[name];
        _stats[2] = dribbling;

        JSONObject arrpositioning = N[POSITIONING].AsObject;
        int positioning = arrpositioning[name];

        JSONObject arrballcontrol = N[BALL_CONTROL].AsObject;
        int ballcontrol = arrballcontrol[name];
        _stats[1] = ballcontrol;

        JSONObject arrheight = N[HEIGHT].AsObject;
        int height = arrheight[name];

        player.transform.localScale = new Vector3(1.0f, height / 180.0f, 1.0f);
       
    }

    private void Service_OnMessageReceived(string msg)
    {
        message = msg;
        flag = true;
        Debug.Log(message);
    }

}
