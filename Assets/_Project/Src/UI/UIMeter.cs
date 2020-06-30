using Void  = Ransomink.Events.Void;
using Event = Ransomink.Events.Event;
using Ransomink.Events;
using Ransomink.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIMeter : MonoBehaviour
{
    [Serializable]
    public class Info : UnityEngine.Object
    {
        public bool flag;
        public float value;
        public GameObject sender;

        public Info() { }

        public Info(bool f, float v, GameObject s)
        {
            flag   = f;
            value  = v;
            sender = s;
        }
    }

    [Header("REF")]
    [SerializeField] protected Image meter;
    [SerializeField] protected Image trail;

    [Header("STATS")]
    [SerializeField] protected float tolerance;
    [SerializeField] protected float curValue;
    [SerializeField] protected float maxValue;

    [Header("TRAIL")]
    [SerializeField] protected Gradient meterGradient;
    [SerializeField] protected Color meterColor;
    [SerializeField] protected Color trailColor;
    [SerializeField] protected float trailDelay;
    [SerializeField] protected float trailDuration;

    [Header("EVENTS")]
    [SerializeField] protected Event OnValueChanged;
    [SerializeField] protected Event OnDepleted;

    [Header("SETTINGS")]
    [SerializeField] protected bool useMeter;
    [SerializeField] protected bool useTrail;

    private bool  _isTrailing;
    private Timer _timer;

    public float CurValue
    {
                get { return curValue; }
        private set
        {
            curValue = Mathf.Clamp(value, 0f, maxValue);
            if (curValue <= 0f)
            {
                curValue = 0f;
                OnDepleted.Raise(_void);
                //return;
            }

            OnValueChanged.Raise(_void);
        }
    }

    public float MaxValue
    {
                get { return  maxValue; }
        private set { value = maxValue; }
    }

    public float Percentage
    {
        get { return meter.fillAmount; }
    }

    private Void  _void;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (_isTrailing)
        {
            if (!_timer.IsDone)
            {
                DoTrail();
            }
            else
            {
                _isTrailing = false;
            }
        }
    }

    private void Init()
    {
        _timer      = new Timer();
        CurValue    = MaxValue;
        //meter.color = meterColor;
        meter.color = meterGradient.Evaluate(meter.fillAmount);
        trail.color = trailColor;
        meter.fillAmount = CurValue / MaxValue;
        trail.fillAmount = meter.fillAmount;
    }

    public void DoDeplete()
    {
        Debug.LogWarning($"Meter is empty");
    }

    public void UpdateValue(UIMeterArgs e)
    {
        switch (e.flag)
        {
            case true:
                DoGain(e.value);
                break;
            case false:
                DoLoss(e.value);
                break;
        }

        UpdateMeter();
    }

    public void UpdateMeter()
    {
        if (!useMeter) return;

        meter.fillAmount = CurValue / MaxValue;
        meter.color      = meterGradient.Evaluate(meter.fillAmount);
        UpdateTrail();
    }

    public void UpdateTrail()
    {
        if (!useTrail) return;

        _timer.NewDuration(trailDuration);
        _isTrailing = true;
    }

    private void DoLoss(float value)
    {
        CurValue -= value;
    }

    private void DoGain(float value)
    {
        CurValue += value;
    }

    private void DoTrail()
    {
        trail.fillAmount = Mathf.Lerp(trail.fillAmount, meter.fillAmount, _timer.PercentageDone);
    }
}
