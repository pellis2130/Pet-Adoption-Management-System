/*
 Name: Princess Ellis
 Date: 2026
 Assignment: SDC320 Course Project
 Description: Main console application for the Pet Adoption Management System.
*/

public class Program
{
    private static PetRepository repository = new PetRepository();

    public static void Main(string[] args)
    {
        repository.InitializeDatabase();

        bool running = true;

        while (running)
        {
            ShowMenu();
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    AddPet();
                    break;
                case "2":
                    ViewAllPets();
                    break;
                case "3":
                    SearchPet();
                    break;
                case "4":
                    UpdatePet();
                    break;
                case "5":
                    DeletePet();
                    break;
                case "6":
                    DemonstratePolymorphism();
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
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

    private static void AddPet()
    {
        Console.WriteLine("\n--- Add Pet ---");
        Console.Write("Enter pet type (Dog/Cat/Other): ");
        string type = Console.ReadLine() ?? "";

        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Breed: ");
        string breed = Console.ReadLine() ?? "";

        Console.Write("Age: ");
        int age = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Vaccinated? (true/false): ");
        bool vaccinated = bool.Parse(Console.ReadLine() ?? "false");

        Console.Write("Spayed or Neutered? (true/false): ");
        bool spayed = bool.Parse(Console.ReadLine() ?? "false");

        Console.Write("Medical Notes: ");
        string notes = Console.ReadLine() ?? "";

        Console.Write("Last Vet Visit: ");
        string lastVet = Console.ReadLine() ?? "";

        MedicalRecord record = new MedicalRecord(vaccinated, spayed, notes, lastVet);
        Pet pet;

        if (type.Equals("Dog", StringComparison.OrdinalIgnoreCase))
        {
            Console.Write("Energy Level: ");
            string energy = Console.ReadLine() ?? "";

            Console.Write("Good With Kids? (true/false): ");
            bool goodWithKids = bool.Parse(Console.ReadLine() ?? "false");

            pet = new Dog(0, name, breed, age, "Available", record, energy, goodWithKids);
        }
        else if (type.Equals("Cat", StringComparison.OrdinalIgnoreCase))
        {
            Console.Write("Indoor Only? (true/false): ");
            bool indoorOnly = bool.Parse(Console.ReadLine() ?? "false");

            Console.Write("Litter Trained? (true/false): ");
            bool litterTrained = bool.Parse(Console.ReadLine() ?? "false");

            pet = new Cat(0, name, breed, age, "Available", record, indoorOnly, litterTrained);
        }
        else
        {
            Console.Write("Animal Type: ");
            string animalType = Console.ReadLine() ?? "";

            Console.Write("Special Care Instructions: ");
            string care = Console.ReadLine() ?? "";

            pet = new OtherPet(0, name, breed, age, "Available", record, animalType, care);
        }

        repository.CreatePet(pet);
        Console.WriteLine("Pet added successfully.");
    }

    private static void ViewAllPets()
    {
        Console.WriteLine("\n--- All Pets ---");

        List<Pet> pets = repository.GetAllPets();

        if (pets.Count == 0)
        {
            Console.WriteLine("No pets found.");
            return;
        }

        foreach (Pet pet in pets)
        {
            PrintPetInfo(pet);
        }
    }

    private static void SearchPet()
    {
        Console.Write("\nEnter Pet ID: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        Pet? pet = repository.SearchPet(id);

        if (pet == null)
        {
            Console.WriteLine("Pet not found.");
        }
        else
        {
            PrintPetInfo(pet);
        }
    }

    private static void UpdatePet()
    {
        Console.Write("\nEnter Pet ID to update: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        Pet? pet = repository.SearchPet(id);

        if (pet == null)
        {
            Console.WriteLine("Pet not found.");
            return;
        }

        Console.Write("New Name: ");
        pet.Name = Console.ReadLine() ?? pet.Name;

        Console.Write("New Breed: ");
        pet.Breed = Console.ReadLine() ?? pet.Breed;

        Console.Write("New Age: ");
        pet.Age = int.Parse(Console.ReadLine() ?? pet.Age.ToString());

        Console.Write("New Status (Available/Pending/Adopted): ");
        pet.UpdateStatus(Console.ReadLine() ?? pet.AdoptionStatus);

        repository.UpdatePet(pet);
        Console.WriteLine("Pet updated successfully.");
    }

    private static void DeletePet()
    {
        Console.Write("\nEnter Pet ID to delete: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        repository.DeletePet(id);
        Console.WriteLine("Pet deleted successfully.");
    }

    private static void DemonstratePolymorphism()
    {
        Console.WriteLine("\n--- Polymorphism Demo ---");

        List<Pet> pets = repository.GetAllPets();

        foreach (Pet pet in pets)
        {
            Console.WriteLine($"{pet.Name} is handled as a Pet object. Pet Type: {pet.GetPetType()}");
        }

        Console.WriteLine("\n--- Interface Demo ---");

        foreach (Pet pet in pets)
        {
            if (pet is IAdoptable adoptable)
            {
                PrintAdoptableInfo(adoptable);
            }
        }
    }

    private static void PrintPetInfo(Pet pet)
    {
        Console.WriteLine("\n-------------------------");
        Console.WriteLine(pet);
        Console.WriteLine("-------------------------");
    }

    private static void PrintAdoptableInfo(IAdoptable adoptable)
    {
        Console.WriteLine($"Adoption Status: {adoptable.GetAdoptionStatus()}");
    }
}
