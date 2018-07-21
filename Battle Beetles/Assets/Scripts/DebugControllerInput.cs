using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControllerInput : MonoBehaviour
{
	[Header("Test with this number of players")]
	public int numOfPlayers = 1;

	[Header("Buttons")]
	public bool faceButtons = false;
	public bool bumpers = false;
	public bool startAndSelect = false;
	public bool analogIn = false;

	[Header("Axes, may 02 have mercy on your soul")]
	public bool leftStick = false;
	public bool rightStick = false;
	public bool triggers = false;
	public bool dpad = false;

	[Header("Display axis values? (very messy)")]
	public bool displayAxisValues = false;

	[Header("Axis accuracy")]
	[Range(0.0f, 1.0f)]
	public float axisBounds = 0.5f;

	private bool threwError = false;

	void Start()
	{
		// check for any errors
		if (CheckForErrors() == false)
		{
			// debug a warning so that debug messages can be turned off for builds or convenience
			DebugWarning();
		}
	}

	void FixedUpdate ()
	{
		// run only if we didn't throw an error
		if (!threwError)
		{
			DoButtons();
			DoAxes();
		}
	}

	private void DoButtons()
	{
		// testing A, B, X, and Y
		if(faceButtons)
		{
			TestButtonInput("A", "A");
			TestButtonInput("B", "B");
			TestButtonInput("X", "X");
			TestButtonInput("Y", "Y");
		}

		// testing both bumpers
		if (bumpers)
		{
			TestButtonInput("BumperLeft", "Left Bumper");
			TestButtonInput("BumperRight", "Right Bumper");
		}

		// testing start and select
		if (startAndSelect)
		{
			TestButtonInput("Start", "Start");
			TestButtonInput("Select", "Select");
		}

		// testing the analog sticks pressed in
		if (analogIn)
		{
			TestButtonInput("AnalogInRight", "Right Analog Stick In");
			TestButtonInput("AnalogInLeft", "Left Analog Stick In");
		}
	}

	private void DoAxes()
	{
		// test left sticks
		if(leftStick)
		{
			TestAxisInput("HorizontalLeft", "Horizontal Left");
			TestAxisInput("VerticalLeft", "Vertical Left");
		}

		// test right sticks
		if (rightStick)
		{
			TestAxisInput("HorizontalRight", "Horizontal Right");
			TestAxisInput("VerticalRight", "Vertical Right");
		}

		// test both triggers
		if (triggers)
		{
			TestAxisInput("TriggerRight", "Right Trigger");
			TestAxisInput("TriggerLeft", "Left Trigger");
		}

		// test the dpad
		if (dpad)
		{
			TestAxisInput("HorizontalDpad", "Horizontal Dpad");
			TestAxisInput("VerticalDpad", "Vertical Dpad");
		}
	}

	private void TestAxisInput(string axisName, string displayName)
	{
		string tmpName;

		// for the number of players
		for (int i = 1; i < numOfPlayers; ++i)
		{
			// for 1 player, add nothing to the end of the input's name
			if (i == 1)
			{
				tmpName = axisName;	
			}
			// otherwise, add the number
			else
			{
				tmpName = axisName + i.ToString();
			}

			// if the axis is outside the bounds, display it
			if (Input.GetAxis(tmpName) >= axisBounds || Input.GetAxis(tmpName) <= -1 * axisBounds)
			{
				Debug.Log(AxisOutput(i, displayName, tmpName));
			}
		}
	}

	private void TestButtonInput(string buttonName, string displayName)
	{
		string tmpName;

		// for the number of players
		for (int i = 1; i < numOfPlayers; ++i)
		{
			// for 1 player, add nothing to the end of the input's name
			if (i == 1)
			{
				tmpName = buttonName;	
			}
			// otherwise, add the number
			else
			{
				tmpName = buttonName + i.ToString();
			}

			// display it
			if (Input.GetButtonDown(tmpName))
			{
				Debug.Log(ButtonOutput(i, displayName));
			}
		}
	}

	private string ButtonOutput(int player, string buttonName)
	{
		// return string message regarding the button that was pressed
		return "Player " + player + " pressed " + buttonName;
	}

	private string AxisOutput(int player, string displayName, string axisName)
	{
		// return string message regarding the axis that was pressed
		if (displayAxisValues)
		{
			return "Player " + player + "'s axis, " + displayName + ", equals " + Input.GetAxis(axisName);
		}
		else
		{
			return "Player " + player + "'s axis, " + displayName + ", is being pressed";
		}
	}

	private void DebugWarning()
	{
		string tmpWarning = "", warningIntro = "Debug Controller Input script is testing the following:\n";

		// add inputs that are set to true
		if (faceButtons)
			tmpWarning += "Face Buttons, ";
		if (bumpers)
			tmpWarning += "Bumpers, ";
		if (startAndSelect)
			tmpWarning += "Start and Select, ";
		if (analogIn)
			tmpWarning += "Analog In, ";
		if (leftStick)
			tmpWarning += "Left Stick, ";
		if (rightStick)
			tmpWarning += "Right Stick, ";
		if (triggers)
			tmpWarning += "Triggers, ";
		if (dpad)
			tmpWarning += "D-Pad, ";

		// if something was true, debug a warning
		if (tmpWarning != "")
		{
			Debug.LogWarning(warningIntro + tmpWarning);
		}
	}

	private bool CheckForErrors()
	{
		// number of players needs to be greater than or equal to 1 for the debugging to run
		if (numOfPlayers < 1)
		{
			Debug.LogError("Number of players must be greater than 1. Stopping input debug.");
			threwError = true;
			return true;
		}

		// all is well
		return false;
	}
}