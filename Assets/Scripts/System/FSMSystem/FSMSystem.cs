using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FSMSystem class represents the Finite State Machine class.
///  It has a List with the States the NPC has and methods to add,
///  delete a state, and to change the current state the Machine is on.
/// </summary>
public class FSMSystem
{
    private List<FSMState> states;

    // The only way one can change the state of the FSM is by performing a transition
    // Don't change the CurrentState directly
    private StateID currentStateID;
    public StateID CurrentStateID { get { return currentStateID; } }
    private FSMState currentState;
    public FSMState CurrentState { get { return currentState; } }

    private bool isEnterState = false;

    public FSMSystem()
    {
        states = new List<FSMState>();
    }

    public void SetCurrentState(FSMState s)
    {
        foreach(FSMState state in states)
        {
            if(s == state)
            {
                currentState = s;
                currentStateID = s.ID;
                s.DoBeforeEntering();
                isEnterState = true;
                return;
            }
        }
        Debug.LogError("FSM ERROR: Impossible to set current state because no " + s.ID.ToString() + " state in FSM, please add first");
    }

    public void SetCurrentState(StateID stateID)
    {
        foreach (FSMState state in states)
        {
            if (stateID == state.ID)
            {
                currentState = state;
                currentStateID = stateID;
                state.DoBeforeEntering();
                isEnterState = true;
                return;
            }
        }
        Debug.LogError("FSM ERROR: Impossible to set current state because no " + stateID.ToString() + " state in FSM, please add first");
    }

    /// <summary>
    /// This method places new states inside the FSM,
    /// or prints an ERROR message if the state was already inside the List.
    /// First state added is also the initial state.
    /// </summary>
    public void AddState(FSMState s)
    {
        // Check for Null reference before deleting
        if (s == null)
        {
            Debug.LogError("FSM ERROR: Null reference is not allowed");
            return;
        }
        s.FSM = this;
        // First State inserted is also the Initial state,
        //   the state the machine is in when the simulation begins
        if (states.Count == 0)
        {
            states.Add(s);
            return;
        }

        // Add the state to the List if it's not inside it
        foreach (FSMState state in states)
        {
            if (state.ID == s.ID)
            {
                Debug.LogError("FSM ERROR: Impossible to add state " + s.ID.ToString() +
                               " because state has already been added");
                return;
            }
        }
        states.Add(s);
    }

    /// <summary>
    /// This method delete a state from the FSM List if it exists, 
    ///   or prints an ERROR message if the state was not on the List.
    /// </summary>
    public void DeleteState(StateID id)
    {
        // Check for NullState before deleting
        if (id == StateID.NullStateID)
        {
            Debug.LogError("FSM ERROR: NullStateID is not allowed for a real state");
            return;
        }

        // Search the List and delete the state if it's inside it
        foreach (FSMState state in states)
        {
            if (state.ID == id)
            {
                states.Remove(state);
                return;
            }
        }
        Debug.LogError("FSM ERROR: Impossible to delete state " + id.ToString() +
                       ". It was not on the list of states");
    }

    /// <summary>
    /// This method tries to change the state the FSM is in based on
    /// the current state and the transition passed. If current state
    ///  doesn't have a target state for the transition passed, 
    /// an ERROR message is printed.
    /// </summary>
    public void PerformTransition(Transition trans)
    {
        // Check for NullTransition before changing the current state
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("FSM ERROR: NullTransition is not allowed for a real transition");
            return;
        }

        // Check if the currentState has the transition passed as argument
        StateID id = currentState.GetOutputState(trans);
        if (id == StateID.NullStateID)
        {
            Debug.LogError("FSM ERROR: State " + currentStateID.ToString() + " does not have a target state " +
                           " for transition " + trans.ToString());
            return;
        }

        isEnterState = false;

        // Update the currentStateID and currentState		
        currentStateID = id;
        foreach (FSMState state in states)
        {
            if (state.ID == currentStateID)
            {
                
                // Do the post processing of the state before setting the new one
                currentState.DoBeforeLeaving();

                currentState = state;

                // Reset the state to its desired condition before it can reason or act
                currentState.DoBeforeEntering();

                
                break;
            }
        }

        isEnterState = true;

    } // PerformTransition()

    public void Update()
    {
        if (currentState == null || !isEnterState) return;
        currentState.Act();
    }

} //class FSMSystem