using TMPro;
using UnityEngine;

public class ShockwaveCooldown : MonoBehaviour
{
    ShockwaveController _shockwaveCon;

    [SerializeField] TMP_Text _SWCooldown;

    Color _textColor;

    void Awake()
    {
        GameObject SWC = GameObject.Find("ShockwaveController");
        if(SWC == null)
        {
            Debug.LogError("shockwave cooldown : shockwave controller is null");
            return;
        }
        _shockwaveCon = SWC.GetComponent<ShockwaveController>();
        _textColor = _SWCooldown.color;
    }

    void Update()
    {
        if(_shockwaveCon == null)
        {
            GameObject SWC = GameObject.Find("ShockwaveController");
            if(SWC == null)
            {
                return;
            }
            _shockwaveCon = SWC.GetComponent<ShockwaveController>();
        }

        // Update the displayed shockwave cooldown and its availability state.
        float cooldownTime = _shockwaveCon.GetCooldownTime();
        if(cooldownTime == 0)
        {
            _SWCooldown.text = "S : GO";
            _textColor.a = 1f;
            _SWCooldown.color = _textColor;
        }
        else 
        {
            _SWCooldown.text = "S : " + cooldownTime.ToString("F1");
            _textColor.a = 0.2f;
            _SWCooldown.color = _textColor;
        }
    }
}
