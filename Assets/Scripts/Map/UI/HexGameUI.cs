using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
/// <summary>
/// Component that manages the game UI.
/// </summary>
public class HexGameUI : MonoBehaviour
{
	[SerializeField]
	HexGrid grid;

	int currentCellIndex = -1;
	int prevCellIndex = -1;
	int prevCombatCellIndex = -1;
	HexUnit selectedUnit;
	Fleet selectedFleet;
	Planet selectedPlanet;
	public GameManager gameManager;

    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// Set whether map edit mode is active.
    /// </summary>
    /// <param name="toggle">Whether edit mode is enabled.</param>
    public void SetEditMode(bool toggle)
	{
		enabled = !toggle;
		grid.ShowUI(!toggle);
		grid.ClearPath();
		if (toggle)
		{
			Shader.EnableKeyword("_HEX_MAP_EDIT_MODE");
			grid.EnableAllRenderer();
		}
		else
		{
			Shader.DisableKeyword("_HEX_MAP_EDIT_MODE");
			grid.DisableAllRenderer();
		}

		
	}

	void Update()
	{

        if (!EventSystem.current.IsPointerOverGameObject())
		{

			if (Input.GetMouseButtonDown(0))
			{
				DoSelection();
			}
			else if (selectedUnit)
			{
				if (Input.GetMouseButtonDown(1))
				{
					DoMove();
				}
				else
				{
					DoPathfinding();
				}
			}
		}
	}

	/// <summary>
	/// The Current Function to select a fleet, a planet, or an empty cell
	/// </summary>
	void DoSelection()
	{
		grid.ClearPath();
		UpdateCurrentCell();
		if(prevCellIndex >= 0)
		{
            grid.GetCell(prevCellIndex).DisableHighlight();
        }
		if (currentCellIndex >= 0)
		{
			Debug.Log(grid.GetCell(currentCellIndex).visibility);

			selectedFleet = grid.GetCell(currentCellIndex).fleet;
            selectedPlanet = grid.GetCell(currentCellIndex).planet;
			//First Select Fleet, then planet, if empty cell, close the current GUI


            if (selectedFleet != null && selectedFleet.owner == Owner.PLAYER)
			{
                //Select Player Fleet
                selectedUnit = selectedFleet.hexUnit;
                FleetInfoUI fui = FindObjectOfType<FleetInfoUI>();
                //If No UI Open, open fleet UI
				if (!fui.isUIOpen)
                {
                    selectedFleet.OpenUI();
                }
                else
                {
                    if (selectedPlanet != null)
                    {
						//If Fleet UI Open, Select Planet
                        selectedPlanet.OpenUI();
					}
					else
					{
                        selectedFleet.OpenUI();
                    }
                }
            }
			else if (selectedFleet != null && selectedFleet.owner != Owner.PLAYER)
			{

				//Select Enemy Fleet
				FleetInfoUI fui = FindObjectOfType<FleetInfoUI>();
				if(!fui.isUIOpen)
				{
					//If No Fleet UI Open, Open Fleet UI
                    selectedFleet.OpenUI();
				}
				else
				{
					if(selectedPlanet != null)
					{
						//If Fleet UI Open, Select Planet
						selectedPlanet.OpenUI();
					}
					else
					{
                        selectedFleet.OpenUI();
                    }
				}

            }
            else if(selectedFleet == null && selectedPlanet != null)
			{
				//If no fleet, open planet UI
				selectedUnit = null;
				selectedPlanet.OpenUI();

            }
			else
			{
				selectedUnit = null;
				
			}
			

			grid.GetCell(currentCellIndex).EnableHighlight(Color.white);
			prevCellIndex = currentCellIndex;
		}
		else
		{

		}

	}
	

	void DoPathfinding()
	{
        if (prevCombatCellIndex != -1 && prevCombatCellIndex != currentCellIndex)
        {
            grid.GetCell(prevCombatCellIndex).DisableHighlight();
        }
        if (UpdateCurrentCell())
		{
			if (currentCellIndex >= 0 &&
				selectedUnit.IsValidDestination(grid.GetCell(currentCellIndex)))
			{
				grid.FindPath(
					selectedUnit.Location,
					grid.GetCell(currentCellIndex),
					selectedUnit);
			}else if(currentCellIndex >= 0 &&
                selectedUnit.IsValidCombat(grid.GetCell(currentCellIndex))) {

                //if (prevCellIndex)
                prevCombatCellIndex = currentCellIndex;
				//if()

                grid.GetCell(prevCombatCellIndex).EnableHighlight(Color.red);
                grid.ClearPath();

            }
			else
			{
				grid.ClearPath();
			}
		}
	}

	void DoMove()
	{
		AudioManager.Instance.PlaySFX("ShipMovement");	
		if (grid.HasPath)
		{

            selectedUnit.Travel(grid.GetPath());
			grid.ClearPath();
		}else if (selectedUnit.IsValidCombat(grid.GetCell(currentCellIndex)))
		{
			//Debug.Log("Yes or No");De
            HexCell destination = grid.GetCell(currentCellIndex);
            if (destination.fleet != null)
            {

                gameManager.StartCombat(selectedFleet, destination.fleet);

                if (destination.fleet == null && selectedFleet != null)
                {
					if (selectedUnit.IsValidDestination(destination))
					{
                        grid.FindPath(
                        selectedUnit.Location,
                        grid.GetCell(currentCellIndex),
                        selectedUnit);

						if (grid.HasPath)
						{
                            selectedUnit.Travel(grid.GetPath());
                            selectedFleet.RemoveActionPoints(100);
                            grid.ClearPath();
                        }
                        return;
                    }

				}
				else if (selectedFleet == null)
                {
                    
                }
                
            }
        }
	}

	bool UpdateCurrentCell()
	{
		HexCell cell = grid.GetCell(
			Camera.main.ScreenPointToRay(Input.mousePosition));
		int index = cell ? cell.Index : -1;
		if (index != currentCellIndex)
		{
			currentCellIndex = index;
			return true;
		}
		return false;
	}
}
