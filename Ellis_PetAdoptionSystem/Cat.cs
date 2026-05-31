/*
 Name: Princess Ellis
 Date: 2026
 Assignment: SDC320 Course Project
 Description: Cat class derived from Pet and implementing IAdoptable.
*/

public class Cat : Pet, IAdoptable
{
    public bool IndoorOnly { get; set; }
    public bool LitterTrained { get; set; }

    public Cat(int petId, string name, string breed, int age, string adoptionStatus, MedicalRecord medicalRecord, bool indoorOnly, bool litterTrained)
        : base(petId, name, "Cat", breed, age, adoptionStatus, medicalRecord)
    {
        IndoorOnly = indoorOnly;
        LitterTrained = litterTrained;
    }

    public override string GetPetType()
    {
        return "Cat";
    }

    public string MakeSound()
    {
        return "Meow!";
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
        return base.ToString() + $"\nIndoor Only: {IndoorOnly}\nLitter Trained: {LitterTrained}\nSound: {MakeSound()}";
    }
}