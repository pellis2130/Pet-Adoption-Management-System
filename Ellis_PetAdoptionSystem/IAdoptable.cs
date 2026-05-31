/*
 Name: Princess Ellis
 Date: 2026
 Assignment: SDC320 Course Project
 Description: Interface for adoption behavior.
*/

public interface IAdoptable
{
    string GetAdoptionStatus();
    void MarkAdopted();
    void MarkAvailable();
}