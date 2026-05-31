/*
 Name: Princess Ellis
 Date: 2026
 Assignment: SDC320 Course Project
 Description: Medical record class used through composition.
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
        return $"Vaccinated: {Vaccinated}, Spayed/Neutered: {SpayedOrNeutered}, Notes: {MedicalNotes}, Last Vet Visit: {LastVetVisit}";
    }
}