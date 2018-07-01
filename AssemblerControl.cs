public Program()

{

}



public void Save()

{

}



public void Main(string argument, UpdateType updateSource)

{
    // Assembler Control:
    var chest = GridTerminalSystem.GetBlockWithName("Large Cargo Container Ref");

            Dictionary<String,float> consolidated = new Dictionary<String,float>();
            var containerInventory = chest.GetInventory(0);
            var containerItems = containerInventory.GetItems();
            for(int j = containerItems.Count - 1; j >= 0; j--)
            {    
                String itemName = containerItems[j].Content.SubtypeName;
                float amount = (float)containerItems[j].Amount;
                if (!consolidated.ContainsKey(itemName)) {
                   consolidated.Add(itemName, amount);
                } else {
                   consolidated[itemName] += amount;
                }
                Echo(itemName + ": " + amount);

                var assembler = GridTerminalSystem.GetBlockWithName("Assembler "+itemName);
                if (assembler != null) {
                    var prodAmount = 2000;
                    switch (itemName) {
                        case "SteelPlate": prodAmount = 20000; break;
                        case "Construction": prodAmount = 20000; break;
                        case "Computer": prodAmount = 20000; break;
                        case "SmallTube": prodAmount = 20000; break;
                    }
                    if ( amount > prodAmount) {
                        assembler.ApplyAction ("OnOff_Off");
                    } else {
                        assembler.ApplyAction ("OnOff_On");
                    }
                } else { 
                    Echo("No Assembler for: " + itemName);
                }
            } 
}
