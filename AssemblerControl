public Program()

{

}



public void Save()

{

}



public void Main(string argument, UpdateType updateSource)

{

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

                var heavy = 1;
                if (itemName == "SteelPlate" || itemName == "Construction" || itemName == "Computer" || itemName == "SmallTube") {
                    heavy = 0;
                }

                var assembler = GridTerminalSystem.GetBlockWithName("Assembler "+itemName);
                if (assembler != null) {
                    if (  heavy == 0 && amount > 6000) {
                        assembler.ApplyAction ("OnOff_Off");
                    } else if ( heavy == 1 && amount > 2000) {
                        assembler.ApplyAction ("OnOff_Off");
                    } else {
                        assembler.ApplyAction ("OnOff_On");
                    }
                }
            } 
}

