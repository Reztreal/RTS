using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    private SkillData _skillData;
    private GameObject _source;
    private Button _button;
    private bool _isReady;
    
    public void Initialize(SkillData skillData, GameObject source)
    {
        _skillData = skillData;
        _source = source;
    }

    public void Trigger(GameObject target = null)
    {
        if (!_isReady) return;
        StartCoroutine(WrappedTrigger(target));
    }

    private IEnumerator WrappedTrigger(GameObject target = null)
    {
        yield return new WaitForSeconds(_skillData.skillDuration);
        _skillData.Trigger(_source, target);
        SetReady(false);
        yield return new WaitForSeconds(_skillData.skillCooldown);
        SetReady(true);
    }

    public void SetButton(Button button)
    {
        _button = button;
        SetReady(true);
    }

    private void SetReady(bool isReady)
    {
        _isReady = isReady;
        if (_button != null) _button.interactable = _isReady;
    }
    
    public SkillData SkillData { get { return _skillData; } }
}
