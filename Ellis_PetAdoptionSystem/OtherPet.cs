/*
 Name: Princess Ellis
 Date: 2026
 Assignment: SDC320 Course Project
 Description: OtherPet class for pets that are not dogs or cats.
*/

public class OtherPet : Pet, IAdoptable
{
    public string AnimalType { get; set; }
    public string SpecialCareInstructions { get; set; }

    public OtherPet(int petId, string name, string breed, int age, string adoptionStatus, MedicalRecord medicalRecord, string animalType, string specialCareInstructions)
        : base(petId, name, "Other", breed, age, adoptionStatus, medicalRecord)
    {
        AnimalType = animalType;
        SpecialCareInstructions = specialCareInstructions;
    }

    public override string GetPetType()
    {
        return AnimalType;
    }

    public string GetAdoptionStatus()
    {
        return AdoptionStatus;
    }

    public void MarkAdopted()
    {
        UpdateStatus("Adopted");
    }

    public void MarkAvailable()
    {
        UpdateStatus("Available");
    }

    public override string ToString()
    {
        return base.ToString() + $"\nAnimal Type: {AnimalType}\nSpecial Care: {SpecialCareInstructions}";
    }
}