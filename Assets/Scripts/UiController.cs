using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SetSets : GameEvent
{
    public int[] sets;
    public SetSets(int[] sets)
    {
        this.sets = sets;
    }
}
public class SetPoints : GameEvent
{
    public int points;
    public SetPoints(int p)
    {
        points = p;

    }
}
public class SetXpBar : GameEvent
{
    public int next;
    public int last;
    public int current;
    public SetXpBar(int next,int last,int current)
    {
        this.next = next;
        this.last = last;
        this.current = current;
    }
}

public class UiController : MonoBehaviour
{

    public Text _pointsTxt;
    public SkillSet[] _skillSets;
    public UIPositionTween _posTween;
    public Image _xpBar;

    bool onScreen;
    bool has;

    void Start()
    {
        EventManager.instance.AddListener<SetSets>(SetSets);
        EventManager.instance.AddListener<SetPoints>(SetPoints);
        EventManager.instance.AddListener<SetXpBar>(SetXpBar);
    }

    void SetSets(SetSets e)
    {
        for(int i =0;i<e.sets.Length;i++)
        {
            _skillSets[i].SetSet(e.sets[i]);
        }
    }
    void SetPoints(SetPoints e)
    {
        _pointsTxt.text = "Points: " + e.points;

        if (e.points > 0 && !onScreen)
        {
            _posTween.PlayForward();
            onScreen = true;
            has = true;
        }
        if(e.points == 0 && onScreen)
        {
            StartCoroutine(Wait());
            has = false;
        }

    }
    void SetXpBar(SetXpBar e)
    {
        if (e.current == 0)
            _xpBar.fillAmount = 0;
        else
            _xpBar.fillAmount = (float)(e.current - e.last) / (e.next - e.last);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        if (!has)
        {
            _posTween.PlayReverse();
            onScreen = false;
        }   
    }
}