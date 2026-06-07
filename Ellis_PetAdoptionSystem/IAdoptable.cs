/*******************************************************************
* Name: Princess Ellis
* Date: June 7, 2026
* Assignment: SDC320 Week 4 Course Project - Database Implementation
*
* Interface that defines adoption behavior for adoptable pets.
*/
public interface IAdoptable
{
    string GetAdoptionStatus();
    void MarkAdopted();
    void MarkAvailable();
}