using UnityEngine;
using System.Collections;

public interface IListener
{
    void OnEvent(EVENT_TYPE event_type, Component Sender, object Param = null);
}