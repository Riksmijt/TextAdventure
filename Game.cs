using System;

namespace ZuulCS
{
	public class Game
	{
		private Parser parser;
		//private Room currentRoom;
        private Player player;
       // private Inventory inv;
       

		public Game ()
		{
           // inv = new Inventory();
			parser = new Parser();
            player = new Player();
            createRooms();
        }

		private void createRooms()
		{
			Room outside, theatre, pub, lab, office,basement,upstairs;

			// create the rooms
			outside = new Room("outside the main entrance of the university");
			theatre = new Room("in a lecture theatre");
			pub = new Room("in the campus pub");
			lab = new Room("in a computing lab");
			office = new Room("in the computing admin office");
            basement = new Room("in the basement");
            upstairs = new Room("upstairs");
			// initialise room exits
			outside.setExit("east", theatre);
			outside.setExit("south", lab);
			outside.setExit("west", pub);

			theatre.setExit("west", outside);
            theatre.Items.Add(new InventoryItem("apple", 1, "perhaps this could heal you",false,true));
            theatre.Items.Add(new InventoryItem("sniper", 20, "one shot one kill", false,false));
            
			pub.setExit("east", outside);
            pub.Items.Add(new InventoryItem("acid", 2, "are you sure you can grab acid", true,false));

			lab.setExit("north", outside);
			lab.setExit("east", office);
            lab.setExit("down", basement);
            lab.setExit("up", upstairs);

			office.setExit("west", lab);

            basement.setExit("up", lab);
            upstairs.setExit("down", lab);

			player.CurrentRoom = outside;  // start game outside
		}


		/**
	     *  Main play routine.  Loops until end of play.
	     */
		public void play()
		{
			printWelcome();

			// Enter the main command loop.  Here we repeatedly read commands and
			// execute them until the game is over.
			bool finished = false;
			while (! finished) {
				
                if (player.isAlive() == true)
                {
                    Command command = parser.getCommand();
                    finished = processCommand(command);

                }
                else {
                    finished = true;
                }
               
            }
			Console.WriteLine("Thank you for playing.");
            Console.WriteLine("press enter to quit");
            Console.Read();
        }

		/**
	     * Print out the opening message for the player.
	     */
		private void printWelcome()
		{
			Console.WriteLine();
			Console.WriteLine("Welcome to Zuul!");
			Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
			Console.WriteLine("Type 'help' if you need help.");
			Console.WriteLine();
           
			Console.WriteLine(player.CurrentRoom.getLongDescription());
            
		}
        public InventoryItem GetFirstCurrentRoomItem(string name)
        {
           
            foreach(InventoryItem item in player.CurrentRoom.Items)
            {
                if(item.name == name)
                {
                    return item;
                }
            }
            return null;
        }
       
        public InventoryItem GetFirstPlayerItem(string name)
        {
            foreach (InventoryItem item in player.PlayerItems)
            {
                if (item.name == name)
                {
                    return item;
                }
            }
            return null;
        }
       public InventoryItem usable(string name)
        {
            foreach(InventoryItem item in player.PlayerItems)
            {
                if(item.name == name && item.healing == true)
                {
                    return item;
                }
            }
            return null;
        }
        

		/**
	     * Given a command, process (that is: execute) the command.
	     * If this command ends the game, true is returned, otherwise false is
	     * returned.
	     */
		private bool processCommand(Command command)
		{
			bool wantToQuit = false;

			if(command.isUnknown()) {
				Console.WriteLine("I don't know what you mean...");
				return false;
			}

            if (player.isAlive() == true)
            {
                //inv.lookAtItmes();
                string commandWord = command.getCommandWord();
                switch (commandWord)
                {
                    case "help":
                        printHelp();
                        break;
                    case "go":
                        goRoom(command);
                        
                        break;
                    case "quit":
                        wantToQuit = true;
                        break;
                    case "look":
                        Console.WriteLine(player.CurrentRoom.getLongDescription());
                        break;
                    case "take":
                        Console.WriteLine(command.getSecondWord());
                        player.take(GetFirstCurrentRoomItem(command.getSecondWord()));
                        
                        break;
                    case "drop":
                        player.drop(GetFirstPlayerItem(command.getSecondWord()));
                        break;
                    case "inventory":

                        Console.WriteLine(player.showInv());
                        break;
                    case "use":
                        player.use(usable(command.getSecondWord()));
                        break;
                }
            }
            else
            {
                Console.WriteLine("you are dead");
                
                wantToQuit = true;
             }


            return wantToQuit;
        }

		// implementations of user commands:

		/**
	     * Print out some help information.
	     * Here we print some stupid, cryptic message and a list of the
	     * command words.
	     */
		private void printHelp()
		{
			Console.WriteLine("You are lost. You are alone.");
			Console.WriteLine("You wander around at the university.");
			Console.WriteLine();
			Console.WriteLine("Your command words are:");
			parser.showCommands();
		}

		/**
	     * Try to go to one direction. If there is an exit, enter the new
	     * room, otherwise print an error message.
	     */
		private void goRoom(Command command)
		{
			if(!command.hasSecondWord()) {
				// if there is no second word, we don't know where to go...
				Console.WriteLine("Go where?");
				return;
			}

			string direction = command.getSecondWord();

			// Try to leave current room.
			Room nextRoom = player.CurrentRoom.getExit(direction);

			if (nextRoom == null) {
				Console.WriteLine("There is no door to "+direction+"!");
			} else {
              
                player.CurrentRoom = nextRoom;
                player.damage(5);
                foreach(InventoryItem item in player.CurrentRoom.Items)
                {
                    Console.WriteLine(item.toString());
                    
                }

                Console.WriteLine(player.CurrentRoom.getLongDescription());
			}
		}

	}
}
