/*******************************************************************
* Name: Princess Ellis
* Date: June 7, 2026
* Assignment: SDC320 Week 4 Course Project - Database Implementation
*
* Cat class that inherits from Pet and demonstrates polymorphism.
*/
public class Cat : Pet
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

    public override string ToString()
    {
        return base.ToString() + $"\nIndoor Only: {IndoorOnly}\nLitter Trained: {LitterTrained}\nSound: {MakeSound()}";
    }
}