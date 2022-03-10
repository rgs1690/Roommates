using Roommates.Models;
using Roommates.Repositories;
using System;
using System.Collections.Generic;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Roommates;Integrated Security=True";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);
            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();
                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAll();
                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for chore"):
                        Console.Write("Chore Id: ");
                        int choreId = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetById(choreId);

                        Console.WriteLine($"{chore.Id} - {chore.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a chore"):
                        Console.Write("Chore name: ");
                        string choreName = Console.ReadLine();

                        Chore choreToAdd = new Chore()
                        {
                            Name = choreName,
                        };

                        choreRepo.Insert(choreToAdd);

                        Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search for roommate"):
                        Console.Write("Roommate Id: ");
                        int roomieId = int.Parse(Console.ReadLine());

                        Roommate roomie = roommateRepo.GetById(roomieId);

                        Console.WriteLine($"{roomie.Id} - {roomie.FirstName} {roomie.LastName} moved into {roomie.Room.Name} on {roomie.MovedInDate.ToShortDateString()} and pays ${roomie.RentPortion} towards rent.");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Assign chore to roommate"):
                        List<Chore> allChores = choreRepo.GetAllChoresForAssignment();
                        foreach (Chore c in allChores)
                        {
                            Console.WriteLine($"{c.Id}. {c.Name}");
                        }
                        Console.Write("Chore ID: ");
                        int choreToAssign = int.Parse(Console.ReadLine());

                        List<Roommate> allRoomies = roommateRepo.GetAll();
                        foreach (Roommate rm in allRoomies)
                        {
                            Console.WriteLine($"{rm.Id}. {rm.FirstName} {rm.LastName}");
                        }
                        Console.Write("Roommate ID:");
                        int rmToAssign = int.Parse(Console.ReadLine());

                        if (choreRepo.AssignChore(choreToAssign, rmToAssign))
                        {
                            Console.WriteLine("The chore was successfully assigned!");
                        }
                        else
                        {
                            Console.WriteLine("There was a problem while assigning this chore.");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;


                    case ("Update a room"):
                        List<Room> roomOptions = roomRepo.GetAll();
                        foreach (Room r in roomOptions)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }

                        Console.Write("Which room would you like to update? ");
                        int selectedRoomId = int.Parse(Console.ReadLine());
                        Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

                        Console.Write("New Name: ");
                        selectedRoom.Name = Console.ReadLine();

                        Console.Write("New Max Occupancy: ");
                        selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

                        roomRepo.Update(selectedRoom);

                        Console.WriteLine("Room has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Delete a room"):
                        List<Room> roomsSelections = roomRepo.GetAll();
                        foreach (Room r in roomsSelections)
                        {
                            Console.WriteLine($" {r.Id} - {r.Name} ");
                        }
                        Console.Write("Which room would you like to update? ");
                        int roomToDeleteId = int.Parse(Console.ReadLine());
                        Room roomToDelete = roomsSelections.FirstOrDefault(r => r.Id == roomToDeleteId);

                        roomRepo.Delete(roomToDeleteId);
                        Console.WriteLine($"The room {roomToDelete.Name} has been deleted");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();

                        break;

                    case ("Update a chore"):
                        List<Chore> choreOptions = choreRepo.GetAll();
                        foreach (Chore c in choreOptions)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name} Roommate Id {c.RoommateId}");
                        }

                        Console.Write("Which room would you like to update? ");
                        int selectedChoreId = int.Parse(Console.ReadLine());
                        Chore selectedChore = choreOptions.FirstOrDefault(c => c.Id == selectedChoreId);

                        Console.Write("New Name: ");
                        selectedChore.Name = Console.ReadLine();

                        choreRepo.UpdateChore(selectedChore);

                        Console.WriteLine("Chore has been successfully updated");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Delete a chore"):
                        List<Chore> choreSelections = choreRepo.GetAll();
                        foreach (Chore c in choreSelections)
                        {
                            Console.WriteLine($" {c.Id} - {c.Name} ");
                        }
                        Console.Write("Which chore would you like to update? ");
                        int choreToDeleteId = int.Parse(Console.ReadLine());
                        Chore choreToDelete = choreSelections.FirstOrDefault(c => c.Id == choreToDeleteId);

                        choreRepo.DeleteChore(choreToDeleteId);
                        Console.WriteLine($"The chore {choreToDelete.Name} has been deleted");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();

                        break;
                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }

        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Show all chores",
                "Search for chore",
                "Add a chore",
                "Search for roommate",
                "Show all unassigned chores",
                "Assign chore to roommate",
                "Update a room",
                "Delete a room",
                "Update a chore",
                "Delete a chore",

                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}