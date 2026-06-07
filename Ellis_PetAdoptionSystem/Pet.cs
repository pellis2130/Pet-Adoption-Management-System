/*******************************************************************
* Name: Princess Ellis
* Date: June 7, 2026
* Assignment: SDC320 Week 4 Course Project - Database Implementation
*
* Abstract base class for all pet types.
*/
public abstract class Pet : IAdoptable
{
    public int PetId { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public string Breed { get; set; }
    public int Age { get; set; }
    public string AdoptionStatus { get; set; }
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

    public string GetAdoptionStatus()
    {
        return AdoptionStatus;
    }

    public void MarkAdopted()
    {
        AdoptionStatus = "Adopted";
    }

    public void MarkAvailable()
    {
        AdoptionStatus = "Available";
    }

    public void UpdateStatus(string status)
    {
        AdoptionStatus = status;
    }

    public override string ToString()
    {
        return $"ID: {PetId}\nName: {Name}\nType: {GetPetType()}\nSpecies: {Species}\nBreed: {Breed}\nAge: {Age}\nStatus: {AdoptionStatus}\n{MedicalRecord}";
    }
}