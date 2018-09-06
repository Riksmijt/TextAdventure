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
			Room outside, theatre, pub, lab, office, basement, upstairs;

			// create the rooms
			outside = new Room("outside the main entrance of the university",false);
			theatre = new Room("in a lecture theatre",false);
			pub = new Room("in the campus pub",true);
			lab = new Room("in a computing lab",false);
			office = new Room("in the computing admin office",false);
            basement = new Room("in the basement",false);
            upstairs = new Room("upstairs",false);
			// initialise room exits
			outside.setExit("east", theatre);
			outside.setExit("south", lab);
			outside.setExit("west", pub);
            

			theatre.setExit("west", outside);
            theatre.RoomInventory.addItem(new Apple());
            theatre.RoomInventory.addItem(new Apple());
            

            
            pub.setExit("east", outside);
            pub.RoomInventory.addItem(new Apple());

			lab.setExit("north", outside);
			lab.setExit("east", office);
            lab.setExit("down", basement);
            lab.setExit("up", upstairs);
            

			office.setExit("west", lab);

            upstairs.RoomInventory.addItem(new AWP());
            basement.RoomInventory.addItem(new Key());
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

       
       
      /* public InventoryItem usable(string name)
        {
            foreach(InventoryItem item in player.PlayerItems)
            {
                if(item.name == name && item.healing == true)
                {
                    return item;
                }
            }
            return null;
        }*/
        

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
                        player.CurrentRoom.RoomInventory.GetItemsRoom();
                        break;
                    case "take":
                        player.CurrentRoom.RoomInventory.Take(player.PlayerInventory, command.getSecondWord());
                        break;
                    case "drop":
                        player.PlayerInventory.Drop(player.CurrentRoom.RoomInventory, command.getSecondWord());
                        break;
                    case "inventory":
                        foreach (Item item in player.PlayerInventory.Items)
                        {
                            Console.WriteLine(item.name);
                        }
                        break;
                    case "use":
                        useItem(command);
                        //Item itemToUse = player.PlayerInventory.GetFirstPlayerItem(command.getSecondWord());
                        //itemToUse.use(player);


                       // Item keyToUse = player.PlayerInventory.GetKey(command.getThirdWord());
                       // keyToUse.use(player);


                       // player.PlayerInventory.Drop(itemToUse);
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

		/**--
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
            if (!command.hasSecondWord())
            {
                // if there is no second word, we don't know where to go...
                Console.WriteLine("Go where?");
                return;
            }

            string direction = command.getSecondWord();

            // Try to leave current room.
            Room nextRoom = player.CurrentRoom.getExit(direction);
            if (nextRoom == null)
            {
                Console.WriteLine("There is no door to " + direction + "!");
            }
            else if (nextRoom.locked)
            {
                Console.WriteLine("this room is locked");
            }                
            else
            {

                player.CurrentRoom = nextRoom;
                player.damage(5);
                player.CurrentRoom.RoomInventory.GetItemsRoom();

                Console.WriteLine(player.CurrentRoom.getLongDescription());
            }
            
        }

        public void useItem(Command command)
        {

            Item i = null;

            if (command.hasSecondWord())
            {
                for (int y = player.PlayerInventory.Items.Count - 1; y >= 0; y--)
                {
                    if (command.getSecondWord() == player.PlayerInventory.Items[y].name)
                    {
                        i = player.PlayerInventory.Items[y];
                    }
                }
            }
            if (command.hasThirdWord())
            {
                Room unlockableRoom = player.CurrentRoom.getExit(command.getThirdWord());
                if (unlockableRoom == null)
                {
                    Console.WriteLine("this room might be already unlocked or it doesn't exist. Feels bad man");
                }
                else
                {
                    if (i == null)
                    {
                        Console.WriteLine("I don't think you have that item");
                        return;
                    }
                    else
                    {
                        i.use(unlockableRoom);
                        return;
                    }
                }
            }
        
            else
            {
                Console.WriteLine("I think you don't have that item. Can't help you sorry!");
            }
        }


    }
}
