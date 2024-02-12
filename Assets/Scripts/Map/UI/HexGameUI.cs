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
	HexUnit selectedUnit;
	Fleet selectedFleet;
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
		}
		else
		{
			Shader.DisableKeyword("_HEX_MAP_EDIT_MODE");
		}
	}

	void Update()
	{

        if (!EventSystem.current.IsPointerOverGameObject())
		{
			//Debug.Log(EventSystem.current.IsPointerOverGameObject());
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
	/// The Current Function to select a fleet or an empty cell
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


			selectedFleet = grid.GetCell(currentCellIndex).fleet;

			if (selectedFleet != null && selectedFleet.owner == Fleet.FleetOwner.PLAYER)
			{
                selectedUnit = selectedFleet.hexUnit;
				selectedFleet.OpenUI();
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
                    grid.FindPath(
                        selectedUnit.Location,
                        grid.GetCell(currentCellIndex),
                        selectedUnit);

                    selectedUnit.Travel(grid.GetPath());
					selectedFleet.RemoveActionPoints(100);
                    grid.ClearPath();
                    return;

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
