using TMPro;
using UnityEngine;

public class DashCooldown : MonoBehaviour
{
    Player _player;

    [SerializeField] TMP_Text _dashCD;

    Color _textColor;

    void Awake()
    {
        GameObject pl = GameObject.FindWithTag("Player");
        if(!pl.TryGetComponent(out _player))
        {
            Debug.LogError("dash cooldown : player is null");
            return;
        }
        _textColor = _dashCD.color;
    }

    void Update()
    {
        if(_player == null)
        {
            GameObject pl = GameObject.FindWithTag("Player");
            if(pl == null)
            return;
            _player = pl.GetComponent<Player>();
        }


        float cooldownTime = _player.GiveDashTimer();
        if(cooldownTime == 0)
        {
            _dashCD.text = "D : GO";
            _textColor.a = 1f;
            _dashCD.color = _textColor;
        }
        else 
        {
            _dashCD.text = "D : " + cooldownTime.ToString("F1");
            _textColor.a = 0.2f;
            _dashCD.color = _textColor;
        }
    }
}