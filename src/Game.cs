using System;
using System.Collections;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;
	public Item item;
	public Inventory inventory;


	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player();
		CreateRooms();
	}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room attic = new Room("in a dusty, old attic. Various boxes are covered in dust and cobwebs.\nYou notice a sizeable hole in the floor");
		Room eastwing_2_f1 = new Room("in a hallway with a Victorian Era-esque interior");
		Room eastwing_1_f1 = new Room("walking down the hall");
		Room centralhall_1_f1 = new Room("inside the main hall, various paintings cover the wall");
		Room centralhall_2_f1 = new Room("walking down the hall and notice a flight of stairs");
		Room centralhall_1_ground = new Room("on the ground floor now after walking down the stairs");

		attic.AddExit("down", eastwing_2_f1);
		eastwing_2_f1.AddExit("south", eastwing_1_f1);
		eastwing_1_f1.AddExit("north", eastwing_2_f1);
		eastwing_1_f1.AddExit("west", centralhall_1_f1);
		centralhall_1_f1.AddExit("east", eastwing_1_f1);
		centralhall_1_f1.AddExit("north", centralhall_2_f1);
		centralhall_2_f1.AddExit("south", centralhall_1_f1);
		centralhall_2_f1.AddExit("down", centralhall_1_ground);

		// Create your Items here

		Item Evil_Ring = new Item(1, "inconspicuous_ring");
		// And add them to the Rooms
		// ...
		attic.Chest.Put("inconspicuous_ring", Evil_Ring);

		// Start game outside
		player.CurrentRoom = attic;
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished)
		{
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);

			if (!player.IsAlive())
			{
				finished = true;
				Console.WriteLine("You died, how sad.");
			}
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if (command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
			case "look":
				LookCommand();
				Console.WriteLine();
				Console.WriteLine("These items are currently in the room:");
				player.CurrentRoom.Chest.Show();
				break;
			case "status":
				StatusHealth();
				break;
			case "drop":
				Drop(command);
				break;
			case "take":
				Take(command);
				break;
			case "use":
				UseItem(command);
				break;
		}

		return wantToQuit;
	}

	// ######################################
	// implementations of user commands:
	// ######################################

	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if (!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to " + direction + "!");
			return;
		}
		player.Damage(20);
		player.CurrentRoom = nextRoom;
		LookCommand();
	}
	private void LookCommand()
	{
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}
	private void StatusHealth()
	{
		if (player.health <= 50)
		{
			Console.WriteLine("You have " + player.health + " HP left");
			Console.WriteLine("I suggest finding some way to heal.");
		}
		else
		{
			Console.WriteLine("You have " + player.health + " HP left");
		}
		Console.WriteLine();
		Console.WriteLine("Inventory:");
		player.ShowInventory();
	}
	private void Drop(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Drop what?");
			return;
		}
		else
		{
			string itemName = command.SecondWord;
			player.DropToChest(itemName);

		}
	}
	private void Take(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Take what?");
			return;
		}
		else
		{
			string itemName = command.SecondWord;
			player.TakeFromChest(itemName);
		}
	}
	public void UseItem(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Use what?"); 
			return;
		}
		string itemName = command.SecondWord;

		Item item = player.Backpack.Get(itemName); 
		
		if (item != null)
		{
			player.Use(itemName, command);
		}
		else
		{
			Console.WriteLine("You don't have a " + itemName + ".");
		}
	}
}


