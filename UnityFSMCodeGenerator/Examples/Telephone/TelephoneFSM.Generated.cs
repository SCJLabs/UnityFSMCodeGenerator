//
// Auto-generated by Unity FSM Code Generator:
//     https://github.com/justonia/UnityFSMCodeGenerator
//
// ** Do not modify, changes will be overwritten. **
//

using System.Collections;
using System.Collections.Generic;

namespace UnityFSMCodeGenerator.Examples
{
    // This FSM models a simple telephone that is capable of receiving calls, putting them on hold, or leaving a message.
    public class TelephoneFSM :  UnityFSMCodeGenerator.BaseFsm, UnityFSMCodeGenerator.IFsmIntrospectionSupport
    {
        public readonly static string GeneratedFromPrefab = "Assets/UnityFSMCodeGenerator/UnityFSMCodeGenerator/Examples/Telephone/TelephoneFSM.prefab";
        public readonly static string GeneratedFromGUID = "3045bb4d728b8b5478f1e8a3ed6bab84";
        
        public enum State
        {
            OffHook,
            Ringing,
            Connected,
            OnHold,
            DisconnectCall,
        }
    
        public const State START_STATE = State.OffHook;
    
        public enum Event
        {
            CallConnected,
            CallDialed,
            HungUp,
            LeftMessage,
            OffHold,
            OnHold,
        }
    
        public interface IContext : UnityFSMCodeGenerator.IFsmContext
        {
            State State { get; set; }
            UnityFSMCodeGenerator.Examples.IAudioControl AudioControl { get; }
            UnityFSMCodeGenerator.Examples.IHaptics Haptics { get; }
            UnityFSMCodeGenerator.Examples.ITelephone Telephone { get; }
        }
    
        #region Public Methods
    
        public IContext Context { get { return context; }}
        
        // TelephoneFSM is completely stateless when events are not firing. Bind() sets
        // the current context but does nothing else until you call SendEvent().
        // Instances of this class may be re-used and shared by calling Bind() in-between
        // invocations of SendEvent().
        public void Bind(IContext context)
        {
            if (isFiring) {
                throw new System.InvalidOperationException("Cannot call TelephoneFSM.Bind(IContext) while events are in-progress");
            }
    
            this.context = context;
        }
    
        // Send an event, possibly triggering a transition, an internal action, or an 
        // exception if the event is not handled in the current state. If an event is in
        // process of firing, the event is queued and then sent once firing is done.
        public void SendEvent(Event _event)
        {
            if (eventPool.Count == 0) {
                eventPool.Enqueue(new QueuedEvent());
            }
            var queuedEvent = eventPool.Dequeue();
            queuedEvent._event = _event;
            InternalSendEvent(queuedEvent);
        }
        
        public static IContext NewDefaultContext(
            UnityFSMCodeGenerator.Examples.IAudioControl audioControl,
            UnityFSMCodeGenerator.Examples.IHaptics haptics,
            UnityFSMCodeGenerator.Examples.ITelephone telephone,
            State startState = START_STATE)
        {
            return new DefaultContext{
                State = startState,
                AudioControl = audioControl, Haptics = haptics, Telephone = telephone, 
            };
        }
    
        
        // Convenience so you can use the State enum in a Dictionary and not worry about
        // garbage creation via boxing: new Dictionary<State, Foo>(new StateComparer());
        public struct StateComparer : IEqualityComparer<State>
        {
            public bool Equals(State x, State y) { return x == y; }
            public int GetHashCode(State obj) { return obj.GetHashCode(); }
        }
    
        #endregion
    
        #region Private Variables
           
        public override UnityFSMCodeGenerator.IFsmContext BaseContext { get { return context; }}
        
        private class QueuedEvent
        {
            public Event _event;
        }
    
        readonly Queue<QueuedEvent> eventQueue = new Queue<QueuedEvent>();
        readonly Queue<QueuedEvent> eventPool = new Queue<QueuedEvent>();
        private bool isFiring;
        private IContext context;
    
        private class DefaultContext : IContext
        {
            public State State { get; set; }
            public UnityFSMCodeGenerator.Examples.IAudioControl AudioControl { get; set; }
            public UnityFSMCodeGenerator.Examples.IHaptics Haptics { get; set; }
            public UnityFSMCodeGenerator.Examples.ITelephone Telephone { get; set; }
            
        }
    
        #endregion
    
        #region Private Methods
        
