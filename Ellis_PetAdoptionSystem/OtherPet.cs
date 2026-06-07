/*******************************************************************
* Name: Princess Ellis
* Date: June 7, 2026
* Assignment: SDC320 Week 4 Course Project - Database Implementation
*
* OtherPet class for animals that are not dogs or cats.
*/
public class OtherPet : Pet
{
    public string AnimalType { get; set; }
    public string SpecialCareInstructions { get; set; }

    public OtherPet(int petId, string name, string animalType, string breed, int age, string adoptionStatus, MedicalRecord medicalRecord, string specialCareInstructions)
        : base(petId, name, "Other", breed, age, adoptionStatus, medicalRecord)
    {
        AnimalType = animalType;
        SpecialCareInstructions = specialCareInstructions;
    }

    public override string GetPetType()
    {
        return "Other";
    }

    public override string ToString()
    {
        return base.ToString() + $"\nAnimal Type: {AnimalType}\nSpecial Care: {SpecialCareInstructions}";
    }
}