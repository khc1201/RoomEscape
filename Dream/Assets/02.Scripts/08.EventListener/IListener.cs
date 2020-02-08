using UnityEngine;
using System.Collections;

public interface IListener
{
    void OnEvent(enum_EventType event_type, Component Sender, object Param = null);
}