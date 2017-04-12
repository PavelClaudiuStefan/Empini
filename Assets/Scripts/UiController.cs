using UnityEngine;
using UnityEngine.UI;

public class SetHpUiEvent : GameEvent
{
    int hp;
    public int Hp { get { return hp; } set { hp = value; } }
    public SetHpUiEvent(int hp)
    {
        Hp = hp;
    }
}

public class UiController : MonoBehaviour
{

    public Text _hpText;

    void Start()
    {
        EventManager.instance.AddListener<SetHpUiEvent>(SetHp);
    }

    public void SetHp(SetHpUiEvent e)
    {
        _hpText.text = e.Hp.ToString();
    }

}