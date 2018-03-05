using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationView : MonoBehaviour
{
	int attackerWinsBoth = 0;
	int defenderWinsBoth = 0;
	int bothSidesLoseOne = 0;
	int attacksMade = 0;

	[SerializeField]
	public int iterationsPerFrame = 10000000;

	[SerializeField]
	public int maximumIterations = 2000000;

	bool simulationRunning = true;

	[SerializeField]
	public Text outputText;
	
	public void BeginSimulation()
	{
		simulationRunning = !simulationRunning;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!simulationRunning || attacksMade >= maximumIterations)
		{
			return;
		}

		List<int> attackD6 = new List<int>();
		List<int> defenseD6 = new List<int>();
		for (int i = 0; i < iterationsPerFrame; ++i)
		{
			attackD6 = new List<int>();
			attackD6.Add(Random.Range(1, 12+ 1));
			attackD6.Sort(HighToLowSort);

			defenseD6 = new List<int>();
			defenseD6.Add(Random.Range(1, 12 + 1));
			defenseD6.Sort(HighToLowSort);

			//Highest dice
			if (attackD6[0] > defenseD6[0] 
				//&& attackD6[1] > defenseD6[1]
				)
			{
				attackerWinsBoth++;
			}
			else if (attackD6[0] <= defenseD6[0]
				//&& attackD6[1] <= defenseD6[1]
				)
			{
				defenderWinsBoth++;
			}
			else
			{
				bothSidesLoseOne++;
			}
			
			attacksMade++;
		}

		string outputString = "******************************************\n" +
			attackD6.Count + "v" + defenseD6.Count + " Attack X Defense X \n";
		outputString += "Attacks Made: " + attacksMade.ToString("N0") + "\n";
		outputString += "Attacker Wins All: " + (attackerWinsBoth * 100.0f / attacksMade).ToString("00.##") + "%\n";
		outputString += "Defender Wins All: " + (defenderWinsBoth * 100.0f / attacksMade).ToString("00.##") + "%\n";
		if (bothSidesLoseOne > 0)
		{
			outputString += "Each Loses One: " + (bothSidesLoseOne * 100.0f / attacksMade).ToString("00.##") + "%\n";
		}

		outputString += "\nAttack Win to Defender Win Ratio: " + ((float)attackerWinsBoth / defenderWinsBoth).ToString("00.##")+ "\n";

		outputString += "******************************************\n";

		outputText.text = outputString;
	}

	public int HighToLowSort(int x, int y)
	{
		if (x > y)
		{
			return -1;
		}
		else if (y > x)
		{
			return 1;
		}

		return 0;
	}
}
