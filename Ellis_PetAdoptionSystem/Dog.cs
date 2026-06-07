/*******************************************************************
* Name: Princess Ellis
* Date: June 7, 2026
* Assignment: SDC320 Week 4 Course Project - Database Implementation
*
* Dog class that inherits from Pet and demonstrates polymorphism.
*/
public class Dog : Pet
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

    public override string ToString()
    {
        return base.ToString() + $"\nEnergy Level: {EnergyLevel}\nGood With Kids: {GoodWithKids}\nSound: {MakeSound()}";
    }
}