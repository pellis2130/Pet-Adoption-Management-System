/*
 Name: Princess Ellis
 Date: 2026
 Assignment: SDC320 Course Project
 Description: Represents an adoption transaction.
*/

public class Adoption
{
    public int AdoptionId { get; set; }
    public int PetId { get; set; }
    public int AdopterId { get; set; }
    public string AdoptionDate { get; set; }
    public decimal AdoptionFee { get; set; }

    public Adoption(int adoptionId, int petId, int adopterId, string adoptionDate, decimal adoptionFee)
    {
        AdoptionId = adoptionId;
        PetId = petId;
        AdopterId = adopterId;
        AdoptionDate = adoptionDate;
        AdoptionFee = adoptionFee;
    }

    public override string ToString()
    {
        return $"Adoption ID: {AdoptionId}\nPet ID: {PetId}\nAdopter ID: {AdopterId}\nDate: {AdoptionDate}\nFee: ${AdoptionFee}";
    }
}