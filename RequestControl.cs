/*
 * Expected format of IMyTerminalBlock.CustomData:
 * 
 * ItemSubType;TargetAmount
 * ItemSubType;TargetAmount
 * ...
 * 
 * Change SourceBlockName to name of source container.
 */

private const string SourceBlockName = "Large Cargo Container Ref";
private const bool DebugMode = false;

public Program()
{
}

public void Save()
{

}

public void Main(string argument, UpdateType updateSource)
{
    var blocks = new List<IMyTerminalBlock>();
    GridTerminalSystem.GetBlocksOfType<IMyTerminalBlock>(blocks, block => !String.IsNullOrEmpty(block.CustomData));

    var sourceBlock = GridTerminalSystem.GetBlockWithName(SourceBlockName);

    foreach (var block in blocks)
    {
      Log(block.CustomName + " has custom data: " + block.CustomData);

      string[] lines = block.CustomData.Split('\n');
      foreach (string line in lines)
      {
        string[] columns = line.Split(';');
        if (columns.Length != 2)
        {
          continue;
        }

        string itemSubType = columns[0].Trim();
        VRage.MyFixedPoint targetAmount = (VRage.MyFixedPoint) Convert.ToDecimal(columns[1].Trim());

        Log(block.CustomName + " wants " + targetAmount + " of " + itemSubType);

        TransferItems(sourceBlock, block, itemSubType, targetAmount);
      }
    }
}

public void TransferItems(IMyTerminalBlock sourceBlock, IMyTerminalBlock targetBlock, string itemSubType, VRage.MyFixedPoint targetAmount)
{
  if (targetBlock.GetInventory(0) == null || targetBlock.GetInventory(0).IsFull)
  {
    Log(targetBlock.CustomName + " has no inventory 0 or inventory is full");
    return;
  }

  var sourceInventory = sourceBlock.GetInventory(0);
  if (sourceInventory == null)
  {
    Log(sourceBlock.CustomName + " has no inventory");
    return;
  }

  var sourceItems = sourceInventory.GetItems();
  int sourceItemIndex = -1;
  for (int i = 0; i < sourceItems.Count; i++)
  {
    if (sourceItems[i].Content.SubtypeName == itemSubType)
    {
      sourceItemIndex = i;
      Log(itemSubType + " was found at index " + sourceItemIndex + " of " + sourceBlock.CustomName);

      break;
    }
  }

  if (sourceItemIndex == -1)
  {
    Log(sourceBlock.CustomName + " has no " + itemSubType + " in its inventory");
    return;
  }

  var targetInventory = targetBlock.GetInventory(0);
  if (targetInventory == null)
  {
    Log(targetBlock.CustomName + " has no inventory");
    return;
  }

  var currentAmount = (VRage.MyFixedPoint) 0;
  var targetBlockItems = targetInventory.GetItems();
  foreach (var item in targetBlockItems)
  {
    if (item.Content.SubtypeName == itemSubType)
    {
      currentAmount += item.Amount;
    }
  }

  Log(targetBlock.CustomName + " has " + currentAmount + " of " + itemSubType);

  var transferAmount = targetAmount - currentAmount;

  if (transferAmount > (VRage.MyFixedPoint) 0)
  {
    Log("Transfering " + transferAmount + " of " + itemSubType + " from " + sourceBlock.CustomName + " to " + targetBlock.CustomName);
    sourceInventory.TransferItemTo(targetInventory, sourceItemIndex, null, null, transferAmount);
  }
}

public void Log(object message)
{
  if (DebugMode)
  {
    Echo(message.ToString());
  }
}
