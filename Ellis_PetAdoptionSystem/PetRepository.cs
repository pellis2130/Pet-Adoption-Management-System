/*******************************************************************
* Name: Princess Ellis
* Date: June 7, 2026
* Assignment: SDC320 Week 4 Course Project - Database Implementation
*
* Repository class that creates tables and performs CRUD operations.
*/
using System.Collections.Generic;
using System.Data.SQLite;

public class PetRepository
{
    public static void CreateTables(SQLiteConnection conn)
    {
        string petsSql =
            "CREATE TABLE IF NOT EXISTS Pets (" +
            "PetId INTEGER PRIMARY KEY AUTOINCREMENT, " +
            "Name TEXT NOT NULL, " +
            "Species TEXT NOT NULL, " +
            "Breed TEXT, " +
            "Age INTEGER, " +
            "AdoptionStatus TEXT NOT NULL, " +
            "PetType TEXT NOT NULL);";

        string medicalSql =
            "CREATE TABLE IF NOT EXISTS MedicalRecords (" +
            "RecordId INTEGER PRIMARY KEY AUTOINCREMENT, " +
            "PetId INTEGER NOT NULL, " +
            "Vaccinated INTEGER NOT NULL, " +
            "SpayedOrNeutered INTEGER NOT NULL, " +
            "MedicalNotes TEXT, " +
            "LastVetVisit TEXT);";

        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = petsSql;
        cmd.ExecuteNonQuery();

        cmd.CommandText = medicalSql;
        cmd.ExecuteNonQuery();
    }

    public static void AddPet(SQLiteConnection conn, Pet pet)
    {
        string petSql =
            "INSERT INTO Pets(Name, Species, Breed, Age, AdoptionStatus, PetType) " +
            "VALUES(@Name, @Species, @Breed, @Age, @AdoptionStatus, @PetType);";

        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = petSql;
        cmd.Parameters.AddWithValue("@Name", pet.Name);
        cmd.Parameters.AddWithValue("@Species", pet.Species);
        cmd.Parameters.AddWithValue("@Breed", pet.Breed);
        cmd.Parameters.AddWithValue("@Age", pet.Age);
        cmd.Parameters.AddWithValue("@AdoptionStatus", pet.AdoptionStatus);
        cmd.Parameters.AddWithValue("@PetType", pet.GetPetType());
        cmd.ExecuteNonQuery();

        long petId = conn.LastInsertRowId;

        string medicalSql =
            "INSERT INTO MedicalRecords(PetId, Vaccinated, SpayedOrNeutered, MedicalNotes, LastVetVisit) " +
            "VALUES(@PetId, @Vaccinated, @SpayedOrNeutered, @MedicalNotes, @LastVetVisit);";

        cmd = conn.CreateCommand();
        cmd.CommandText = medicalSql;
        cmd.Parameters.AddWithValue("@PetId", petId);
        cmd.Parameters.AddWithValue("@Vaccinated", pet.MedicalRecord.Vaccinated ? 1 : 0);
        cmd.Parameters.AddWithValue("@SpayedOrNeutered", pet.MedicalRecord.SpayedOrNeutered ? 1 : 0);
        cmd.Parameters.AddWithValue("@MedicalNotes", pet.MedicalRecord.MedicalNotes);
        cmd.Parameters.AddWithValue("@LastVetVisit", pet.MedicalRecord.LastVetVisit);
        cmd.ExecuteNonQuery();
    }

    public static List<Pet> GetAllPets(SQLiteConnection conn)
    {
        List<Pet> pets = new List<Pet>();

        string sql =
            "SELECT p.PetId, p.Name, p.Species, p.Breed, p.Age, p.AdoptionStatus, p.PetType, " +
            "m.Vaccinated, m.SpayedOrNeutered, m.MedicalNotes, m.LastVetVisit " +
            "FROM Pets p LEFT JOIN MedicalRecords m ON p.PetId = m.PetId;";

        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            pets.Add(BuildPetFromReader(rdr));
        }

