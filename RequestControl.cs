public Program()

{

}



public void Save()

{

}



public void Main(string argument, UpdateType updateSource)

{

    var  srcName = "Large Cargo Container Ref";
    var  destName = "Shipyard: Connector";
    var itemName = "Computer";
    var amount = 1;
    var containerIndex = 0;

    var srcBlock = GridTerminalSystem.GetBlockWithName(srcName);
    var destBlock = GridTerminalSystem.GetBlockWithName(destName);

    var srcInventory = srcBlock.GetInventory(0);
    var destInventory = destBlock.GetInventory(0);

    var containerItems = srcInventory.GetItems();
    for(int j = containerItems.Count - 1; j >= 0; j--)
    { 
        String containerItemName = containerItems[j].Content.SubtypeName;
        if (containerItemName == itemName) {
            containerIndex = j;
        }
    }

    srcInventory.TransferItemTo(destInventory, containerIndex, null, true, amount);
}
