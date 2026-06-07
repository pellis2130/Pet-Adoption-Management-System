/*******************************************************************
* Name: Princess Ellis
* Date: June 7, 2026
* Assignment: SDC320 Week 4 Course Project - Database Implementation
*
* Class that represents a pet's medical record.
*/
public class MedicalRecord
{
    public int RecordId { get; set; }
    public bool Vaccinated { get; set; }
    public bool SpayedOrNeutered { get; set; }
    public string MedicalNotes { get; set; }
    public string LastVetVisit { get; set; }

    public MedicalRecord(bool vaccinated, bool spayedOrNeutered, string medicalNotes, string lastVetVisit)
    {
        Vaccinated = vaccinated;
        SpayedOrNeutered = spayedOrNeutered;
        MedicalNotes = medicalNotes;
        LastVetVisit = lastVetVisit;
    }

    public override string ToString()
    {
        return $"Medical Record: Vaccinated: {Vaccinated}, Spayed/Neutered: {SpayedOrNeutered}, Notes: {MedicalNotes}, Last Vet Visit: {LastVetVisit}";
    }
}