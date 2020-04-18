using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu (fileName = "GameEventListener", menuName = "GameEventListener", order = 0)]
public class GameEventListener : ScriptableObject {

    public GameEvent GameEvent;
    public UnityEvent Response;

    private void OnEnable() {
        this.Register();
    }

    public void OnEventRaised() {
        Response.Invoke();
    }

    void Register() {
        if (GameEvent != null && Response != null)
            GameEvent.RegisterListener(this);
    }

    public void SetupListener(GameEvent e, UnityAction action) {
        if (e != null) {
            this.GameEvent = e;

            UnityEvent response = new UnityEvent();
            response.AddListener(action);
            this.Response = response;

            this.Register();
        }
    }
}
