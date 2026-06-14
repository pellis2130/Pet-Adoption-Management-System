/*******************************************************************
* Name: Princess Ellis
* Date: June 7, 2026
* Assignment: SDC320 Week 4 Course Project - Database Implementation
*
* Main application class for the Pet Adoption Management System.
* Demonstrates inheritance, composition, polymorphism, abstract classes,
* constructors, access specifiers, and database storage/retrieval.
*/
using System;
using System.Collections.Generic;
using System.Data.SQLite;

public class Program
{
    public static void Main(string[] args)
    {
        string databaseFile = "PetAdoption.db";
        string connectionString = $"Data Source={databaseFile};Version=3;BusyTimeout=5000;";

        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            conn.Open();

            using (SQLiteCommand pragmaCmd = new SQLiteCommand("PRAGMA journal_mode=WAL; PRAGMA busy_timeout=5000;", conn))
            {
                pragmaCmd.ExecuteNonQuery();
            }

            PetRepository.CreateTables(conn);

            bool running = true;

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        AddPet(conn);
                        break;
                    case "2":
                        ViewAllPets(conn);
                        break;
                    case "3":
                        SearchPet(conn);
                        break;
                    case "4":
                        UpdatePet(conn);
                        break;
                    case "5":
                        DeletePet(conn);
                        break;
                    case "6":
                        DemonstratePolymorphism(conn);
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine("\n===== Pet Adoption Management System =====");
        Console.WriteLine("1. Add Pet");
        Console.WriteLine("2. View All Pets");
        Console.WriteLine("3. Search Pet by ID");
        Console.WriteLine("4. Update Pet");
        Console.WriteLine("5. Delete Pet");
        Console.WriteLine("6. Demonstrate Polymorphism");
        Console.WriteLine("0. Exit");
        Console.Write("Choose an option: ");
    }

    private static void AddPet(SQLiteConnection conn)
    {
        Console.WriteLine("\n--- Add Pet ---");

        Console.Write("Enter pet type (Dog/Cat/Other): ");
        string petType = Console.ReadLine() ?? "";

        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Breed: ");
        string breed = Console.ReadLine() ?? "";

        int age = ReadInt("Age: ");
        bool vaccinated = ReadBool("Vaccinated? (true/false): ");
        bool spayedOrNeutered = ReadBool("Spayed or Neutered? (true/false): ");

        Console.Write("Medical Notes: ");
        string medicalNotes = Console.ReadLine() ?? "";

        Console.Write("Last Vet Visit: ");
        string lastVetVisit = Console.ReadLine() ?? "";

        MedicalRecord medicalRecord = new MedicalRecord(
            vaccinated,
            spayedOrNeutered,
            medicalNotes,
            lastVetVisit
        );

        Pet pet;

        if (petType.ToLower() == "dog")
        {
            Console.Write("Energy Level: ");
            string energyLevel = Console.ReadLine() ?? "";

            bool goodWithKids = ReadBool("Good With Kids? (true/false): ");

            pet = new Dog(
                0,
                name,
                breed,
                age,
                "Available",
                medicalRecord,
                energyLevel,
                goodWithKids
            );
        }
        else if (petType.ToLower() == "cat")
        {
            bool indoorOnly = ReadBool("Indoor Only? (true/false): ");
            bool litterTrained = ReadBool("Litter Trained? (true/false): ");

            pet = new Cat(
                0,
                name,
                breed,
                age,
                "Available",
                medicalRecord,
                indoorOnly,
                litterTrained
            );
        }
        else
        {
            Console.Write("Animal Type: ");
            string animalType = Console.ReadLine() ?? "";

            Console.Write("Special Care Instructions: ");
            string specialCare = Console.ReadLine() ?? "";

            pet = new OtherPet(
                0,
                name,
                animalType,
                breed,
                age,
                "Available",
                medicalRecord,
                specialCare
            );
        }

        PetRepository.AddPet(conn, pet);
        Console.WriteLine("Pet added successfully.");
    }

    private static void ViewAllPets(SQLiteConnection conn)
    {
        Console.WriteLine("\n--- All Pets ---");

        List<Pet> pets = PetRepository.GetAllPets(conn);

        if (pets.Count == 0)
        {
            Console.WriteLine("No pets found.");
            return;
        }

        foreach (Pet pet in pets)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine(pet);
        }
    }

    private static void SearchPet(SQLiteConnection conn)
    {
        int id = ReadInt("Enter pet ID to search: ");

        Pet? pet = PetRepository.SearchPet(conn, id);

        if (pet == null)
        {
            Console.WriteLine("Pet was not found.");
        }
        else
        {
            Console.WriteLine("\n--- Pet Found ---");
            Console.WriteLine(pet);
        }
    }

    private static void UpdatePet(SQLiteConnection conn)
    {
        int id = ReadInt("Enter pet ID to update: ");

        Pet? pet = PetRepository.SearchPet(conn, id);

        if (pet == null)
        {
            Console.WriteLine("Pet was not found.");
            return;
        }

        Console.Write("New name: ");
        string newName = Console.ReadLine() ?? "";
        if (newName != "")
        {
            pet.Name = newName;
        }

        Console.Write("New adoption status: ");
        string newStatus = Console.ReadLine() ?? "";
        if (newStatus != "")
        {
            pet.AdoptionStatus = newStatus;
        }

        Console.Write("New medical notes: ");
        string newMedicalNotes = Console.ReadLine() ?? "";
        if (newMedicalNotes != "")
        {
            pet.MedicalRecord.MedicalNotes = newMedicalNotes;
        }

        PetRepository.UpdatePet(conn, pet);
        Console.WriteLine("Pet updated successfully.");
    }

    private static void DeletePet(SQLiteConnection conn)
    {
        int id = ReadInt("Enter pet ID to delete: ");

        Pet? pet = PetRepository.SearchPet(conn, id);

        if (pet == null)
        {
            Console.WriteLine("Pet was not found.");
            return;
        }

        PetRepository.DeletePet(conn, id);
        Console.WriteLine("Pet deleted successfully.");
    }

    private static void DemonstratePolymorphism(SQLiteConnection conn)
    {
        Console.WriteLine("\n--- Polymorphism Demo ---");

        List<Pet> pets = PetRepository.GetAllPets(conn);

        if (pets.Count == 0)
        {
            Console.WriteLine("No pets available for the polymorphism demo.");
            return;
        }

        foreach (Pet pet in pets)
        {
            Console.WriteLine($"{pet.Name} is handled as a Pet object. Pet type: {pet.GetPetType()}");
        }

        List<IAdoptable> adoptables = new List<IAdoptable>();

        foreach (Pet pet in pets)
        {
            adoptables.Add(pet);
        }

        foreach (IAdoptable adoptable in adoptables)
        {
            Console.WriteLine("Adoption Status: " + adoptable.GetAdoptionStatus());
        }
    }

    private static int ReadInt(string prompt)
    {
        int value;

        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out value))
            {
                return value;
            }

            Console.WriteLine("Please enter a valid number.");
        }
    }

    private static bool ReadBool(string prompt)
    {
        bool value;

        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";

            if (bool.TryParse(input, out value))
            {
                return value;
            }

            Console.WriteLine("Please enter true or false.");
        }
    }
}