/*******************************************************************
* Name: Princess Ellis
* Date: June 7, 2026
* Assignment: SDC320 Week 4 Course Project - Database Implementation
*
* Repository class for database CRUD operations.
*/
using System;
using System.Collections.Generic;
using System.Data.SQLite;

public static class PetRepository
{
    public static void CreateTables(SQLiteConnection conn)
    {
        string sql = @"
        CREATE TABLE IF NOT EXISTS Pets (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Type TEXT NOT NULL,
            Name TEXT NOT NULL,
            AnimalType TEXT,
            Breed TEXT,
            Age INTEGER,
            AdoptionStatus TEXT,
            IsVaccinated INTEGER,
            IsSpayedOrNeutered INTEGER,
            MedicalNotes TEXT,
            LastVetVisit TEXT,
            EnergyLevel TEXT,
            GoodWithKids INTEGER,
            IndoorOnly INTEGER,
            LitterTrained INTEGER,
            SpecialCare TEXT
        );";

        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
        {
            cmd.ExecuteNonQuery();
        }
    }

    public static void AddPet(SQLiteConnection conn, Pet pet)
    {
        string type = pet.GetPetType();

        string animalType = "";
        string energyLevel = "";
        int goodWithKids = 0;
        int indoorOnly = 0;
        int litterTrained = 0;
        string specialCare = "";

        if (pet is Dog dog)
        {
            energyLevel = dog.EnergyLevel;
            goodWithKids = dog.GoodWithKids ? 1 : 0;
        }
        else if (pet is Cat cat)
        {
            indoorOnly = cat.IndoorOnly ? 1 : 0;
            litterTrained = cat.LitterTrained ? 1 : 0;
        }
        else if (pet is OtherPet otherPet)
        {
            animalType = otherPet.AnimalType;
            specialCare = otherPet.SpecialCareInstructions;
        }

        string sql = @"
        INSERT INTO Pets
        (Type, Name, AnimalType, Breed, Age, AdoptionStatus, IsVaccinated, 
         IsSpayedOrNeutered, MedicalNotes, LastVetVisit, EnergyLevel, 
         GoodWithKids, IndoorOnly, LitterTrained, SpecialCare)
        VALUES
        (@Type, @Name, @AnimalType, @Breed, @Age, @AdoptionStatus, @IsVaccinated,
         @IsSpayedOrNeutered, @MedicalNotes, @LastVetVisit, @EnergyLevel,
         @GoodWithKids, @IndoorOnly, @LitterTrained, @SpecialCare);";

        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@Name", pet.Name);
            cmd.Parameters.AddWithValue("@AnimalType", animalType);
            cmd.Parameters.AddWithValue("@Breed", pet.Breed);
            cmd.Parameters.AddWithValue("@Age", pet.Age);
            cmd.Parameters.AddWithValue("@AdoptionStatus", pet.AdoptionStatus);
            cmd.Parameters.AddWithValue("@IsVaccinated", pet.MedicalRecord.Vaccinated ? 1 : 0);
            cmd.Parameters.AddWithValue("@IsSpayedOrNeutered", pet.MedicalRecord.SpayedOrNeutered ? 1 : 0);
            cmd.Parameters.AddWithValue("@MedicalNotes", pet.MedicalRecord.MedicalNotes);
            cmd.Parameters.AddWithValue("@LastVetVisit", pet.MedicalRecord.LastVetVisit);
            cmd.Parameters.AddWithValue("@EnergyLevel", energyLevel);
            cmd.Parameters.AddWithValue("@GoodWithKids", goodWithKids);
            cmd.Parameters.AddWithValue("@IndoorOnly", indoorOnly);
            cmd.Parameters.AddWithValue("@LitterTrained", litterTrained);
            cmd.Parameters.AddWithValue("@SpecialCare", specialCare);

            cmd.ExecuteNonQuery();
        }
    }

    public static List<Pet> GetAllPets(SQLiteConnection conn)
    {
        List<Pet> pets = new List<Pet>();

        string sql = "SELECT * FROM Pets;";

        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
        using (SQLiteDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                pets.Add(BuildPet(reader));
            }
        }

        return pets;
    }

    public static Pet? SearchPet(SQLiteConnection conn, int id)
    {
        string sql = "SELECT * FROM Pets WHERE Id = @Id;";

        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@Id", id);

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return BuildPet(reader);
                }
            }
        }

        return null;
    }

    public static void UpdatePet(SQLiteConnection conn, Pet pet)
    {
        string sql = @"
        UPDATE Pets
        SET Name = @Name,
            AdoptionStatus = @AdoptionStatus,
            MedicalNotes = @MedicalNotes
        WHERE Id = @Id;";

        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@Name", pet.Name);
            cmd.Parameters.AddWithValue("@AdoptionStatus", pet.AdoptionStatus);
            cmd.Parameters.AddWithValue("@MedicalNotes", pet.MedicalRecord.MedicalNotes);
            cmd.Parameters.AddWithValue("@Id", pet.PetId);

            cmd.ExecuteNonQuery();
        }
    }

    public static void DeletePet(SQLiteConnection conn, int id)
    {
        string sql = "DELETE FROM Pets WHERE Id = @Id;";

        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }

    private static Pet BuildPet(SQLiteDataReader reader)
    {
        int id = Convert.ToInt32(reader["Id"]);
        string type = reader["Type"].ToString() ?? "";
        string name = reader["Name"].ToString() ?? "";
        string animalType = reader["AnimalType"].ToString() ?? "";
        string breed = reader["Breed"].ToString() ?? "";
        int age = Convert.ToInt32(reader["Age"]);
        string adoptionStatus = reader["AdoptionStatus"].ToString() ?? "";

        bool isVaccinated = Convert.ToInt32(reader["IsVaccinated"]) == 1;
        bool isSpayedOrNeutered = Convert.ToInt32(reader["IsSpayedOrNeutered"]) == 1;
        string medicalNotes = reader["MedicalNotes"].ToString() ?? "";
        string lastVetVisit = reader["LastVetVisit"].ToString() ?? "";

        MedicalRecord medicalRecord = new MedicalRecord(
            isVaccinated,
            isSpayedOrNeutered,
            medicalNotes,
            lastVetVisit
        );

        if (type == "Dog")
        {
            string energyLevel = reader["EnergyLevel"].ToString() ?? "";
            bool goodWithKids = Convert.ToInt32(reader["GoodWithKids"]) == 1;

            return new Dog(id, name, breed, age, adoptionStatus, medicalRecord, energyLevel, goodWithKids);
        }
        else if (type == "Cat")
        {
            bool indoorOnly = Convert.ToInt32(reader["IndoorOnly"]) == 1;
            bool litterTrained = Convert.ToInt32(reader["LitterTrained"]) == 1;

            return new Cat(id, name, breed, age, adoptionStatus, medicalRecord, indoorOnly, litterTrained);
        }
        else
        {
            string specialCare = reader["SpecialCare"].ToString() ?? "";

            return new OtherPet(id, name, animalType, breed, age, adoptionStatus, medicalRecord, specialCare);
        }
    }
}