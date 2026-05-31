/*
 Name: Princess Ellis
 Date: 2026
 Assignment: SDC320 Course Project
 Description: Dog class derived from Pet and implementing IAdoptable.
*/

public class Dog : Pet, IAdoptable
{
    public string EnergyLevel { get; set; }
    public bool GoodWithKids { get; set; }

    public Dog(int petId, string name, string breed, int age, string adoptionStatus, MedicalRecord medicalRecord, string energyLevel, bool goodWithKids)
        : base(petId, name, "Dog", breed, age, adoptionStatus, medicalRecord)
    {
        EnergyLevel = energyLevel;
        GoodWithKids = goodWithKids;
    }

    public override string GetPetType()
    {
        return "Dog";
    }

    public string MakeSound()
    {
        return "Woof!";
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
        return base.ToString() + $"\nEnergy Level: {EnergyLevel}\nGood With Kids: {GoodWithKids}\nSound: {MakeSound()}";
    }
}