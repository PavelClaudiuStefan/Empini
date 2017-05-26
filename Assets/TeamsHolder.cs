using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegisterInTeam : GameEvent
{
    public float id;
    public int order;

    public RegisterInTeam(float id,int order)
    {
        this.id = id;
        this.order = order;
    }
}

public class TeamsHolder : NetworkBehaviour {

    public class Team
    {
        public List<float> playerId = new List<float>();
        public List<Vector3> pos = new List<Vector3>();

        public Team(Vector3 pos1, Vector3 pos2)
        {
            pos.Add(pos1);
            pos.Add(pos2);
        }

        public int playersInWinZone;

        int next = 0;

        public void AddPlayer(float id)
        {
            playerId.Add(id);
            EventManager.instance.Raise(new TeleportOnPosition(pos[next], id));
            next++;
        }

        public void CheckPlayerIn(float id)
        {
            foreach (var p in playerId)
                if (id == p)
                    playersInWinZone++;

        }

        public void CheckPlayerOut(float id)
        {
            foreach (var p in playerId)
                if (id == p)
                    playersInWinZone--;
        }
    }

    [SyncVar]
    public int team_A_Score;
    [SyncVar]
    public int team_B_Score;

    public List<Team> teams;

    public Transform team1Pos1;
    public Transform team1Pos2;
    public Transform team2Pos1;
    public Transform team2Pos2;

    public Image _slider1;
    public Image _slider2;

    void Start() {
        if (isServer)
        {
            teams = new List<Team>();
            teams.Add(new Team(team1Pos1.position, team1Pos2.position));
            teams.Add(new Team(team2Pos1.position, team2Pos2.position));

            EventManager.instance.AddListener<RegisterInTeam>(RegisterInTeam);
            StartCoroutine(CheckPlayersIn());
        }
    }

    void RegisterInTeam(RegisterInTeam e)
    {
        if ((float)e.order % 2 == 0)
        {
            teams[0].AddPlayer(e.id);
        }
        else
        {
            teams[1].AddPlayer(e.id);
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (isServer && col.tag == "Player")
        {
            float id = col.gameObject.GetComponent<PlayerNetworkIdentification>().id;
            foreach (var t in teams)
                t.CheckPlayerIn(id);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (isServer && col.tag == "Player")
        {
            float id = col.gameObject.GetComponent<PlayerNetworkIdentification>().id;
            foreach (var t in teams)
                t.CheckPlayerOut(id);
        }
    }

    IEnumerator CheckPlayersIn()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            int points = Mathf.Abs(teams[0].playersInWinZone - teams[1].playersInWinZone);

            if (teams[0].playersInWinZone > teams[1].playersInWinZone)
                team_A_Score += points;

            if (teams[1].playersInWinZone > teams[0].playersInWinZone)
                team_B_Score += points;
        }
    }

    void Update()
    {
        _slider1.fillAmount = (float)team_A_Score / 500;
        _slider2.fillAmount = (float)team_B_Score / 500;
    }
}
