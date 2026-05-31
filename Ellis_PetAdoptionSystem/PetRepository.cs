/*
 Name: Princess Ellis
 Date: 2026
 Assignment: SDC320 Course Project
 Description: Handles SQLite CRUD operations for pets.
*/

using Microsoft.Data.Sqlite;

public class PetRepository
{
    private readonly string _connectionString = "Data Source=PetAdoption.db";

    public void InitializeDatabase()
    {
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        connection.Open();

        string createPetsTable = @"
            CREATE TABLE IF NOT EXISTS Pets (
                PetId INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Species TEXT NOT NULL,
                Breed TEXT,
                Age INTEGER,
                AdoptionStatus TEXT NOT NULL,
                PetType TEXT NOT NULL,
                ExtraInfo1 TEXT,
                ExtraInfo2 TEXT
            );";

        string createMedicalTable = @"
            CREATE TABLE IF NOT EXISTS MedicalRecords (
                RecordId INTEGER PRIMARY KEY AUTOINCREMENT,
                PetId INTEGER NOT NULL,
                Vaccinated INTEGER NOT NULL,
                SpayedOrNeutered INTEGER NOT NULL,
                MedicalNotes TEXT,
                LastVetVisit TEXT
            );";

        using SqliteCommand command1 = new SqliteCommand(createPetsTable, connection);
        command1.ExecuteNonQuery();

        using SqliteCommand command2 = new SqliteCommand(createMedicalTable, connection);
        command2.ExecuteNonQuery();
    }

    public void CreatePet(Pet pet)
    {
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        connection.Open();

        string extraInfo1 = "";
        string extraInfo2 = "";

        if (pet is Dog dog)
        {
            extraInfo1 = dog.EnergyLevel;
            extraInfo2 = dog.GoodWithKids.ToString();
        }
        else if (pet is Cat cat)
        {
            extraInfo1 = cat.IndoorOnly.ToString();
            extraInfo2 = cat.LitterTrained.ToString();
        }
        else if (pet is OtherPet other)
        {
            extraInfo1 = other.AnimalType;
            extraInfo2 = other.SpecialCareInstructions;
        }

        string insertPet = @"
            INSERT INTO Pets (Name, Species, Breed, Age, AdoptionStatus, PetType, ExtraInfo1, ExtraInfo2)
            VALUES (@Name, @Species, @Breed, @Age, @AdoptionStatus, @PetType, @ExtraInfo1, @ExtraInfo2);
            SELECT last_insert_rowid();";

        using SqliteCommand command = new SqliteCommand(insertPet, connection);
        command.Parameters.AddWithValue("@Name", pet.Name);
        command.Parameters.AddWithValue("@Species", pet.Species);
        command.Parameters.AddWithValue("@Breed", pet.Breed);
        command.Parameters.AddWithValue("@Age", pet.Age);
        command.Parameters.AddWithValue("@AdoptionStatus", pet.AdoptionStatus);
        command.Parameters.AddWithValue("@PetType", pet.GetPetType());
        command.Parameters.AddWithValue("@ExtraInfo1", extraInfo1);
        command.Parameters.AddWithValue("@ExtraInfo2", extraInfo2);

        long newPetId = (long)command.ExecuteScalar()!;

        string insertMedical = @"
            INSERT INTO MedicalRecords (PetId, Vaccinated, SpayedOrNeutered, MedicalNotes, LastVetVisit)
            VALUES (@PetId, @Vaccinated, @SpayedOrNeutered, @MedicalNotes, @LastVetVisit);";

        using SqliteCommand medicalCommand = new SqliteCommand(insertMedical, connection);
        medicalCommand.Parameters.AddWithValue("@PetId", newPetId);
        medicalCommand.Parameters.AddWithValue("@Vaccinated", pet.MedicalRecord.Vaccinated ? 1 : 0);
        medicalCommand.Parameters.AddWithValue("@SpayedOrNeutered", pet.MedicalRecord.SpayedOrNeutered ? 1 : 0);
        medicalCommand.Parameters.AddWithValue("@MedicalNotes", pet.MedicalRecord.MedicalNotes);
        medicalCommand.Parameters.AddWithValue("@LastVetVisit", pet.MedicalRecord.LastVetVisit);
        medicalCommand.ExecuteNonQuery();
    }