        return pets;
    }

    public static Pet? SearchPet(SQLiteConnection conn, int petId)
    {
        string sql =
            "SELECT p.PetId, p.Name, p.Species, p.Breed, p.Age, p.AdoptionStatus, p.PetType, " +
            "m.Vaccinated, m.SpayedOrNeutered, m.MedicalNotes, m.LastVetVisit " +
            "FROM Pets p LEFT JOIN MedicalRecords m ON p.PetId = m.PetId " +
            "WHERE p.PetId = @PetId;";

        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@PetId", petId);

        SQLiteDataReader rdr = cmd.ExecuteReader();

        if (rdr.Read())
        {
            return BuildPetFromReader(rdr);
        }

        return null;
    }

    public static void UpdatePet(SQLiteConnection conn, Pet pet)
    {
        string petSql =
            "UPDATE Pets SET Name=@Name, Species=@Species, Breed=@Breed, Age=@Age, " +
            "AdoptionStatus=@AdoptionStatus, PetType=@PetType WHERE PetId=@PetId;";

        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = petSql;
        cmd.Parameters.AddWithValue("@Name", pet.Name);
        cmd.Parameters.AddWithValue("@Species", pet.Species);
        cmd.Parameters.AddWithValue("@Breed", pet.Breed);
        cmd.Parameters.AddWithValue("@Age", pet.Age);
        cmd.Parameters.AddWithValue("@AdoptionStatus", pet.AdoptionStatus);
        cmd.Parameters.AddWithValue("@PetType", pet.GetPetType());
        cmd.Parameters.AddWithValue("@PetId", pet.PetId);
        cmd.ExecuteNonQuery();

        string medicalSql =
            "UPDATE MedicalRecords SET Vaccinated=@Vaccinated, SpayedOrNeutered=@SpayedOrNeutered, " +
            "MedicalNotes=@MedicalNotes, LastVetVisit=@LastVetVisit WHERE PetId=@PetId;";

        cmd = conn.CreateCommand();
        cmd.CommandText = medicalSql;
        cmd.Parameters.AddWithValue("@Vaccinated", pet.MedicalRecord.Vaccinated ? 1 : 0);
        cmd.Parameters.AddWithValue("@SpayedOrNeutered", pet.MedicalRecord.SpayedOrNeutered ? 1 : 0);
        cmd.Parameters.AddWithValue("@MedicalNotes", pet.MedicalRecord.MedicalNotes);
        cmd.Parameters.AddWithValue("@LastVetVisit", pet.MedicalRecord.LastVetVisit);
        cmd.Parameters.AddWithValue("@PetId", pet.PetId);
        cmd.ExecuteNonQuery();
    }

    public static void DeletePet(SQLiteConnection conn, int petId)
    {
        SQLiteCommand cmd = conn.CreateCommand();

        cmd.CommandText = "DELETE FROM MedicalRecords WHERE PetId=@PetId;";
        cmd.Parameters.AddWithValue("@PetId", petId);
        cmd.ExecuteNonQuery();

        cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM Pets WHERE PetId=@PetId;";
        cmd.Parameters.AddWithValue("@PetId", petId);
        cmd.ExecuteNonQuery();
    }

    private static Pet BuildPetFromReader(SQLiteDataReader rdr)
    {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string species = rdr.GetString(2);
        string breed = rdr.GetString(3);
        int age = rdr.GetInt32(4);
        string status = rdr.GetString(5);
        string petType = rdr.GetString(6);

        MedicalRecord record = new MedicalRecord(
            rdr.GetInt32(7) == 1,
            rdr.GetInt32(8) == 1,
            rdr.GetString(9),
            rdr.GetString(10)
        );

        if (petType == "Dog")
        {
            return new Dog(id, name, breed, age, status, record, "Medium", true);
        }
        else if (petType == "Cat")
        {
            return new Cat(id, name, breed, age, status, record, true, true);
        }
        else
        {
            return new OtherPet(id, name, species, breed, age, status, record, "Special care may be required.");
        }
    }
}