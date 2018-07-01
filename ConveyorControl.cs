public Program()

{

}



public void Save()

{

}



public void Main(string argument, UpdateType updateSource)

{

 const string name = "Conveyor Sorter Refinery";

  var block = GridTerminalSystem.GetBlockWithName(name) as IMyConveyorSorter;
  
  var itemFilters = new List<MyInventoryItemFilter>();
  
  itemFilters.Add(new MyInventoryItemFilter("MyObjectBuilder_Component/SteelPlate"));

  block.SetFilter(MyConveyorSorterMode.Whitelist, itemFilters);

}