    public List<Pet> GetAllPets()
    {
        List<Pet> pets = new List<Pet>();

        using SqliteConnection connection = new SqliteConnection(_connectionString);
        connection.Open();

        string selectQuery = @"
            SELECT p.PetId, p.Name, p.Species, p.Breed, p.Age, p.AdoptionStatus, p.PetType, p.ExtraInfo1, p.ExtraInfo2,
                   m.Vaccinated, m.SpayedOrNeutered, m.MedicalNotes, m.LastVetVisit
            FROM Pets p
            LEFT JOIN MedicalRecords m ON p.PetId = m.PetId;";

        using SqliteCommand command = new SqliteCommand(selectQuery, connection);
        using SqliteDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            int petId = reader.GetInt32(0);
            string name = reader.GetString(1);
            string species = reader.GetString(2);
            string breed = reader.GetString(3);
            int age = reader.GetInt32(4);
            string status = reader.GetString(5);
            string petType = reader.GetString(6);
            string extra1 = reader.IsDBNull(7) ? "" : reader.GetString(7);
            string extra2 = reader.IsDBNull(8) ? "" : reader.GetString(8);

            bool vaccinated = !reader.IsDBNull(9) && reader.GetInt32(9) == 1;
            bool spayed = !reader.IsDBNull(10) && reader.GetInt32(10) == 1;
            string notes = reader.IsDBNull(11) ? "" : reader.GetString(11);
            string lastVet = reader.IsDBNull(12) ? "" : reader.GetString(12);

            MedicalRecord record = new MedicalRecord(vaccinated, spayed, notes, lastVet);

            Pet pet;

            if (species == "Dog")
            {
                pet = new Dog(petId, name, breed, age, status, record, extra1, bool.Parse(extra2));
            }
            else if (species == "Cat")
            {
                pet = new Cat(petId, name, breed, age, status, record, bool.Parse(extra1), bool.Parse(extra2));
            }
            else
            {
                pet = new OtherPet(petId, name, breed, age, status, record, petType, extra2);
            }

            pets.Add(pet);
        }

        return pets;
    }

    public Pet? SearchPet(int petId)
    {
        return GetAllPets().FirstOrDefault(p => p.PetId == petId);
    }

    public void UpdatePet(Pet pet)
    {
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        connection.Open();

        string updateQuery = @"
            UPDATE Pets
            SET Name = @Name,
                Breed = @Breed,
                Age = @Age,
                AdoptionStatus = @AdoptionStatus
            WHERE PetId = @PetId;";

        using SqliteCommand command = new SqliteCommand(updateQuery, connection);
        command.Parameters.AddWithValue("@Name", pet.Name);
        command.Parameters.AddWithValue("@Breed", pet.Breed);
        command.Parameters.AddWithValue("@Age", pet.Age);
        command.Parameters.AddWithValue("@AdoptionStatus", pet.AdoptionStatus);
        command.Parameters.AddWithValue("@PetId", pet.PetId);
        command.ExecuteNonQuery();
    }

    public void DeletePet(int petId)
    {
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        connection.Open();

        string deleteMedical = "DELETE FROM MedicalRecords WHERE PetId = @PetId;";
        using SqliteCommand medicalCommand = new SqliteCommand(deleteMedical, connection);
        medicalCommand.Parameters.AddWithValue("@PetId", petId);
        medicalCommand.ExecuteNonQuery();

        string deletePet = "DELETE FROM Pets WHERE PetId = @PetId;";
        using SqliteCommand petCommand = new SqliteCommand(deletePet, connection);
        petCommand.Parameters.AddWithValue("@PetId", petId);
        petCommand.ExecuteNonQuery();
    }
}