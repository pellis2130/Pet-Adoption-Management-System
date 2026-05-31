/*
 Name: Princess Ellis
 Date: 2026
 Assignment: SDC320 Course Project
 Description: Abstract base class for all pets.
*/

public abstract class Pet
{
    private string _name = "";
    private int _age;

    public int PetId { get; set; }

    public string Name
    {
        get { return _name; }
        set { _name = string.IsNullOrWhiteSpace(value) ? "Unknown" : value; }
    }

    public string Species { get; protected set; } = "";
public string Breed { get; set; } = "";

    public int Age
    {
        get { return _age; }
        set { _age = value < 0 ? 0 : value; }
    }

    public string AdoptionStatus { get; protected set; }
    public MedicalRecord MedicalRecord { get; set; }

    protected Pet(int petId, string name, string species, string breed, int age, string adoptionStatus, MedicalRecord medicalRecord)
    {
        PetId = petId;
        Name = name;
        Species = species;
        Breed = breed;
        Age = age;
        AdoptionStatus = adoptionStatus;
        MedicalRecord = medicalRecord;
    }

    public abstract string GetPetType();

    public void UpdateStatus(string status)
    {
        AdoptionStatus = status;
    }

    public override string ToString()
    {
        return $"ID: {PetId}\nName: {Name}\nType: {GetPetType()}\nSpecies: {Species}\nBreed: {Breed}\nAge: {Age}\nStatus: {AdoptionStatus}\nMedical Record: {MedicalRecord}";
    }
}