        private void InternalSendEvent(QueuedEvent _event)
        {
            if (isFiring) {
                eventQueue.Enqueue(_event);
                return;
            }
    
            try {
                isFiring = true;
    
                SingleInternalSendEvent(_event);
    
                while (eventQueue.Count > 0) {
                    var queuedEvent = eventQueue.Dequeue();
                    SingleInternalSendEvent(queuedEvent);
                    eventPool.Enqueue(queuedEvent);
                }
            }
            finally {
                isFiring = false;
                eventQueue.Clear();
            }
        }
    
        
        private void SingleInternalSendEvent(QueuedEvent _eventData)
        {
            Event _event = _eventData._event;
            State from = context.State;
        
            switch (context.State) {
            case State.OffHook:
                switch (_event) {        
                case Event.CallDialed:
                    if (TransitionTo(State.Ringing, from)) {
                        SwitchState(from, State.Ringing);
                    }
                    break;        
                default:
                    if (!HandleInternalActions(from, _event)) {
                        throw new System.Exception(string.Format("Unhandled event '{0}' in state '{1}'", _event.ToString(), context.State.ToString()));
                    }
                    break;
                }
                break;
        
            case State.Ringing:
                switch (_event) {        
                case Event.CallConnected:
                    if (TransitionTo(State.Connected, from)) {
                        SwitchState(from, State.Connected);
                    }
                    break;        
                case Event.HungUp:
                    if (TransitionTo(State.OffHook, from)) {
                        SwitchState(from, State.OffHook);
                    }
                    break;        
                default:
                    if (!HandleInternalActions(from, _event)) {
                        throw new System.Exception(string.Format("Unhandled event '{0}' in state '{1}'", _event.ToString(), context.State.ToString()));
                    }
                    break;
                }
                break;
        
            case State.Connected:
                switch (_event) {        
                case Event.LeftMessage:
                    if (TransitionTo(State.DisconnectCall, from)) {
                        SwitchState(from, State.DisconnectCall);
                    }
                    break;        
                case Event.OnHold:
                    if (TransitionTo(State.OnHold, from)) {
                        SwitchState(from, State.OnHold);
                    }
                    break;        
                case Event.HungUp:
                    if (TransitionTo(State.DisconnectCall, from)) {
                        SwitchState(from, State.DisconnectCall);
                    }
                    break;        
                default:
                    if (!HandleInternalActions(from, _event)) {
                        throw new System.Exception(string.Format("Unhandled event '{0}' in state '{1}'", _event.ToString(), context.State.ToString()));
                    }
                    break;
                }
                break;
        
            case State.OnHold:
                switch (_event) {        
                case Event.OffHold:
                    if (TransitionTo(State.Connected, from)) {
                        SwitchState(from, State.Connected);
                    }
                    break;        
                case Event.HungUp:
                    if (TransitionTo(State.DisconnectCall, from)) {
                        SwitchState(from, State.DisconnectCall);
                    }
                    break;        
                default:
                    if (!HandleInternalActions(from, _event)) {
                        throw new System.Exception(string.Format("Unhandled event '{0}' in state '{1}'", _event.ToString(), context.State.ToString()));
                    }
                    break;
                }
                break;
        
            case State.DisconnectCall:
                switch (_event) {        
                case Event.HungUp:
                    if (TransitionTo(State.OffHook, from)) {
                        SwitchState(from, State.OffHook);
                    }
                    break;        
                default:
                    if (!HandleInternalActions(from, _event)) {
                        throw new System.Exception(string.Format("Unhandled event '{0}' in state '{1}'", _event.ToString(), context.State.ToString()));
                    }
                    break;
                }
                break;
        
            }
        }
    
        
        
        private bool HandleInternalActions(State state, Event _event)
        {
            // no states have internal actions, intentionally empty
            return false;
        }
    
    
        private void SwitchState(State from, State to)
        {
            context.State = to;
            DispatchOnExit(from);
            DispatchOnEnter(to);
        }
        
        private bool TransitionTo(State state, State from)
        {
            // TODO: Guard conditions might hook in here
            return true;
        }
    
        
        private void DispatchOnEnter(State state)
        {
            switch (state) {
            case State.OffHook:
                break;
            case State.Ringing:
                context.AudioControl.StartRinging();
                context.Haptics.Pulse();
                break;
            case State.Connected:
                context.Telephone.ConnectedToCall();
                break;
            case State.OnHold:
                context.Telephone.SuspendCall();
                break;
            case State.DisconnectCall:
                context.Telephone.DisconnectCall();
                break;
            }
        }
    
        
        private void DispatchOnExit(State state)
        {
            switch (state) {
            case State.OffHook:
                break;
            case State.Ringing:
                context.AudioControl.StopRinging();
                break;
            case State.Connected:
                break;
            case State.OnHold:
                break;
            case State.DisconnectCall:
                break;
            }
        }
    
        #endregion
        
        #region IFsmIntrospectionSupport
        
        string IFsmIntrospectionSupport.GeneratedFromPrefabGUID { get { return GeneratedFromGUID; }}
        
        private Dictionary<State, string> debugStateLookup = new Dictionary<State, string>(new StateComparer()){
            { State.OffHook, "Off Hook" },
            { State.Ringing, "Ringing" },
            { State.Connected, "Connected" },
            { State.OnHold, "On Hold" },
            { State.DisconnectCall, "Disconnect Call" },
        };
        private List<string> debugStringStates = new List<string>(){
            "Off Hook",
            "Ringing",
            "Connected",
            "On Hold",
            "Disconnect Call",
        };
        private Dictionary<string, State> stateNameToStateLookup = new Dictionary<string, State>(){
            { "Off Hook", State.OffHook },
            { "Ringing", State.Ringing },
            { "Connected", State.Connected },
            { "On Hold", State.OnHold },
            { "Disconnect Call", State.DisconnectCall },
        };
        
        string UnityFSMCodeGenerator.IFsmIntrospectionSupport.State { get { return context != null ? debugStateLookup[context.State] : null; }}
        
        List<string> UnityFSMCodeGenerator.IFsmIntrospectionSupport.AllStates { get { return debugStringStates; }}
        
        object UnityFSMCodeGenerator.IFsmIntrospectionSupport.EnumStateFromString(string stateName) { return stateNameToStateLookup[stateName]; }
        
        #endregion
    
    }
    
}
